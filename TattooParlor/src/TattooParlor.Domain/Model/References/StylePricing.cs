using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.References;

public class StylePricing
{
    public required int Id { get; set; }
    public required TattooStyle Style { get; set; }
    public required decimal PricePerCm { get; set; }
    public decimal? MulticolorMultiplier { get; set; }
}
