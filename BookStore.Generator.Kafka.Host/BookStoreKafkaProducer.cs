using BookStore.Application.Contracts.BookAuthors;
using BookStore.Generator.Services;
using Confluent.Kafka;

namespace BookStore.Generator.Kafka.Host;

/// <summary>
/// Имплементация для отправки контрактов через топик Kafka
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="producer">Kafka-продюсер</param>
/// <param name="logger">Логгер</param>
public class BookStoreKafkaProducer(IConfiguration configuration, IProducer<Guid, IList<BookAuthorCreateUpdateDto>> producer, ILogger<BookStoreKafkaProducer> logger) : IProducerService
{
    private readonly string _topicName = configuration.GetSection("Kafka")["TopicName"] ?? throw new ArgumentNullException("TopicName", "TopicName section of Kafka is missing");

    /// <inheritdoc/>
    public async Task SendAsync(IList<BookAuthorCreateUpdateDto> batch)
    {
        try
        {
            logger.LogInformation("Sending a batch of {count} contracts to {topic}", batch.Count, _topicName);
            var message = new Message<Guid, IList<BookAuthorCreateUpdateDto>>
            {
                Key = Guid.NewGuid(),
                Value = batch
            };
            await producer.ProduceAsync(_topicName, message);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during sending a batch of {count} contracts to {topic}", batch.Count, _topicName);
        }
    }
}
