using BookStore.Application.Contracts;
using BookStore.ServiceDefaults.Metrics;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Host.Controllers;
/// <summary>
/// Базовый контроллер для CRUD-операций над сущностями
/// </summary>
/// <typeparam name="TDto">DTO для Get-запросов</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO для Post/Put-запросов</typeparam>
/// <typeparam name="TKey">Тип идентификатора DTO</typeparam>
/// <param name="appService">Служба для манипуляции DTO</param>
/// <param name="meter">Метрика использования контроллера</param> 
/// <param name="logger">Логгер</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    IApiMeter meter,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Добавление новой записи
    /// </summary>
    /// <param name="newDto">Новые данные</param>
    /// <returns>Добавленные данные</returns> 
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Create(TCreateUpdateDto newDto)
    {
        logger.LogInformation("{method} method of {controller} is called with {@dto} parameter", nameof(Create), GetType().Name, newDto);
        try
        {
            var res = await appService.Create(newDto);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Create), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "201");
            return CreatedAtAction(nameof(this.Create), res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Create), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "500");
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Изменение имеющихся данных
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="newDto">Измененные данные</param>
    /// <returns>Обновленные данные</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Edit(TKey id, TCreateUpdateDto newDto)
    {
        logger.LogInformation("{method} method of {controller} is called with {key},{@dto} parameters", nameof(Edit), GetType().Name, id, newDto);
        try
        {
            var res = await appService.Update(newDto, id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Edit), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "200");
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Edit), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "500");
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Удаление данных
    /// </summary>
    /// <param name="id">Идентификатор</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(TKey id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Delete), GetType().Name, id);
        try
        {
            var res = await appService.Delete(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Delete), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                res ? "200": "204");
            return res ? Ok() : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Delete), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "500");
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получение списка всех данных
    /// </summary>
    /// <returns>Список всех данных</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetAll), GetType().Name);
        try
        {
            var res = await appService.GetAll();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAll), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "200");
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(GetAll), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "500");
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получение данных по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Данные</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Get(TKey id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(Get), GetType().Name, id);
        try
        {
            var res = await appService.Get(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(Get), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                res != null ? "200" : "404");
            return res != null ? Ok(res) : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception happened during {method} method of {controller}", nameof(Get), GetType().Name);
            meter.RecordCall(
                ControllerContext.ActionDescriptor.ControllerName,
                ControllerContext.ActionDescriptor.MethodInfo.Name,
                ControllerContext.HttpContext.Request.Method,
                "500");
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

}
