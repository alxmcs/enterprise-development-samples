using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.TattooSessions;

/// <summary>
///  DTO для Post/Put-запросов по сессиям
/// </summary>
/// <param name="CustomerId">Идентификатор клиента</param>
/// <param name="TattooArtistId">Идентификатор мастера</param>
/// <param name="SessionDate">Дата сессии</param>
/// <param name="BodyZone">Зона татуировки</param>
/// <param name="Style">Стиль татуировки</param>
/// <param name="HeightInCm">Высота татуировки в сантиметрах</param>
/// <param name="WidthInCm">Ширина татуировки в сантиметрах</param>
/// <param name="IsMulticolor">Индикатор многоцветности</param>
/// <param name="CancelDate">Дата отмены</param>
public record TattooSessionCreateUpdateDto
(
    int CustomerId,
    int TattooArtistId,
    DateTime SessionDate,
    BodyZone BodyZone,
    TattooStyle Style,
    double HeightInCm,
    double WidthInCm,
    bool IsMulticolor,
    DateTime? CancelDate
);
