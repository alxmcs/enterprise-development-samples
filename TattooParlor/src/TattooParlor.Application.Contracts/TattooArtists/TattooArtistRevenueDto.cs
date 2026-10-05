namespace TattooParlor.Application.Contracts.TattooArtists;

/// <summary>
/// DTO для аналитического запроса по мастерам
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="FirstName">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="Revenue">Прибыль</param>
public record TattooArtistRevenueDto
(
    int Id,
    string FirstName,
    string LastName,
    decimal Revenue
);