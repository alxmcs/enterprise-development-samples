using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.StylePricings;

public record class StylePricingDto
    (
        int Id,
        TattooStyle Style,
        decimal PricePerSqCm,
        decimal? MulticolorMultiplier
    );

