using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.StylePricings;

/// <summary>
/// DTO для Get-запросов по справочнику со стоимостью татуировок по стилям
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Style">Стиль татуировки</param>
/// <param name="PricePerSqCm">Цена за квадратных сантиметр</param>
/// <param name="MulticolorMultiplier">Коэффициент за несколько цветов</param>
public record class StylePricingDto
(
    int Id,
    TattooStyle Style,
    decimal PricePerSqCm,
    decimal? MulticolorMultiplier
) : IHasId<int>;

