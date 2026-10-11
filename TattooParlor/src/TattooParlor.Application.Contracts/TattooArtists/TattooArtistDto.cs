using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.TattooArtists;

/// <summary>
/// DTO для Get-запросов по мастерам
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="FirstName">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="DateOfBirth">Дата рождения</param>
/// <param name="PhoneNumber">Номер телефона</param>
/// <param name="Email">Электронная почта</param>
/// <param name="WorkStartDate">Дата начала работы</param>
/// <param name="Rating">Рейтинг</param>
/// <param name="FavoriteStyle">Предпочитаемый стиль</param>
public record TattooArtistDto
(
    int Id,
    string FirstName,
    string LastName,
    DateOnly? DateOfBirth,
    string? PhoneNumber,
    string? Email,
    DateOnly WorkStartDate,
    double Rating,
    TattooStyle? FavoriteStyle
) : IHasId<int>;