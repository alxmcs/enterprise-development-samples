using BookStore.Contracts.Author;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WepApi.Host.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над авторами
/// </summary>
/// <param name="crudService">CRUD-служба</param>
/// <param name="logger">Логгер</param>
public class AuthorController(IAuthorService crudService, ILogger<AuthorController> logger) 
    : CrudControllerBase<AuthorDto, AuthorCreateUpdateDto, int>(crudService, logger)
{
    /// <summary>
    /// Получение списка авторов издания
    /// </summary>
    /// <param name="bookId">Идентификатор издания</param>
    /// <returns>Список авторов</returns>
    [HttpGet("book/{bookId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<AuthorDto>>> GetBookAuthors(int bookId)
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetBookAuthors), GetType().Name);
        try
        {
            var res = await crudService.GetBookAuthors(bookId);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetBookAuthors), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetBookAuthors), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

}
