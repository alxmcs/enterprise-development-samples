using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.References;

/// <summary>
/// Справочник со стоимостью татуировок по участкам тела
/// </summary>
public class BodyZonePricing
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Участок тела
    /// </summary>
    public required BodyZone BodyZone { get; set; }

    /// <summary>
    /// Коэффициент
    /// </summary>
    public required decimal Multiplier { get; set; }
}
