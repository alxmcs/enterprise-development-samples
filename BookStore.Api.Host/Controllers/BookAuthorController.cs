using BookStore.Application.Contracts.BookAuthors;

namespace BookStore.Api.Host.Controllers;

public class BookAuthorController(IBookAuthorService crudService, ILogger<BookAuthorController> logger)
    : CrudControllerBase<BookAuthorDto, BookAuthorCreateUpdateDto, int>(crudService, logger);
