namespace BookStore.Application.Contracts.BookAuthors;
/// <summary>
/// DTO для POST/PUT запросов к связям авторов и книг
/// </summary>
/// <param name="AuthorId">Идентификатор автора</param>
/// <param name="BookId">Идентификатор издания</param>
public record BookAuthorCreateUpdateDto(int AuthorId, int BookId);
