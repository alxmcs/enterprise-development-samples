using BookStore.Application.Contracts.BookAuthors;
using BookStore.ServiceDefaults.Metrics;

namespace BookStore.Api.Host.Controllers;
/// <summary>
/// Контроллер для CRUD-операций над связями авторов и книг
/// </summary>
/// <param name="crudService">Аппликейшен служба связей</param>
/// <param name="meter">Метрика использования контроллера</param>
/// <param name="logger">Логгер</param>
public class BookAuthorController(IBookAuthorService crudService, IApiMeter meter, ILogger<BookAuthorController> logger)
    : CrudControllerBase<BookAuthorDto, BookAuthorCreateUpdateDto, int>(crudService, meter,logger);
