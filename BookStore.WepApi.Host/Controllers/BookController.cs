using BookStore.Contracts;
using BookStore.Contracts.Book;

namespace BookStore.WepApi.Host.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над изданиями
/// </summary>
/// <param name="crudService">CRUD-служба</param>
/// <param name="logger">Логгер</param>
public class BookController(ICrudService<BookDto, BookCreateUpdateDto, int> crudService, ILogger<BookController> logger) 
    : CrudControllerBase<BookDto, BookCreateUpdateDto, int>(crudService, logger);
