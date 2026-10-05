using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.TattooArtists;

/// <summary>
/// DTO для Post/Put-запросов по мастерам
/// </summary>
/// <param name="FirstName">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="DateOfBirth">Дата рождения</param>
/// <param name="PhoneNumber">Номер телефона</param>
/// <param name="Email">Электронная почта</param>
/// <param name="WorkStartDate">Дата начала работы</param>
/// <param name="FavoriteStyle">Предпочитаемый стиль</param>
public record TattooArtistCreateUpdateDto
(
    string FirstName,
    string LastName,
    DateOnly? DateOfBirth,
    string? PhoneNumber,
    string? Email,
    DateOnly WorkStartDate,
    TattooStyle? FavoriteStyle
);