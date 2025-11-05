using BookStore.Generator.Generator;
using BookStore.Generator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStore.Generator.Controllers;

/// <summary>
/// Контроллер для запуска информационного обмена через брокер сообщений
/// </summary>
/// <param name="logger"></param>
/// <param name="producerService"></param>
[Route("api/[controller]")]
[ApiController]
public class GeneratorController(ILogger<GeneratorController> logger, IProducerService producerService) : ControllerBase
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
    public async Task<IActionResult> Get([FromQuery] int batchSize, [FromQuery] int payloadLimit, [FromQuery] int waitTime)
    {
        logger.LogInformation("Generating {limit} contracts via {batchSize} batches and {waitTime}s delay", payloadLimit, batchSize, waitTime);
        try
        {
            var counter = 0;
            while (counter < payloadLimit)
            {
                await producerService.SendAsync(BookAuthorGenerator.GenerateLinks(batchSize));
                logger.LogInformation("Batch of {batchSize} items has been sent", batchSize);
                await Task.Delay(waitTime * 1000);
                counter += batchSize;
            }

            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
