using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Model;

/// <summary>
/// Автор
/// </summary>
public class Author
{
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Имя автора
    /// </summary>
    [StringLength(100, ErrorMessage = "Имя автора не должно превышать 100 символов")]
    public string? LastName { get; set; }

    /// <summary>
    /// Фамилия автора
    /// </summary>
    [StringLength(100, ErrorMessage = "Фамилия автора не должна превышать 100 символов")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Отчество автора
    /// </summary>
    [StringLength(100, ErrorMessage = "Отчество автора не должно превышать 100 символов")]
    public string? Patronymic { get; set; }

    /// <summary>
    /// Биография автора
    /// </summary>
    [StringLength(int.MaxValue)]
    public string? Biography { get; set; }

    /// <summary>
    /// Список работ
    /// </summary>
    public virtual ICollection<BookAuthor>? BookAuthors { get; set; }

    /// <summary>
    /// Число работ
    /// </summary>
    public int? WorkCount => BookAuthors?.Count;

}
