using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Generator.Domain;
using TattooParlor.Generator.Infrasturucture;
using TattooParlor.ServiceDefaults.Metrics;

namespace TattooParlor.Generator.Presentation;

/// <summary>
/// Контроллер для генерации и отправки контрактов через брокер сообщений
/// </summary>
/// <param name="logger">Логгер</param>
/// <param name="meter">Метрики</param>
/// <param name="producerService">Сервис отправки сообщений</param>
[Route("api/[controller]")]
[ApiController]
public class GeneratorController(ILogger<GeneratorController> logger, IApiMeter meter, IProducerService producerService) : ControllerBase
{
    /// <summary>
    /// Метод для отправки сообщений через брокер
    /// </summary>
    /// <param name="batchSize">Размер батча отправляемых сообщений</param>
    /// <param name="payloadLimit">Количество отправляемых сообщений</param>
    /// <param name="waitTime">Пауза в секундах между отправками батчей</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<TattooSessionCreateUpdateDto>>> Get([FromQuery] int batchSize, [FromQuery] int payloadLimit, [FromQuery] int waitTime, CancellationToken cancellationToken)
    {
        logger.LogInformation("Generating {limit} contracts via {batchSize} batches and {waitTime}s delay", payloadLimit, batchSize, waitTime);
        try
        {
            var list = new List<TattooSessionCreateUpdateDto>(payloadLimit);
            var counter = 0;
            while (counter < payloadLimit)
            {
                var batch = SessionGenerator.GenerateLinks(batchSize);
                await producerService.SendAsync(batch, cancellationToken);
                logger.LogInformation("Batch of {batchSize} items has been sent", batchSize);
                await Task.Delay(waitTime * 1000);
                counter += batchSize;
                list.AddRange(batch);
            }

            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "200");
            return Ok(list);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "500");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}