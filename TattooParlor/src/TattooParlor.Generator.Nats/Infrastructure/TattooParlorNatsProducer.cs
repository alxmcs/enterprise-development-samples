using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Net;
using System.Text.Json;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Generator.Infrasturucture;

namespace TattooParlor.Generator.Nats.Infrastructure;

/// <summary>
/// Имплементация для отправки контрактов через стрим Nats
/// с маршрутизацией подтверждённых и отменённых сессий по разным сабджектам
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="connection">Подключение к Nats</param>
/// <param name="logger">Логгер</param>
public class TattooParlorNatsProducer(IConfiguration configuration, INatsConnection connection, ILogger<TattooParlorNatsProducer> logger) : IProducerService
{
    private readonly string _streamName = configuration.GetSection("Nats")["StreamName"] ?? throw new KeyNotFoundException("StreamName section of Nats is missing");
    private readonly string _confirmedSubjectName = configuration.GetSection("Nats")["ConfirmedSubjectName"] ?? throw new KeyNotFoundException("ConfirmedSubjectName section of Nats is missing");
    private readonly string _canceledSubjectName = configuration.GetSection("Nats")["CanceledSubjectName"] ?? throw new KeyNotFoundException("CanceledSubjectName section of Nats is missing");

    /// <inheritdoc/>
    public async Task SendAsync(IList<TattooSessionCreateUpdateDto> batch, CancellationToken cancellationToken)
    {
        try
        {
            var context = connection.CreateJetStreamContext();
            logger.LogInformation("Establishing a stream {stream} with subjects {confirmed} and {canceled}", _streamName, _confirmedSubjectName, _canceledSubjectName);
            await context.CreateOrUpdateStreamAsync(new StreamConfig(_streamName, [_confirmedSubjectName, _canceledSubjectName])
            {
                Retention = StreamConfigRetention.Limits,
                Storage = StreamConfigStorage.File,
                Discard = StreamConfigDiscard.Old,
                NumReplicas = 1,
                MaxMsgs = 100_000,
                MaxAge = TimeSpan.FromHours(1)
            }, cancellationToken);

            foreach (var stateGroup in batch.GroupBy(contract => contract.CancelDate is not null))
            {
                var subject = stateGroup.Key ? _canceledSubjectName : _confirmedSubjectName;
                var payload = JsonSerializer.SerializeToUtf8Bytes(stateGroup.ToList());
                var ack = await context.PublishAsync(subject, payload, cancellationToken: cancellationToken);
                if (ack?.Error is not null)
                {
                    logger.LogError("Failed to publish a batch of {count} contracts to {subject} of {stream}: {code} {description}",
                        stateGroup.Count(), subject, _streamName, ack.Error.Code, ack.Error.Description);
                }
                else
                {
                    logger.LogInformation("Published a batch of {count} {status} contracts to {subject} of {stream} (seq {seq}, duplicate {duplicate})",
                        stateGroup.Count(), stateGroup.Key ? "canceled" : "confirmed", subject, _streamName, ack?.Seq, ack?.Duplicate);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during sending a batch of {count} contracts to {stream}", batch.Count, _streamName);
        }
    }
}
