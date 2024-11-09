using BookStore.Contracts;
using BookStore.Contracts.BookAuthor;

namespace BookStore.WepApi.Host.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над связями авторов и изданий
/// </summary>
/// <param name="crudService">CRUD-служба</param>
/// <param name="logger">Логгер</param>
public class BookAuthorController(ICrudService<BookAuthorDto, BookAuthorCreateUpdateDto, int> crudService, ILogger<BookAuthorController> logger) 
    : CrudControllerBase<BookAuthorDto, BookAuthorCreateUpdateDto, int>(crudService, logger);
