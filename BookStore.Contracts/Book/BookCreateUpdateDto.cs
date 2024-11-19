namespace BookStore.Contracts.Book;

/// <summary>
/// Dto для создания или изменения издания
/// </summary>
public class BookCreateUpdateDto
{
    /// <summary>
    /// Название
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Аннотация
    /// </summary>
    public string? Annotation { get; set; }
    /// <summary>
    /// Число страниц
    /// </summary>
    public int? PageCount { get; set; }
    /// <summary>
    /// Год
    /// </summary>
    public int? Year { get; set; }
    /// <summary>
    /// Издательство
    /// </summary>
    public string? Publisher { get; set; }
    /// <summary>
    /// ISBN
    /// </summary>
    public string? Isbn { get; set; }
}
