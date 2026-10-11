using TattooParlor.Application.Contracts;
using TattooParlor.Application.Contracts.BodyZonePricings;

namespace TattooParlor.Api.Controllers;

/// <summary>
/// Контроллер для операций над участком тела
/// </summary>
/// <param name="appService">Аппликейшен служба</param>
/// <param name="logger">Логгер</param>
public class BodyZonePricingController(IApplicationService<BodyZonePricingDto, BodyZonePricingCreateUpdateDto, int> appService, ILogger<BodyZonePricingController> logger)
    : CrudControllerBase<BodyZonePricingDto, BodyZonePricingCreateUpdateDto, int>(appService, logger);
