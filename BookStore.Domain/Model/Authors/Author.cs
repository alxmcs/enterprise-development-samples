using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Model.Authors;

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
    /// Перегрузка метода, возвращающего строковое представление объекта
    /// </summary>
    /// <returns>Имя автора</returns>
    public override string ToString() =>
        string.IsNullOrEmpty(Patronymic)
            ? $"{FirstName} {LastName}"
            : $"{LastName} {FirstName} {Patronymic}";
}