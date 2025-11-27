using BookStore.Application.Contracts;
using BookStore.Application.Contracts.Books;
using BookStore.ServiceDefaults.Metrics;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Host.Controllers;
/// <summary>
/// Контроллер аналитических запросов
/// </summary>
/// <param name="service">Аналитическая служба</param>
/// <param name="logger">Логгер</param>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(IAnalyticsService service, IApiMeter meter, ILogger<AuthorController> logger) : ControllerBase
{
    /// <summary>
    /// Получение последних 5 книг заданного автора
    /// </summary>
    /// <param name="id">Идентификатор автора</param>
    /// <returns>Список из пяти последних изданных книг</returns>
    [HttpGet("last5-books")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<BookDto>>> GetLast5AuthorsBook(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetLast5AuthorsBook), GetType().Name, id);
        try
        {
            var res = await service.GetLast5AuthorsBook(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetLast5AuthorsBook), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                res != null ? "200" : "204");
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetLast5AuthorsBook), GetType().Name, ex);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "500");
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получение топ 5 авторов по числу изданных страниц
    /// </summary>
    /// <returns>Список из пяти наиболее продуктивных авторов</returns>
    [HttpGet("top5-authors")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<KeyValuePair<string, int?>>>> GetTop5AuthorsByPageCount()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetTop5AuthorsByPageCount), GetType().Name);
        try
        {
            var res = await service.GetTop5AuthorsByPageCount();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5AuthorsByPageCount), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                res != null ? "200" : "204");
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetTop5AuthorsByPageCount), GetType().Name, ex);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "500");
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
