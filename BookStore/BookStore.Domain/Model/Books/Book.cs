using BookStore.Domain.Model.BookAuthors;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Model.Books;

/// <summary>
/// Издание
/// </summary>
public class Book
{
    /// <summary>
    /// Идентификатор издания
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Название издания
    /// </summary>
    [StringLength(250, ErrorMessage = "Название издания не должно превышать 250 символов")]
    public string? Title { get; set; }

    /// <summary>
    /// Аннотация
    /// </summary>
    [StringLength(10000, ErrorMessage = "Аннотация издания не должна превышать 10000 символов")]
    public string? Annotation { get; set; }

    /// <summary>
    /// Число страниц
    /// </summary>
    [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Число страниц не может быть отрицательным")]
    public int? PageCount { get; set; }

    /// <summary>
    /// Год издания
    /// </summary>
    [RegularExpression(@"^[1-9]\d{0,3}$", ErrorMessage = "Некорректный год издания")]
    public int? Year { get; set; }

    /// <summary>
    /// Издательство
    /// </summary>
    [StringLength(100, ErrorMessage = "Название издательства не должно превышать 100 символов")]
    public string? Publisher { get; set; }

    /// <summary>
    /// ISBN
    /// </summary>
    [RegularExpression(@"^(\d{3}-)?\d-(\d{5})-(\d{3})-\d$", ErrorMessage = "Некорректный ISBN")]
    public string? Isbn { get; set; }

    /// <summary>
    /// Список авторов
    /// </summary>
    public virtual List<BookAuthor>? BookAuthors { get; set; }

    /// <summary>
    /// Перегрузка метода, возвращающего строковое представление объекта
    /// </summary>
    /// <returns>Название книги</returns>
    public override string ToString() => Title ?? "<Без названия>";
}