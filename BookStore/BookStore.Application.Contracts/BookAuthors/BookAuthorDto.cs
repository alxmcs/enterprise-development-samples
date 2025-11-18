namespace BookStore.Application.Contracts.BookAuthors;
/// <summary>
/// DTO для GET запросов к связям авторов и книг
/// </summary>
/// <param name="Id">Идентификатор связи</param>
/// <param name="AuthorId">Идентификатор автора</param>
/// <param name="BookId">Идентификатор издания</param>
public record BookAuthorDto(int Id, int AuthorId, int BookId);
