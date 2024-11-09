using BookStore.Contracts.Book;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WepApi.Host.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над изданиями
/// </summary>
/// <param name="crudService">CRUD-служба</param>
/// <param name="logger">Логгер</param>
public class BookController(IBookService crudService, ILogger<BookController> logger) 
    : CrudControllerBase<BookDto, BookCreateUpdateDto, int>(crudService, logger)
{
    /// <summary>
    /// Получение всех книг автора
    /// </summary>
    /// <param name="authorId">Идентификатор автора</param>
    /// <returns>Список изданий</returns>
    [HttpGet("author/{authorId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<BookDto>>> GetAuthorBooks(int authorId)
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAuthorBooks), GetType().Name);
        try
        {
            var res = await crudService.GetAuthorBooks(authorId);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAuthorBooks), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetAuthorBooks), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
