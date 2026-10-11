using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.StylePricings;

/// <summary>
/// DTO для Post/Put-запросов по справочнику со стоимостью татуировок по стилям
/// </summary>
/// <param name="Style">Стиль татуировки</param>
/// <param name="PricePerSqCm">Цена за квадратных сантиметр</param>
/// <param name="MulticolorMultiplier">Коэффициент за несколько цветов</param>
public record class StylePricingCreateUpdateDto
(
    TattooStyle Style,
    decimal PricePerSqCm,
    decimal? MulticolorMultiplier
);