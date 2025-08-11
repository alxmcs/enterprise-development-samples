using BookStore.Application.Contracts.Authors;
using BookStore.Application.Contracts.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Host.Controllers;

public class AuthorController(IAuthorService crudService, ILogger<AuthorController> logger)
    : CrudControllerBase<AuthorDto, AuthorCreateUpdateDto, int>(crudService, logger)
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
    public ActionResult<List<BookDto>> GetLast5AuthorsBook(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Get), GetType().Name, id);
        try
        {
            var res = crudService.GetLast5AuthorsBook(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Delete), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Get), GetType().Name, ex);
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
    public ActionResult<List<(string,int)>> GetTop5AuthorsByPageCount()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(Get), GetType().Name);
        try
        {
            var res = crudService.GetTop5AuthorsByPageCount();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Delete), GetType().Name);
            return res.Count > 0 ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(Get), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
