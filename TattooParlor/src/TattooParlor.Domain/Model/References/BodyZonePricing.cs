using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.References;

/// <summary>
/// Справочник со стоимостью татуировок по участкам тела
/// </summary>
[Table("body_zone_pricings", Schema = "tp")]
public class BodyZonePricing
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Участок тела
    /// </summary>
    [Column("body_zone")]
    public required BodyZone BodyZone { get; set; }

    /// <summary>
    /// Коэффициент
    /// </summary>
    [Column("multiplier")]
    public required decimal Multiplier { get; set; }

    /// <summary>
    /// Сессии с данной зоной
    /// </summary>
    public virtual ICollection<TattooSession> Sessions { get; set; } = [];
}
