using BookStore.Application.Contracts.Authors;
using BookStore.Application.Contracts.BookAuthors;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Host.Controllers;
/// <summary>
/// Контроллер для CRUD-операций над авторами
/// </summary>
/// <param name="crudService">Аппликейшен служба авторов</param>
/// <param name="logger">Логгер</param>
public class AuthorController(IAuthorService crudService, ILogger<AuthorController> logger)
    : CrudControllerBase<AuthorDto, AuthorCreateUpdateDto, int>(crudService, logger)
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
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetBookAuthors), GetType().Name);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
