using Confluent.Kafka;
using System.Text;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Generator.Infrasturucture;

namespace TattooParlor.Generator.Kafka.Infrastructure;

/// <summary>
/// Имплементация для отправки контрактов через топик Kafka с ключеванием по тату-мастеру,
/// обработкой delivery report и маршрутизацией недоставленных сообщений в DLT
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="producer">Kafka-продюсер</param>
/// <param name="logger">Логгер</param>
public class TattooParlorKafkaProducer(
    IConfiguration configuration,
    IProducer<int, IList<TattooSessionCreateUpdateDto>> producer,
    ILogger<TattooParlorKafkaProducer> logger) : IProducerService
{
    private readonly string _topicName = configuration.GetSection("Kafka")["TopicName"] ?? throw new KeyNotFoundException("TopicName section of Kafka is missing");
    private readonly string _dltTopicName = configuration.GetSection("Kafka")["DltTopicName"] ?? $"{configuration.GetSection("Kafka")["TopicName"]}-dlt";

    /// <inheritdoc/>
    public async Task SendAsync(IList<TattooSessionCreateUpdateDto> batch, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending a batch of {count} contracts to {topic}", batch.Count, _topicName);
        try
        {
            foreach (var artistGroup in batch.GroupBy(contract => contract.TattooArtistId))
            {
                var message = new Message<int, IList<TattooSessionCreateUpdateDto>>
                {
                    Key = artistGroup.Key,
                    Value = [.. artistGroup]
                };
                producer.Produce(_topicName, message, report => HandleDeliveryReport(report));
            }
            producer.Flush(cancellationToken);
            logger.LogInformation("Batch of {count} contracts has been flushed to {topic}", batch.Count, _topicName);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.LogInformation("Sending of a batch of {count} contracts to {topic} has been cancelled", batch.Count, _topicName);
            return;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during sending a batch of {count} contracts to {topic}", batch.Count, _topicName);
        }
    }

    /// <summary>
    /// Обработчик delivery report для отправленного сообщения
    /// </summary>
    /// <param name="report">Отчёт о доставке сообщения</param>
    private void HandleDeliveryReport(DeliveryReport<int, IList<TattooSessionCreateUpdateDto>> report)
    {
        if (report.Error.IsError)
        {
            logger.LogError("Failed to deliver a message with key {key} to {topic}: {reason} (partition {partition}, offset {offset})",
                report.Message.Key, report.Topic, report.Error.Reason, report.Partition, report.Offset);
            SendToDlt(report);
        }
        else
        {
            logger.LogInformation("Delivered a message with key {key} to {topic} (partition {partition}, offset {offset})",
                report.Message.Key, report.Topic, report.Partition, report.Offset);
        }
    }

    /// <summary>
    /// Маршрутизация недоставленного сообщения в DLT с указанием исходного топика и причины ошибки в заголовках
    /// </summary>
    /// <param name="report">Отчёт о неуспешной доставке сообщения</param>
    private void SendToDlt(DeliveryReport<int, IList<TattooSessionCreateUpdateDto>> report)
    {
        var message = report.Message;
        message.Headers ??= new Headers();
        message.Headers.Add("dlt-origin-topic", Encoding.UTF8.GetBytes(report.Topic));
        message.Headers.Add("dlt-error-reason", Encoding.UTF8.GetBytes(report.Error.Reason));
        try
        {
            producer.Produce(_dltTopicName, message);
            logger.LogInformation("A message with key {key} has been routed to DLT {topic}", message.Key, _dltTopicName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to route a message with key {key} to DLT {topic}", message.Key, _dltTopicName);
        }
    }
}
