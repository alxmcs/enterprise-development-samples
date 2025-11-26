using BookStore.Application.Contracts.BookAuthors;
using BookStore.Application.Contracts.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Host.Controllers;
/// <summary>
/// Контроллер для CRUD-операций над книгами
/// </summary>
/// <param name="crudService">Аппликейшен служба книг</param>
/// <param name="logger">Логгер</param>
public class BookController(IBookService crudService, ILogger<BookController> logger)
    : CrudControllerBase<BookDto, BookCreateUpdateDto, int>(crudService, logger)
{
    /// <summary>
    /// Получение коллекции связанных сущностей по идентифкатору
    /// </summary>
    /// <param name="bookId">Идентификатор</param>
    /// <returns>Данные</returns>
    [HttpGet("{bookId}/Authors")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<BookAuthorDto>>> GetBookAuthors(int bookId)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetBookAuthors), GetType().Name, bookId);
        try
        {
            var res = await crudService.GetBookAuthors(bookId);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetBookAuthors), GetType().Name);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetBookAuthors), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
