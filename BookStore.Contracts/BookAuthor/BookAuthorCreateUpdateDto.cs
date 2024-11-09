namespace BookStore.Contracts.BookAuthor;

/// <summary>
/// Dto для создания или изменения связи автора и издания
/// </summary>
/// <param name="AuthorId">Идентификатор автора</param>
/// <param name="BookId">Идентификатор издания</param>
public record BookAuthorCreateUpdateDto(int AuthorId, int BookId);
