using BookStore.Application.Contracts;
using BookStore.Application.Contracts.BookAuthors;

namespace BookStore.Api.Host.Controllers;

public class BookAuthorController(IApplicationService<BookAuthorDto, BookAuthorCreateUpdateDto, int> crudService, ILogger<BookAuthorController> logger)
    : CrudControllerBase<BookAuthorDto, BookAuthorCreateUpdateDto, int>(crudService, logger);
