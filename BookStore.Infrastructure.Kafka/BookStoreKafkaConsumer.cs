using BookStore.Application.Contracts.BookAuthors;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookStore.Infrastructure.Kafka;

/// <summary>
/// Имплементация для отправки контрактов через топик Kafka
/// </summary>
/// <param name="consumer">Kafka-консьюмер</param>
/// <param name="scopeFactory">Фабрика контекста</param>
/// <param name="configuration">Конфигурация</param>
/// <param name="logger">Логгер</param>
public class BookStoreKafkaConsumer(IConsumer<Guid, IList<BookAuthorCreateUpdateDto>> consumer, IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<BookStoreKafkaConsumer> logger) : BackgroundService
{
    private readonly string _topicName = configuration.GetSection("Kafka")["TopicName"] ?? throw new ArgumentNullException("TopicName", "TopicName section of Kafka is missing");

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        await Task.Yield();
        await Consume(stoppingToken);
    }

    /// <summary>
    /// Хендлер для обработки получаемого сообщения
    /// </summary>
    /// <param name="stoppingToken">Токен отмены</param>
    private async Task Consume(CancellationToken stoppingToken)
    {
        consumer.Subscribe(_topicName);
        logger.LogInformation("Consumer successfully subscribed to topic {topic}", _topicName);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Consuming from topic {topic} via consumer {consumer}", _topicName, consumer.Name);
                var consumeResult = consumer.Consume(stoppingToken);

                using var scope = scopeFactory.CreateScope();

                var bookAuthorService = scope.ServiceProvider.GetRequiredService<IBookAuthorService>();
                await bookAuthorService.ReceiveContractList(consumeResult.Message.Value);

                logger.LogInformation("Successfully consumed message {key} from topic {topic} via consumer {consumer}", consumeResult.Message.Key, _topicName, consumer.Name);
                consumer.Commit();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during receiving contracts from {topic}", _topicName);
        }
    }
}