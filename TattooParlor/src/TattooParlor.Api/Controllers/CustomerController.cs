using Microsoft.AspNetCore.Mvc;
using TattooParlor.Application.Contracts.Customers;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Api.Controllers;

/// <summary>
/// Контроллер для операций над клиентами
/// </summary>
/// <param name="appService">Аппликейшен служба</param>
/// <param name="logger">Логгер</param>
public class CustomerController(ICustomerService appService, ILogger<CustomerController> logger)
    : CrudControllerBase<CustomerDto, CustomerCreateUpdateDto, int>(appService, logger)
{
    /// <summary>
    /// Получение связанных сессий
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Список сессий</returns>
    [HttpGet("{id:int}/sessions")]
    [EndpointSummary("Получение связанных сессий")]
    [EndpointDescription("Возвращает список всех сессий, связанных с указанным клиентом")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<TattooSessionDto>>> GetSessions(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetSessions), GetType().Name, id);
        try
        {
            var customer = await appService.Get(id);
            var res = await appService.GetSessions(customer!);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetSessions), GetType().Name);
            return NotFound(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetSessions), GetType().Name);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred");
        }
    }

    /// <summary>
    /// Получение списка клиентов с указанным стилем
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <returns>Список клиентов с указанным стилем</returns>
    [HttpGet("by-style/{style}")]
    [EndpointSummary("Получение списка клиентов с указанным стилем")]
    [EndpointDescription("Возвращает список всех клиентов с указанным стилем")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<CustomerDto>>> GetCustomersWithStyle(TattooStyle style)
    {
        logger.LogInformation("{method} method of {controller} is called with {style} parameter", nameof(GetCustomersWithStyle), GetType().Name, style);
        try
        {
            var customers = await appService.GetCustomersWithStyle(style);
            return Ok(customers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetCustomersWithStyle), GetType().Name);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred");
        }
    }

    /// <summary>
    /// Топ-5 клиентов по количеству визитов за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала</param>
    /// <param name="endDate">Дата окночания</param>
    /// <returns>Список клиентов с количеством визитов</returns>
    [HttpGet("top-by-visits")]
    [EndpointSummary("Получение топ-5 клиентов по количеству визитов")]
    [EndpointDescription("Возвращает список топ-5 клиентов по количеству визитов за указанный период")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<CustomerVisitsDto>>> GetTop5CustomersByVisits([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        logger.LogInformation("{method} method of {controller} is called with {startDate}/{endDate} parameters", nameof(GetTop5CustomersByVisits), GetType().Name, startDate, endDate);
        try
        {
            var customers = await appService.GetTop5CustomersByVisits(startDate, endDate);
            return Ok(customers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetTop5CustomersByVisits), GetType().Name);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred");
        }
    }
}