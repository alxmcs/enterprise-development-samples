using BookStore.Application.Contracts.BookAuthors;
using BookStore.Generator.Generator;
using BookStore.Generator.Services;
using BookStore.ServiceDefaults.Metrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStore.Generator.Controllers;

/// <summary>
/// Контроллер для запуска информационного обмена через брокер сообщений
/// </summary>
/// <param name="logger">Логгер</param>
/// <param name="meter">Метрика использования контроллера</param>
/// <param name="producerService">Служба отправки сообщений</param>
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
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<BookAuthorCreateUpdateDto>>> Get([FromQuery] int batchSize, [FromQuery] int payloadLimit, [FromQuery] int waitTime)
    {
        logger.LogInformation("Generating {limit} contracts via {batchSize} batches and {waitTime}s delay", payloadLimit, batchSize, waitTime);
        try
        {
            var list = new List<BookAuthorCreateUpdateDto>(payloadLimit);
            var counter = 0;
            while (counter < payloadLimit)
            {
                var batch = BookAuthorGenerator.GenerateLinks(batchSize);
                await producerService.SendAsync(batch);
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
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
