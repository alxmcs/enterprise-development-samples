using Microsoft.AspNetCore.Mvc;
using TattooParlor.Application.Contracts.TattooArtists;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Api.Controllers;

/// <summary>
/// Контроллер для операций над тату-мастерами
/// </summary>
/// <param name="appService">Аппликейшен служба</param>
/// <param name="logger">Логгер</param>
public class TattooArtistController(ITattooArtistService appService, ILogger<TattooArtistController> logger)
        : CrudControllerBase<TattooArtistDto, TattooArtistCreateUpdateDto, int>(appService, logger)
{
    /// <summary>
    /// Получение связанных сессий
    /// </summary>
    /// <param name="id">Идентификатор тату-мастера</param>
    /// <returns>Список сессий</returns>
    [HttpGet("{id:int}/sessions")]
    [EndpointSummary("Получение связанных сессий")]
    [EndpointDescription("Возвращает список всех сессий, связанных с указанным тату-мастером")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<TattooSessionDto>> GetSessions(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetSessions), GetType().Name, id);
        try
        {
            var customer = appService.Get(id);
            var res = appService.GetSessions(customer!);
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
    /// Получение списка тату-мастеров с указанным стилем
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <returns>Список тату-мастеров с указанным стилем</returns>
    [HttpGet("by-style/{style}")]
    [EndpointSummary("Получение списка тату-мастеров с указанным стилем")]
    [EndpointDescription("Возвращает список всех тату-мастеров с указанным стилем")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<TattooArtistDto>> GetArtistsByFavoriteStyle(TattooStyle style)
    {
        logger.LogInformation("{method} method of {controller} is called with {style} parameter", nameof(GetArtistsByFavoriteStyle), GetType().Name, style);
        try
        {
            var artists = appService.GetArtistsByFavoriteStyle(style);
            return Ok(artists);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetArtistsByFavoriteStyle), GetType().Name);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred");
        }
    }

    /// <summary>
    /// Топ-5 клиентов по количеству визитов за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала</param>
    /// <param name="endDate">Дата окночания</param>
    /// <returns>Список клиентов с количеством визитов</returns>
    [HttpGet("top-by-revenue")]
    [EndpointSummary("Получение топ-5 тату-мастеров по доходу")]
    [EndpointDescription("Возвращает список топ-5 тату-мастеров по доходу за указанный период")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
public ActionResult<IList<TattooArtistRevenueDto>> GetTop5ArtistsByRevenue([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        logger.LogInformation("{method} method of {controller} is called with {startDate}/{endDate} parameters", nameof(GetTop5ArtistsByRevenue), GetType().Name, startDate, endDate);
        try
        {
            var artists = appService.GetTop5ArtistsByRevenue(startDate, endDate);
            return Ok(artists);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetTop5ArtistsByRevenue), GetType().Name);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred");
        }
    }
}
