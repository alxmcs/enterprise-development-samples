using BookStore.Contracts;
using BookStore.Contracts.Author;

namespace BookStore.WepApi.Host.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над авторами
/// </summary>
/// <param name="crudService">CRUD-служба</param>
/// <param name="logger">Логгер</param>
public class AuthorController(ICrudService<AuthorDto, AuthorCreateUpdateDto, int> crudService, ILogger<AuthorController> logger) 
    : CrudControllerBase<AuthorDto, AuthorCreateUpdateDto, int>(crudService, logger);
