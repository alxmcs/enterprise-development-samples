using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Net;
using System.Text.Json;
using TattooParlor.Application.Contracts.TattooSessions;

namespace TattooParlor.Infrastructure.Nats;

/// <summary>
/// Консьюмер для получения контрактов из стрима Nats
/// и передачи их в службу сессий
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="connection">Подключение к Nats</param>
/// <param name="scopeFactory">Фабрика контекста</param>
/// <param name="logger">Логгер</param>
public class TattooParlorNatsConsumer(
    IConfiguration configuration,
    INatsConnection connection,
    IServiceScopeFactory scopeFactory,
    ILogger<TattooParlorNatsConsumer> logger) : BackgroundService
{
    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };
    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(5);

    private readonly string _streamName = configuration.GetSection("Nats")["StreamName"] ?? throw new KeyNotFoundException("StreamName section of Nats is missing");
    private readonly string _subjectName = configuration.GetSection("Nats")["SubjectName"] ?? throw new KeyNotFoundException("ConfirmedSubjectName section of Nats is missing");
    private readonly string _consumerName = configuration.GetSection("Nats")["ConsumerName"] ?? "tattoo-sessions-receiver";

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Consuming from {subject} of {stream}", _subjectName, _streamName);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var context = connection.CreateJetStreamContext();
                var consumer = await context.CreateOrUpdateConsumerAsync(_streamName, new ConsumerConfig(_consumerName)
                {
                    DurableName = _consumerName,
                    FilterSubject = _subjectName,
                    AckPolicy = ConsumerConfigAckPolicy.Explicit,
                    AckWait = TimeSpan.FromSeconds(60),
                    MaxDeliver = 5
                }, stoppingToken);

                await foreach (var msg in consumer.ConsumeAsync<byte[]>(cancellationToken: stoppingToken))
                {
                    try
                    {
                        var batch = JsonSerializer.Deserialize<IList<TattooSessionCreateUpdateDto>>(msg.Data, Options) ?? [];
                        logger.LogInformation("Received a batch of {count} contracts from {subject} of {stream}",
                            batch.Count, msg.Subject, _streamName);

                        using var scope = scopeFactory.CreateScope();
                        var sessionService = scope.ServiceProvider.GetRequiredService<ITattooSessionService>();
                        await sessionService.ReceiveSessions(batch, stoppingToken);
                        await msg.AckAsync(cancellationToken: stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Failed to process a message from {subject}; it will be redelivered", msg.Subject);
                        await msg.NakAsync(cancellationToken: stoppingToken);
                    }
                }
                return;
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Consuming from {subject} of {stream} has been cancelled due to application shutdown", _subjectName, _streamName);
                return;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Stream {stream} is not available yet, retrying in {delay}s", _streamName, RetryDelay.TotalSeconds);
                await Task.Delay(RetryDelay, stoppingToken);
            }
        }
    }
}
