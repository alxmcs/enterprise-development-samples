using BookStore.Application.Contracts.Authors;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.ServiceDefaults.Metrics;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Host.Controllers;
/// <summary>
/// Контроллер для CRUD-операций над авторами
/// </summary>
/// <param name="crudService">Аппликейшен служба авторов</param>
/// <param name="meter">Метрика использования контроллера</param>
/// <param name="logger">Логгер</param>
public class AuthorController(IAuthorService crudService, IApiMeter meter, ILogger<AuthorController> logger)
    : CrudControllerBase<AuthorDto, AuthorCreateUpdateDto, int>(crudService, meter, logger)
{
    /// <summary>
    /// Получение коллекции связанных сущностей по идентифкатору
    /// </summary>
    /// <param name="authorId">Идентификатор</param>
    /// <returns>Данные</returns>
    [HttpGet("{authorId}/Books")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<BookAuthorDto>>> GetBookAuthors(int authorId)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetBookAuthors), GetType().Name, authorId);
        try
        {
            var res = await crudService.GetBookAuthors(authorId);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetBookAuthors), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                res != null ? "200" : "204");
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetBookAuthors), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "500");
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
