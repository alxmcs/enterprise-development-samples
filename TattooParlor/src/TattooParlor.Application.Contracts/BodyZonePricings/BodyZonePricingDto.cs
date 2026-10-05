using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.BodyZonePricings;

/// <summary>
/// DTO для Get-запросов по справочнику со стоимостью татуировок по участкам тела
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="BodyZone">Участок тела</param>
/// <param name="Multiplier">Коэффициент</param>
public record BodyZonePricingDto(int Id, BodyZone BodyZone, decimal Multiplier);