using TattooParlor.Application.Contracts;
using TattooParlor.Application.Contracts.StylePricings;

namespace TattooParlor.Api.Controllers;

/// <summary>
/// Контроллер для операций над стилями
/// </summary>
/// <param name="appService">Аппликейшен служба</param>
/// <param name="logger">Логгер</param>
public class StylePricingController(IApplicationService<StylePricingDto, StylePricingCreateUpdateDto, int> appService, ILogger<StylePricingController> logger)
    : CrudControllerBase<StylePricingDto, StylePricingCreateUpdateDto, int>(appService, logger);
