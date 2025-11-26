using BookStore.Application.Contracts.BookAuthors;
using BookStore.Infrastructure.Nats.Deserializers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client.Core;
using NATS.Client.JetStream.Models;
using NATS.Net;

namespace BookStore.Infrastructure.Nats;

/// <summary>
/// Служба для чтения данных из сабжекта Nats при помощи push-консьюмера
/// </summary>
/// <param name="connection">Подключение к Nats</param>
/// <param name="scopeFactory">Фабрика контекста</param>
/// <param name="configuration">Конфигурация</param>
/// <param name="logger">Логгер</param>
public class BookStoreNatsConsumer(INatsConnection connection, IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<BookStoreNatsConsumer> logger) : BackgroundService
{
    private readonly string _streamName = configuration.GetSection("Nats")["StreamName"] ?? throw new KeyNotFoundException("StreamName section of Nats is missing");
    private readonly string _subjectName = configuration.GetSection("Nats")["SubjectName"] ?? throw new KeyNotFoundException("SubjectName section of Nats is missing");

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await connection.ConnectAsync();
            var context = connection.CreateJetStreamContext();
            var consumer = await context.CreateConsumerAsync(_streamName,
                new ConsumerConfig
                {
                    DeliverPolicy = ConsumerConfigDeliverPolicy.All,
                    AckPolicy = ConsumerConfigAckPolicy.Explicit
                },
                stoppingToken);
            logger.LogInformation("Creating consumer for a stream {stream}", _streamName);

            while (!stoppingToken.IsCancellationRequested)
            {
                await foreach (var message in consumer.ConsumeAsync(new BookStorePayloadDeserializer(), cancellationToken: stoppingToken))
                {
                    if (message.Data is null) continue;

                    using var scope = scopeFactory.CreateScope();
                    var bookAuthorService = scope.ServiceProvider.GetRequiredService<IBookAuthorService>();
                    await bookAuthorService.ReceiveContractList(message.Data);
                    logger.LogInformation("Successfully consumed message from subject {subject} of stream {stream}", _subjectName, _streamName);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during receiving contracts from {stream}/{subect}", _subjectName, _subjectName);
        }

    }
}
