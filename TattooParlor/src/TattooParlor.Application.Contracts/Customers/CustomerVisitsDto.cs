namespace TattooParlor.Application.Contracts.Customers;

/// <summary>
/// DTO для аналитического запроса по клиентам
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="FirstName">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="Visits">Число визитов</param>
public record CustomerVisitsDto
(
    int Id,
    string FirstName,
    string LastName,
    int Visits
);