using System.ComponentModel.DataAnnotations;
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
    [Required, StringLength(50, MinimumLength = 1)] string FirstName,
    [Required, StringLength(50, MinimumLength = 1)] string LastName,
    DateOnly? DateOfBirth,
    [Phone, StringLength(20)] string? PhoneNumber,
    [EmailAddress, StringLength(100)] string? Email,
    DateOnly WorkStartDate,
    [EnumDataType(typeof(TattooStyle))] TattooStyle? FavoriteStyle
);