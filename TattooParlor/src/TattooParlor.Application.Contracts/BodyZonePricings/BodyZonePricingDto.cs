using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.BodyZonePricings;

public record BodyZonePricingDto(int Id, BodyZone BodyZone, decimal Multiplier);