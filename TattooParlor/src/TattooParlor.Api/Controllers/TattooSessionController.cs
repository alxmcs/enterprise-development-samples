using Microsoft.AspNetCore.Mvc;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Api.Controllers;

/// <summary>
/// Контроллер для операций над тату-сессиями
/// </summary>
/// <param name="appService">Аппликейшен служба</param>
/// <param name="logger">Логгер</param>
public class TattooSessionController(ITattooSessionService appService, ILogger<TattooSessionController> logger)
    : CrudControllerBase<TattooSessionDto, TattooSessionCreateUpdateDto, Guid>(appService, logger)
{
    /// <summary>
    /// Получение всех сессий, которые не подлежат возврату
    /// </summary>
    /// <returns>Список сессий, которые не подлежат возврату</returns>
    [HttpGet("non-refundable")]
    [EndpointSummary("Получение всех сессий, которые не подлежат возврату")]
    [EndpointDescription("Возвращает список всех сессий, которые не подлежат возврату")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<TattooSessionDto>> GetAllNonRefundable()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAllNonRefundable), GetType().Name);
        try
        {
            var res = appService.GetAllNonRefundable();
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetAllNonRefundable), GetType().Name);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred");
        }
    }

    /// <summary>
    /// Получение диапазона стоимости для указанного стиля
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <returns>Статичтика</returns>
    [HttpGet("cost-range/{style}")]
    [EndpointSummary("Получение диапазона стоимости для указанного стиля")]
    [EndpointDescription("Возвращает диапазон стоимости для указанного стиля")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TattooSessionRange> GetCostRange(TattooStyle style)
    {
        logger.LogInformation("{method} method of {controller} is called with {style} parameter", nameof(GetCostRange), GetType().Name, style);
        try
        {
            var res = appService.GetCostRange(style);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetCostRange), GetType().Name);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred");
        }
    }
}
