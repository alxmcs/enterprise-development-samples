using TattooParlor.Application.Contracts.BodyZonePricings;
using TattooParlor.Application.Contracts.StylePricings;

namespace TattooParlor.Application.Contracts.TattooSessions;

/// <summary>
/// DTO для Get-запросов по сессиям
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="CustomerId">Идентификатор клиента</param>
/// <param name="TattooArtistId">Идентификатор мастера</param>
/// <param name="SessionDate">Дата сессии</param>
/// <param name="BodyZone">Зона татуировки</param>
/// <param name="Style">Стиль татуировки</param>
/// <param name="HeightInCm">Высота татуировки в сантиметрах</param>
/// <param name="WidthInCm">Ширина татуировки в сантиметрах</param>
/// <param name="IsMulticolor">Индикатор многоцветности</param>
/// <param name="CancelDate">Дата отмены</param>
/// <param name="Price">Стоимость</param>
/// <param name="Prepayment">Аванс</param>
/// <param name="IsPrepaymentNonRefundable">Индикатор возврата аванса</param>
public record TattooSessionDto
(
    Guid Id,
    int CustomerId,
    int TattooArtistId,
    DateTime SessionDate,
    BodyZonePricingDto BodyZone,
    StylePricingDto Style,
    double HeightInCm,
    double WidthInCm,
    bool IsMulticolor,
    DateTime? CancelDate,
    decimal Price,
    decimal Prepayment,
    bool IsPrepaymentNonRefundable
) : IHasId<Guid>;
