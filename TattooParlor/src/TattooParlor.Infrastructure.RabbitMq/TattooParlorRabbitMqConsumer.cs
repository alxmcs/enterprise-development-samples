using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text.Json;
using TattooParlor.Application.Contracts.TattooSessions;

namespace TattooParlor.Infrastructure.RabbitMq;

/// <summary>
/// Консьюмер для получения контрактов из очереди RabbitMq
/// и передачи их в службу сессий
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="rabbitMqConnection">Подключение к брокеру сообщений</param>
/// <param name="scopeFactory">Фабрика контекста</param>
/// <param name="logger">Логгер</param>
public class TattooParlorRabbitMqConsumer(
    IConfiguration configuration,
    IConnection rabbitMqConnection,
    IServiceScopeFactory scopeFactory,
    ILogger<TattooParlorRabbitMqConsumer> logger) : BackgroundService
{
    private readonly string _queueName = configuration.GetSection("RabbitMQ")["QueueName"] ?? throw new KeyNotFoundException("QueueName section of RabbitMq is missing");

    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(5);

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Consuming from {queue}", _queueName);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // канал создаётся на каждой попытке: после 404 NOT_FOUND брокер закрывает предыдущий
                await using var channel = await rabbitMqConnection.CreateChannelAsync(cancellationToken: stoppingToken);
                var consumer = new SessionConsumer(channel, scopeFactory, logger);
                await channel.BasicConsumeAsync(queue: _queueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);

                await Task.Delay(Timeout.InfiniteTimeSpan, stoppingToken);
                return;
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Consuming from {queue} has been cancelled", _queueName);
                return;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Queue {queue} is not available yet, retrying in {delay}s", _queueName, RetryDelay.TotalSeconds);
                await Task.Delay(RetryDelay, stoppingToken);
            }
        }
    }

    /// <summary>
    /// Консьюмер сообщений: десериализует батч контрактов, передаёт его в службу сессий
    /// и подтверждает обработку брокеру
    /// </summary>
    /// <param name="channel">Канал брокера</param>
    /// <param name="scopeFactory">Фабрика контекста</param>
    /// <param name="logger">Логгер</param>
    private sealed class SessionConsumer(
        IChannel channel,
        IServiceScopeFactory scopeFactory,
        ILogger<TattooParlorRabbitMqConsumer> logger) : AsyncDefaultBasicConsumer(channel)
    {
        private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

        /// <inheritdoc/>
        public override async Task HandleBasicDeliverAsync(string consumerTag, ulong deliveryTag, bool redelivered, string exchange,
            string routingKey, IReadOnlyBasicProperties properties, ReadOnlyMemory<byte> body, CancellationToken cancellationToken = default)
        {
            try
            {
                var batch = JsonSerializer.Deserialize<IList<TattooSessionCreateUpdateDto>>(body.Span, Options) ?? [];
                logger.LogInformation("Received a batch of {count} contracts from {exchange} with routing key {routingKey}",
                    batch.Count, exchange, routingKey);

                using var scope = scopeFactory.CreateScope();
                var sessionService = scope.ServiceProvider.GetRequiredService<ITattooSessionService>();
                await sessionService.ReceiveSessions(batch, cancellationToken);
                await channel.BasicAckAsync(deliveryTag, multiple: false, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process a message with routing key {routingKey}; it will not be requeued", routingKey);
                await channel.BasicNackAsync(deliveryTag, multiple: false, requeue: false, cancellationToken);
            }
        }
    }
}
