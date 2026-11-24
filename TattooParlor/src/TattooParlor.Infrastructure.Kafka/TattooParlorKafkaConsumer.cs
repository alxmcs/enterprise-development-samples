using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TattooParlor.Application.Contracts.TattooSessions;

namespace TattooParlor.Infrastructure.Kafka;

/// <summary>
/// Консьюмер для получения контрактов из топика Kafka
/// и передачи их в службу сессий
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="consumer">Kafka-консьюмер</param>
/// <param name="scopeFactory">Фабрика контекста</param>
/// <param name="logger">Логгер</param>
public class TattooParlorKafkaConsumer(
    IConfiguration configuration,
    IConsumer<int, IList<TattooSessionCreateUpdateDto>> consumer,
    IServiceScopeFactory scopeFactory,
    ILogger<TattooParlorKafkaConsumer> logger) : BackgroundService
{
    private readonly string _topicName = configuration.GetSection("Kafka")["TopicName"] ?? throw new KeyNotFoundException("TopicName section of Kafka is missing");

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Subscribing to {topic}", _topicName);
        consumer.Subscribe(_topicName);
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);
                if (result?.Message?.Value is null) continue;

                logger.LogInformation("Received a batch of {count} contracts from {topic} (partition {partition}, offset {offset})",
                    result.Message.Value.Count, result.Topic, result.Partition, result.Offset);

                using var scope = scopeFactory.CreateScope();
                var sessionService = scope.ServiceProvider.GetRequiredService<ITattooSessionService>();
                await sessionService.ReceiveSessions(result.Message.Value, stoppingToken);
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Consuming from {topic} has been cancelled", _topicName);
        }
        finally
        {
            consumer.Close();
        }
    }
}
