using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TattooParlor.Domain.Model.Common;

/// <summary>
/// Абстрактный класс для хранения персональных данных
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    [Column("first_name")]
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    [Column("last_name")]
    public required string LastName { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    [Column("date_of_birth")]
    public DateOnly? DateOfBirth { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Column("phone_number")]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    [Column("email")]
    public string? Email { get; set; }
}
