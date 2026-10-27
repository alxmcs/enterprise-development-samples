using System.ComponentModel.DataAnnotations;

namespace TattooParlor.Application.Contracts.Customers;

/// <summary>
/// DTO для Post/Put-запросов по клиентам тату-салона
/// </summary>
/// <param name="FirstName">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="DateOfBirth">Дата рождения</param>
/// <param name="PhoneNumber">Номер телефона</param>
/// <param name="Email">Электронная почта</param>
/// <param name="FirstVisitDate">Дата первого визита</param>
/// <param name="HasAllergies">Наличие аллергий</param>
/// <param name="VisitCount">Число визитов</param>
public record CustomerCreateUpdateDto
(
    [Required, StringLength(50, MinimumLength = 1)] string FirstName,
    [Required, StringLength(50, MinimumLength = 1)] string LastName,
    DateOnly? DateOfBirth,
    [Phone, StringLength(20)] string? PhoneNumber,
    [EmailAddress, StringLength(100)] string? Email,
    DateOnly FirstVisitDate,
    bool HasAllergies,
    [Range(0, int.MaxValue)] int VisitCount = 0
);
