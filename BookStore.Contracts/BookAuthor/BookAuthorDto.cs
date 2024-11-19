namespace BookStore.Contracts.BookAuthor;

/// <summary>
/// Dto для просмотра сведений о связи автора и издания
/// </summary>
public class BookAuthorDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public int AuthorId { get; set; }
    /// <summary>
    /// Идентификатор издания
    /// </summary>
    public int BookId { get; set; }
}
