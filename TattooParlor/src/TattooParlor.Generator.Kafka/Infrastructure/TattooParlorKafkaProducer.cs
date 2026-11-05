using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Generator.Infrasturucture;
using Confluent.Kafka;

namespace TattooParlor.Generator.Kafka.Infrastructure;

/// <summary>
/// Имплементация для отправки контрактов через топик Kafka
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="producer">Kafka-продюсер</param>
/// <param name="logger">Логгер</param>
public class TattooParlorKafkaProducer(IConfiguration configuration, IProducer<Guid, IList<TattooSessionCreateUpdateDto>> producer, ILogger<TattooParlorKafkaProducer> logger) : IProducerService
{
    private readonly string _topicName = configuration.GetSection("Kafka")["TopicName"] ?? throw new KeyNotFoundException("TopicName section of Kafka is missing");

    /// <inheritdoc/>
    public async Task SendAsync(IList<TattooSessionCreateUpdateDto> batch)
    {
        try
        {
            logger.LogInformation("Sending a batch of {count} contracts to {topic}", batch.Count, _topicName);
            var message = new Message<Guid, IList<TattooSessionCreateUpdateDto>>
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