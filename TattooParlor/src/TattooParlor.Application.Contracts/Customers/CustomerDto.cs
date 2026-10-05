namespace TattooParlor.Application.Contracts.Customers;

/// <summary>
/// DTO для Get-запросов по клиентам тату-салона
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="FirstName">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="DateOfBirth">Дата рождения</param>
/// <param name="PhoneNumber">Номер телефона</param>
/// <param name="Email">Электронная почта</param>
/// <param name="FirstVisitDate">Дата первого визита</param>
/// <param name="VisitCount">Число визитов</param>
/// <param name="HasAllergies">Наличие аллергий</param>
public record CustomerDto
(
    int Id,
    string FirstName,
    string LastName,
    DateOnly? DateOfBirth,
    string? PhoneNumber,
    string? Email,
    DateOnly FirstVisitDate,
    int VisitCount,
    bool HasAllergies
);
