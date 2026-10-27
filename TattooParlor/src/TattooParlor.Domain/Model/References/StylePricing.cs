using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.References;

/// <summary>
/// Справочник со стоимостью татуировок по стилям
/// </summary>
[Table("style_pricings", Schema = "tp")]
public class StylePricing
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Стиль татуировки
    /// </summary>
    [Column("style")]
    public required TattooStyle Style { get; set; }

    /// <summary>
    /// Цена за квадратных сантиметр
    /// </summary>
    [Column("price_per_sq_cm")]
    public required decimal PricePerSqCm { get; set; }

    /// <summary>
    /// Коэффициент за несколько цветов
    /// </summary>
    [Column("multicolor_multiplier")]
    public decimal? MulticolorMultiplier { get; set; }

    /// <summary>
    /// Сессии с данным стилем
    /// </summary>
    public virtual ICollection<TattooSession> Sessions { get; set; } = [];
}
