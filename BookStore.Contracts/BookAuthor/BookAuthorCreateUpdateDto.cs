namespace BookStore.Contracts.BookAuthor;

/// <summary>
/// Dto для создания или изменения связи автора и издания
/// </summary>
public class BookAuthorCreateUpdateDto
{
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public int AuthorId { get; set; }
    /// <summary>
    /// Идентификатор издания
    /// </summary>
    public int BookId { get; set; }
}
