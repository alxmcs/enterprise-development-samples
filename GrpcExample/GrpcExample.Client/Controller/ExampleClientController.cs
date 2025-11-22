using Grpc.Core;
using GrpcExample.Protos;
using Microsoft.AspNetCore.Mvc;

namespace GrpcExample.Client.Controller;

/// <summary>
/// Контроллер для запуска gRPC ручек
/// </summary>
/// <param name="client">Клиент gRPC</param>
/// <param name="logger">Логгер</param>
[Route("api/[controller]/[action]")]
[ApiController]
public class ExampleClientController(ExampleService.ExampleServiceClient client, ILogger<ExampleClientController> logger) : ControllerBase
{
    /// <summary>
    /// Вызов унарной ручки, возвращающей единственный контракт
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Один контракт</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Sample>> GetSampleUnary(int id)
    {
        logger.LogInformation("Calling gRPC client method {method} with parameters {params}", nameof(GetSampleUnary), id);
        try
        {
            var result = await client.GetSampleUnaryAsync(new GetSampleByIdRequest { SampleId = id }, deadline: DateTime.UtcNow.AddSeconds(15));
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSampleUnary));
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Вызов унарной ручки, возвращающей контракт с повторяющимся полем
    /// </summary>
    /// <param name="count">Число контрактов</param>
    /// <returns>Коллекция контрактов внутри единственного ответа</returns>
    [HttpGet("{count:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<Sample>>> GetSamplesUnary(int count)
    {
        logger.LogInformation("Calling gRPC client method {method} with parameters {params}", nameof(GetSamplesUnary), count);
        try
        {
            var result = await client.GetSamplesUnaryAsync(new GetSampleCountRequest { SampleCount = count }, deadline: DateTime.UtcNow.AddSeconds(15));
            return Ok(result.Samples);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSamplesUnary));
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Вызов ручки с серверным стримом
    /// </summary>
    /// <param name="count">Число контрактов</param>
    /// <returns>Коллекция контрактов через серверный стрим</returns>
    [HttpGet("{count:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<Sample>>> GetSamplesServerStream(int count)
    {
        logger.LogInformation("Calling gRPC client method {method} with parameters {params}", nameof(GetSamplesServerStream), count);
        try
        {
            var samples = new List<Sample>(count);
            using var call = client.GetSamplesServerStream(new GetSampleCountRequest { SampleCount = count });
            await foreach (var sample in call.ResponseStream.ReadAllAsync())
            {
                samples.Add(sample);
                logger.LogInformation("Received sample {count} from server stream with id {id}", samples.Count, sample.SampleId);
            }
            return Ok(samples);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSamplesServerStream));
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Вызов ручки с клиентским стримом
    /// </summary>
    /// <param name="count">Число контрактов</param>
    /// <returns>Коллекция контрактов внутри единственного ответа</returns>
    [HttpGet("{count:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<Sample>>> GetSamplesClientStream(int count)
    {
        logger.LogInformation("Calling gRPC client method {method} with parameters {params}", nameof(GetSamplesClientStream), count);
        try
        {
            var rand = new Random();
            using var call = client.GetSamplesClientStream(deadline: DateTime.UtcNow.AddSeconds(15));
            for (var i = 0; i < count; i++)
            {
                var sample = new GetSampleByIdRequest { SampleId = rand.Next(1, count) };
                await call.RequestStream.WriteAsync(sample);
                logger.LogInformation("Wrote request {count} to client stream with id {id}", i + 1, sample.SampleId);
            }
            await call.RequestStream.CompleteAsync();
            var response = await call.ResponseAsync;
            return Ok(response.Samples);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSamplesClientStream));
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Вызов ручки с двунаправленным стримом
    /// </summary>
    /// <param name="count">Число контрактов</param>
    /// <returns>Коллекция контрактов через серверный стрим</returns>
    [HttpGet("{count:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<Sample>>> GetSamplesBidirectionalStream(int count)
    {
        logger.LogInformation("Calling gRPC client method {method} with parameters {params}", nameof(GetSamplesBidirectionalStream), count);
        try
        {
            var samples = new List<Sample>(count);
            var rand = new Random();

            using var call = client.GetSamplesBidirectionalStream(deadline: DateTime.UtcNow.AddSeconds(15));
            for (var i = 0; i < count; i++)
            {
                var sample = new GetSampleByIdRequest { SampleId = rand.Next(1, count) };
                await call.RequestStream.WriteAsync(sample);
                logger.LogInformation("Wrote request {count} to client stream with id {id}", i + 1, sample.SampleId);
            }
            await call.RequestStream.CompleteAsync();

            await foreach (var sample in call.ResponseStream.ReadAllAsync())
            {
                samples.Add(sample);
                logger.LogInformation("Received sample {count} from server stream with id {id}", samples.Count, sample.SampleId);
            }
            return Ok(samples);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSamplesBidirectionalStream));
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
