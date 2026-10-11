using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.BodyZonePricings;

/// <summary>
/// DTO для Post/Put-запросов по справочнику со стоимостью татуировок по участкам тела
/// </summary>
/// <param name="BodyZone">Участок тела</param>
/// <param name="Multiplier">Коэффициент</param>
public record BodyZonePricingCreateUpdateDto(BodyZone BodyZone, decimal Multiplier);