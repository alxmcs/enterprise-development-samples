namespace BookStore.Contracts.Author;
/// <summary>
/// Наследник CRUD-службы с дополнительными операциями для авторов
/// </summary>
public interface IAuthorService : ICrudService<AuthorDto, AuthorCreateUpdateDto, int>
{
    /// <summary>
    /// Возвращает авторов издания
    /// </summary>
    /// <param name="bookId">Идентификатор издания</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список авторов</returns>
    public Task<IList<AuthorDto>?> GetBookAuthors(int bookId, CancellationToken cancellationToken);
}
