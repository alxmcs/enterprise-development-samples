using BookStore.Domain.Model.Authors;
using BookStore.Domain.Model.Books;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Model.BookAuthors;

/// <summary>
/// Связь автора и издания
/// </summary>
[Table("book_authors")]
public class BookAuthor
{
    /// <summary>
    /// Идентификатор связи
    /// </summary>
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор автора
    /// </summary>
    [Column("author_id")]
    public required int AuthorId { get; set; }

    /// <summary>
    /// Навигейшен автора
    /// </summary>
    public virtual Author? Author { get; set; }

    /// <summary>
    /// Идентификатор издания
    /// </summary>
    [Column("book_id")]
    public required int BookId { get; set; }

    /// <summary>
    /// Навигейшен издания
    /// </summary>
    public virtual Book? Book { get; set; }
}
