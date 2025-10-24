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
    /// <param name="id">Идентификатор</param>
    /// <returns>Данные</returns>
    [HttpGet("{id}/BookAuthors")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<IList<BookAuthorDto>> GetBookAuthors(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Get), GetType().Name, id);
        try
        {
            var res = crudService.GetBookAuthors(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Get), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
