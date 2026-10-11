using Microsoft.AspNetCore.Mvc;
using TattooParlor.Application.Contracts;

namespace TattooParlor.Api.Controllers;

/// <summary>
/// Базовый контроллер для CRUD-операций над сущностями
/// </summary>
/// <typeparam name="TDto">DTO для Get-запросов</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO для Post/Put-запросов</typeparam>
/// <typeparam name="TKey">Тип идентификатора DTO</typeparam>
/// <param name="appService">Служба для манипуляции DTO</param>
/// <param name="logger">Логгер</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
    where TDto : class, IHasId<TKey>
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Добавление новой записи
    /// </summary>
    /// <param name="newDto">Новые данные</param>
    /// <returns>Добавленные данные</returns> 
    [HttpPost]
    [EndpointSummary("Добавление новой записи")]
    [EndpointDescription("Создаёт запись и возвращает её с присвоенным идентификатором")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Create(TCreateUpdateDto newDto)
    {
        logger.LogInformation("{method} method of {controller} is called with {@dto} parameter", nameof(Create), GetType().Name, newDto);
        try
        {
            var res = appService.Create(newDto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Create), GetType().Name);
            return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Create), GetType().Name);
            return StatusCode(500, "Internal server error occurred");
        }
    }

    /// <summary>
    /// Изменение имеющихся данных
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="newDto">Измененные данные</param>
    /// <returns>Обновленные данные</returns>
    [HttpPut("{id}")]
    [EndpointSummary("Изменение имеющихся данных")]
    [EndpointDescription("Изменяет данные по указанному идентификатору и возвращает обновленные данные")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Edit(TKey id, TCreateUpdateDto newDto)
    {
        logger.LogInformation("{method} method of {controller} is called with {key},{@dto} parameters", nameof(Edit), GetType().Name, id, newDto);
        try
        {
            var res = appService.Update(newDto, id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Edit), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Edit), GetType().Name);
            return StatusCode(500, "Internal server error occurred");
        }
    }

    /// <summary>
    /// Удаление данных
    /// </summary>
    /// <param name="id">Идентификатор</param>
    [HttpDelete("{id}")]
    [EndpointSummary("Удаление имеющихся данных")]
    [EndpointDescription("Удаляет данные по указанному идентификатору")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public IActionResult Delete(TKey id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Delete), GetType().Name, id);
        try
        {
            var res = appService.Delete(id);
            logger.LogInformation("{method} method of {controller} executed with result {res}", nameof(Delete), GetType().Name, res);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Delete), GetType().Name);
            return StatusCode(500, "Internal server error occurred");
        }
    }

    /// <summary>
    /// Получение списка всех данных
    /// </summary>
    /// <returns>Список всех данных</returns>
    [HttpGet]
    [EndpointSummary("Получение списка всех данных")]
    [EndpointDescription("Возвращает список всех данных")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<IList<TDto>> GetAll()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAll), GetType().Name);
        try
        {
            var res = appService.GetAll();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAll), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetAll), GetType().Name);
            return StatusCode(500, "Internal server error occurred");
        }
    }

    /// <summary>
    /// Получение данных по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Данные</returns>
    [HttpGet("{id}")]
    [EndpointSummary("Получение данных по идентификатору")]
    [EndpointDescription("Возвращает данные по указанному идентификатору")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Get(TKey id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Get), GetType().Name, id);
        try
        {
            var res = appService.Get(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
            return NotFound(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
            return StatusCode(500, "Internal server error occurred");
        }
    }

}
