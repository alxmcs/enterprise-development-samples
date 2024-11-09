namespace BookStore.Contracts.BookAuthor;

/// <summary>
/// Dto для просмотра сведений о связи автора и издания
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="AuthorId">Идентификатор автора</param>
/// <param name="BookId">Идентификатор издания</param>
public record BookAuthorDto(int Id, int AuthorId, int BookId);
