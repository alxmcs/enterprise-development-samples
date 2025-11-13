using BookStore.Application.Contracts.BookAuthors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace BookStore.Infrastructure.RabbitMq;

/// <summary>
/// Служба для чтения данных из очереди RabbitMQ
/// </summary>
/// <param name="connection">Подключение к RabbitMQ</param>
/// <param name="scopeFactory">Фабрика контекста</param>
/// <param name="configuration">Конфигурация</param>
/// <param name="logger">Логгер</param>
public class BookStoreRabbitMqConsumer(IConnection connection, IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<BookStoreRabbitMqConsumer> logger) : BackgroundService
{
    private readonly string _queueName = configuration.GetSection("RabbitMq")["QueueName"] ?? throw new KeyNotFoundException("QueueName section of RabbitMq is missing");
    
    /// <inheritdoc/>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Establishing channel to queue {queue}", _queueName);

        stoppingToken.ThrowIfCancellationRequested();
        var channel = connection.CreateModel();
        channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false);

        logger.LogInformation("Began listening to queue {queue}", _queueName);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (_, ea) => await ReceiveMessage(ea, stoppingToken); 
        channel.BasicConsume(_queueName, true, consumer);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Хендлер для обработки получаемого сообщения
    /// </summary>
    /// <param name="args">Аргументы события</param>
    /// <param name="stoppingToken">Токен отмены</param>
    /// <exception cref="ArgumentNullException">Если сериализация боди пейлоада не удалась</exception>
    private async Task ReceiveMessage(BasicDeliverEventArgs args, CancellationToken stoppingToken)
    {
        logger.LogInformation("Received a message from queue {queue}", _queueName);
        try
        {
            stoppingToken.ThrowIfCancellationRequested();
            var contracts = JsonSerializer.Deserialize<List<BookAuthorCreateUpdateDto>>(new MemoryStream(args.Body.ToArray())) 
                ?? throw new FormatException("Unable to parse contracts from message body"); ;
            using var scope = scopeFactory.CreateScope();
            var bookAuthorService = scope.ServiceProvider.GetRequiredService<IBookAuthorService>();
            await bookAuthorService.ReceiveContractList(contracts);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,"Exception occured during receiving contracts from {queue}", _queueName);
        }
    }
}
