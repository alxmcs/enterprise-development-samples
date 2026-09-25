using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.BodyZonePricings;

public record BodyZonePricingCreateUpdateDto(BodyZone BodyZone, decimal Multiplier);