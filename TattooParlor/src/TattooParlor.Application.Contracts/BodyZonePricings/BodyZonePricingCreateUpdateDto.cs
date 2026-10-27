using System.ComponentModel.DataAnnotations;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.BodyZonePricings;

/// <summary>
/// DTO для Post/Put-запросов по справочнику со стоимостью татуировок по участкам тела
/// </summary>
/// <param name="BodyZone">Участок тела</param>
/// <param name="Multiplier">Коэффициент</param>
public record BodyZonePricingCreateUpdateDto(
    [EnumDataType(typeof(BodyZone))] BodyZone BodyZone,
    [Range(0.01, 100)] decimal Multiplier
);