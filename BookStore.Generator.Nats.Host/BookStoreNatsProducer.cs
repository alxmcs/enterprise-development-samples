using BookStore.Application.Contracts.BookAuthors;
using BookStore.Generator.Services;
using NATS.Client.Core;
using NATS.Net;
using System.Text.Json;

namespace BookStore.Generator.Nats.Host;

/// <summary>
/// Имплементация для отправки контрактов через стрим Nats
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="connection">Подключение к Nats</param>
/// <param name="logger">Логгер</param>
public class BookStoreNatsProducer(IConfiguration configuration, INatsConnection connection, ILogger<BookStoreNatsProducer> logger) : IProducerService
{
    private readonly string _streamName = configuration.GetSection("Nats")["StreamName"] ?? throw new ArgumentNullException("StreamName", "StreamName section of Nats is missing");
    private readonly string _subjectName = configuration.GetSection("Nats")["SubjectName"] ?? throw new ArgumentNullException("SubjectName", "SubjectName section of Nats is missing");

    /// <inheritdoc/>
    public async Task SendAsync(IList<BookAuthorCreateUpdateDto> batch)
    {
        try
        {
            await connection.ConnectAsync();
            var context = connection.CreateJetStreamContext();
            var stream = context.CreateOrUpdateStreamAsync(new NATS.Client.JetStream.Models.StreamConfig(_streamName, [_subjectName]));
            logger.LogInformation("Establishing a stream {stream} with subject {subject}", _streamName, _subjectName);

            await context.PublishAsync(_subjectName, JsonSerializer.SerializeToUtf8Bytes(batch));
            logger.LogInformation("Sent a batch of {count} contracts to {subject} of {stream}", batch.Count, _subjectName, _streamName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during sending a batch of {count} contracts to {stream}/{subject}", batch.Count, _streamName, _subjectName);
        }
    }
}
