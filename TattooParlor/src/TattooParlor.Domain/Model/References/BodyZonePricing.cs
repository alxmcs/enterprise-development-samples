using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.References;

public class BodyZonePricing
{
    public required int Id { get; set; }
    public required BodyZone BodyZone { get; set; }
    public required decimal Multiplier { get; set; }
}
