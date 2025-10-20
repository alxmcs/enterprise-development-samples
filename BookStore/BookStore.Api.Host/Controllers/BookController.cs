using BookStore.Application.Contracts;
using BookStore.Application.Contracts.Books;

namespace BookStore.Api.Host.Controllers;

public class BookController(IApplicationService<BookDto, BookCreateUpdateDto, int> crudService, ILogger<BookController> logger)
    : CrudControllerBase<BookDto, BookCreateUpdateDto, int>(crudService, logger);
