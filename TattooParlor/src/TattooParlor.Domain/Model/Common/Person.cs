namespace TattooParlor.Domain.Model.Common;

/// <summary>
/// Абстрактный класс для хранения персональных данных
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly? DateOfBirth { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string? Email { get; set; }
}
