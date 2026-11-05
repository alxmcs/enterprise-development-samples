using RabbitMQ.Client;
using System.Text.Json;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Generator.Infrasturucture;

namespace TattooParlor.Generator.Rabbitmq.Infractructure;

/// <summary>
/// Имплементация для отправки контрактов через топик exchange RabbitMq
/// с маршрутизацией подтверждённых и отменённых сессий в разные очереди
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="rabbitMqConnection">Подключение к брокеру сообщений</param>
/// <param name="logger">Логгер</param>
public class TattooParlorRabbitMqProducer(IConfiguration configuration, IConnection rabbitMqConnection, ILogger<TattooParlorRabbitMqProducer> logger) : IProducerService
{
    private readonly string _exchangeName = configuration.GetSection("RabbitMQ")["ExchangeName"] ?? throw new KeyNotFoundException("ExchangeName section of RabbitMq is missing");
    private readonly string _confirmedQueueName = configuration.GetSection("RabbitMQ")["ConfirmedQueueName"] ?? throw new KeyNotFoundException("ConfirmedQueueName section of RabbitMq is missing");
    private readonly string _canceledQueueName = configuration.GetSection("RabbitMQ")["CanceledQueueName"] ?? throw new KeyNotFoundException("CanceledQueueName section of RabbitMq is missing");
    private readonly string _confirmedRoutingKey = configuration.GetSection("RabbitMQ")["ConfirmedRoutingKey"] ?? throw new KeyNotFoundException("ConfirmedRoutingKey section of RabbitMq is missing");
    private readonly string _canceledRoutingKey = configuration.GetSection("RabbitMQ")["CanceledRoutingKey"] ?? throw new KeyNotFoundException("CanceledRoutingKey section of RabbitMq is missing");

    /// <inheritdoc/>
    public async Task SendAsync(IList<TattooSessionCreateUpdateDto> batch, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Sending a batch of {count} contracts to {exchange}", batch.Count, _exchangeName);
            await using var channel = await rabbitMqConnection.CreateChannelAsync(cancellationToken: cancellationToken);
            await DeclareTopologyAsync(channel, cancellationToken);

            foreach (var stateGroup in batch.GroupBy(contract => contract.CancelDate is not null))
            {
                var routingKey = stateGroup.Key ? _canceledRoutingKey : _confirmedRoutingKey;
                var payload = JsonSerializer.SerializeToUtf8Bytes(stateGroup.ToList());
                await channel.BasicPublishAsync(
                    exchange: _exchangeName,
                    routingKey: routingKey,
                    mandatory: false,
                    basicProperties: new BasicProperties { DeliveryMode = DeliveryModes.Persistent },
                    body: payload,
                    cancellationToken: cancellationToken);
                logger.LogInformation("A batch of {count} {status} contracts has been sent with routing key {routingKey}",
                    stateGroup.Count(), stateGroup.Key ? "canceled" : "confirmed", routingKey);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.LogInformation("Sending of a batch of {count} contracts to {exchange} has been cancelled", batch.Count, _exchangeName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during sending a batch of {count} contracts to {exchange}", batch.Count, _exchangeName);
        }
    }

    /// <summary>
    /// Объявление топологии: топик exchange и две очереди с биндингами по роутинг-ключам
    /// </summary>
    /// <param name="channel">Канал брокера</param>
    /// <param name="cancellationToken">Токен отмены</param>
    private async Task DeclareTopologyAsync(IChannel channel, CancellationToken cancellationToken)
    {
        await channel.ExchangeDeclareAsync(exchange: _exchangeName, type: ExchangeType.Topic, durable: true, cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(queue: _confirmedQueueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: cancellationToken);
        await channel.QueueBindAsync(queue: _confirmedQueueName, exchange: _exchangeName, routingKey: _confirmedRoutingKey, cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(queue: _canceledQueueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: cancellationToken);
        await channel.QueueBindAsync(queue: _canceledQueueName, exchange: _exchangeName, routingKey: _canceledRoutingKey, cancellationToken: cancellationToken);
    }
}
