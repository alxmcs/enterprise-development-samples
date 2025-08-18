using BookStore.Application.Contracts.BookAuthors;
using BookStore.Generator.Services;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BookStore.Generator.RabbitMq.Host;

/// <summary>
/// Имплементация для отправки контрактов через очередь RabbitMq
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="rabbitMqConnection">Подключение к брокеру сообщений</param>
public class RabbitMqProducer(IConfiguration configuration, IConnection rabbitMqConnection, ILogger<RabbitMqProducer> logger) : IProducerService
{
    private readonly string _queueName = configuration.GetSection("RabbitMq")["QueueName"] ?? throw new ArgumentNullException("QueueName", "QueueName section of RabbitMq is missing");

    /// <inheritdoc/>
    public Task SendAsync(IList<BookAuthorCreateUpdateDto> batch)
    {
        try
        {
            logger.LogInformation("Sending a batch of {count} contracts to {queue}", batch.Count, _queueName);
            var json = JsonSerializer.Serialize(batch);
            var payload = Encoding.UTF8.GetBytes(json);

            using var channel = rabbitMqConnection.CreateModel();
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false);
            channel.BasicPublish(exchange: string.Empty, routingKey: _queueName, mandatory: false, body: payload);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            logger.LogError("Exception occured during sending a batch of {count} contracts to {queue}/ Exception: {@ex}", batch.Count, _queueName, ex);
            return Task.CompletedTask;
        }
    }
}
