using BookStore.Application.Contracts;
using BookStore.Application.Contracts.BookAuthors;

namespace BookStore.Api.Host.Controllers;
/// <summary>
/// Контроллер для CRUD-операций над связями авторов и книг
/// </summary>
/// <param name="crudService">Аппликейшен служба связей</param>
/// <param name="logger">Логгер</param>
public class BookAuthorController(IApplicationService<BookAuthorDto, BookAuthorCreateUpdateDto, int> crudService, ILogger<BookAuthorController> logger)
    : CrudControllerBase<BookAuthorDto, BookAuthorCreateUpdateDto, int>(crudService, logger);
