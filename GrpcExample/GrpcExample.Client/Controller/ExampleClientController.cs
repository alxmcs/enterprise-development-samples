using Grpc.Core;
using GrpcExample.Protos;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;

namespace GrpcExample.Client.Controller;

[Route("api/[controller]/[action]")]
[ApiController]
public class ExampleClientController(ExampleService.ExampleServiceClient client, ILogger<ExampleClientController> logger): ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Sample>> GetSampleUnary(int id)
    {
        logger.LogInformation("Calling gRPC client method {method} with parameters {params}", nameof(GetSampleUnary), id);
        try
        {
            var result = await client.GetSampleUnaryAsync(new GetSampleByIdRequest { SampleId = id }, deadline: DateTime.Now.AddSeconds(15));
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSampleUnary));
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    [HttpGet("{count:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<Sample>>> GetSamplesUnary(int count)
    {
        logger.LogInformation("Calling gRPC client method {method} with parameters {params}", nameof(GetSamplesUnary), count);
        try
        {
            var result = await client.GetSamplesUnaryAsync(new GetSampleCountRequest { SampleCount = count }, deadline: DateTime.Now.AddSeconds(15));
            return Ok(result.Samples);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSamplesUnary));
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    [HttpGet("{count:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<Sample>>> GetSamplesServerStream(int count)
    {
        logger.LogInformation("Calling gRPC client method {method} with parameters {params}", nameof(GetSamplesServerStream), count);
        try
        {
            var samples = new List<Sample>();
            using var call = client.GetSamplesServerStream(new GetSampleCountRequest { SampleCount = count });
            await foreach (var sample in call.ResponseStream.ReadAllAsync())
                samples.Add(sample);
            return Ok(samples);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSamplesServerStream));
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    [HttpGet("{count:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<Sample>>> GetSamplesClientStream(int count)
    {
        logger.LogInformation("Calling gRPC client method {method} with parameters {params}", nameof(GetSamplesClientStream), count);
        try
        {
            var rand = new Random();
            using var call = client.GetSamplesClientStream(deadline: DateTime.Now.AddSeconds(15));
            for (var i = 0; i< count; i++)
                await call.RequestStream.WriteAsync(new GetSampleByIdRequest { SampleId = rand.Next() });
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

    [HttpGet("{count:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<Sample>>> GetSamplesBidirectionalStream(int count)
    {
        logger.LogInformation("Calling gRPC client method {method} with parameters {params}", nameof(GetSamplesBidirectionalStream), count);
        try
        {
            var samples = new List<Sample>();
            var rand = new Random();

            using var call = client.GetSamplesBidirectionalStream(deadline: DateTime.Now.AddSeconds(15));
            for (var i = 0; i < count; i++)
                await call.RequestStream.WriteAsync(new GetSampleByIdRequest { SampleId = rand.Next() });
            await call.RequestStream.CompleteAsync();

            await foreach (var sample in call.ResponseStream.ReadAllAsync())
                samples.Add(sample);
            return Ok(samples);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSamplesBidirectionalStream));
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
