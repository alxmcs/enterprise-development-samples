using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.References;

/// <summary>
/// Справочник со стоимостью татуировок по стилям
/// </summary>
public class StylePricing
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Стиль татуировки
    /// </summary>
    public required TattooStyle Style { get; set; }

    /// <summary>
    /// Цена за квадратных сантиметр
    /// </summary>
    public required decimal PricePerSqCm { get; set; }

    /// <summary>
    /// Коэффициент за несколько цветовё
    /// </summary>
    public decimal? MulticolorMultiplier { get; set; }
}
