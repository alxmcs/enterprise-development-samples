using Grpc.Core;
using GrpcExample.Protos;
using GrpcExample.Server.Utils;

namespace GrpcExample.Server.Grpc;
/// <summary>
/// Имплементация gRPC сервера
/// </summary>
/// <param name="logger">Логгер</param>
public class ExampleServer(ILogger<ExampleServer> logger) : ExampleService.ExampleServiceBase
{
    /// <summary>
    /// Пример унарной ручки, возвращающего контракт
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="context">Контекст вызова</param>
    /// <returns>Один контракт</returns>
    public override async Task<Sample> GetSampleUnary(GetSampleByIdRequest request, ServerCallContext context)
    {
        try
        {
            logger.LogInformation("Executing gRPC {name} method with {@request} parameter", nameof(GetSampleUnary), request);
            return await Task.FromResult(Generator.GenerateById(request.SampleId));
        }
        catch (OperationCanceledException)
        {
            logger.LogWarning("Cancellation requested for gRPC {name} method was requested by client {peer}", nameof(GetSampleUnary), context.Peer);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSampleUnary));
            throw;
        }
    }

    /// <summary>
    /// Пример унарной ручки, возвращающего контракт с повторяющимся полем
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="context">Контекст вызова</param>
    /// <returns>Коллекция контрактов внутри единственного ответа</returns>
    public override async Task<GetSamplesRepeatedResponse> GetSamplesUnary(GetSampleCountRequest request, ServerCallContext context)
    {
        try
        {
            logger.LogInformation("Executing gRPC {name} method with {@request} parameter", nameof(GetSamplesUnary), request);
            var result = new GetSamplesRepeatedResponse();
            result.Samples.AddRange(Generator.GenerateByCount(request.SampleCount));
            return await Task.FromResult(result);
        }
        catch (OperationCanceledException)
        {
            logger.LogWarning("Cancellation for gRPC {name} method was requested by client {peer}", nameof(GetSamplesUnary), context.Peer);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSamplesUnary));
            throw;
        }
    }

    /// <summary>
    /// Пример серверного стриминга - клиент посылает один запрос, сервер возвращает поток ответов
    /// Клиент инициирует обмен
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="responseStream">Стрим с ответами</param>
    /// <param name="context">Контекст вызова</param>
    public override async Task GetSamplesServerStream(GetSampleCountRequest request, IServerStreamWriter<Sample> responseStream, ServerCallContext context)
    {
        try
        {
            var count = 1;
            while (!context.CancellationToken.IsCancellationRequested && request.SampleCount >= count)
            {
                logger.LogInformation("Executing gRPC {name} server streaming method - {num} out of {count}", nameof(GetSamplesServerStream), count, request.SampleCount);
                await responseStream.WriteAsync(Generator.GenerateSingle());
                count++;
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogWarning("Cancellation for gRPC {name} method was requested by client {peer}", nameof(GetSamplesServerStream), context.Peer);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSamplesServerStream));
            throw;
        }
    }

    /// <summary>
    /// Пример клиенского стриминга - клиент посылает один запрос, сервер возвращает поток ответов
    /// Клиент инициирует обмен
    /// </summary>
    /// <param name="requestStream">Стрим запросов</param>
    /// <param name="context">Контекст вызова</param>
    /// <returns>Коллекция контрактов внутри единственного ответа</returns>
    public override async Task<GetSamplesRepeatedResponse> GetSamplesClientStream(IAsyncStreamReader<GetSampleByIdRequest> requestStream, ServerCallContext context)
    {
        try
        {
            var samples = new List<Sample>();
            try
            {
                await foreach (var request in requestStream.ReadAllAsync(context.CancellationToken))
                {
                    logger.LogInformation("Executing gRPC {name} client streaming method - received a message", nameof(GetSamplesClientStream));
                    samples.Add(Generator.GenerateById(request.SampleId));
                }
            }
            catch (OperationCanceledException)
            {
                logger.LogWarning("Cancellation for gRPC {name} method was requested by client {peer}", nameof(GetSamplesClientStream), context.Peer);
            }
            var result = new GetSamplesRepeatedResponse();
            result.Samples.AddRange(samples);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSamplesClientStream));
            throw;
        }
    }

    /// <summary>
    /// Пример двунаправленного стримиинга - клиент и сервер посылают друг другу по потоку сообщений
    /// Инициировать обмен может любой участник
    /// </summary>
    /// <param name="requestStream">Стрим запросов</param>
    /// <param name="responseStream">Стрим с ответами</param>
    /// <param name="context">Контекст вызова</param>
    public override async Task GetSamplesBidirectionalStream(IAsyncStreamReader<GetSampleByIdRequest> requestStream, IServerStreamWriter<Sample> responseStream, ServerCallContext context)
    {
        try
        {
            await foreach (var request in requestStream.ReadAllAsync(context.CancellationToken))
            {
                logger.LogInformation("Executing gRPC {name} bidirectional streaming method - received a message", nameof(GetSamplesBidirectionalStream));
                await responseStream.WriteAsync(Generator.GenerateById(request.SampleId));
                logger.LogInformation("Executing gRPC {name} bidirectional streaming method - sent a response", nameof(GetSamplesBidirectionalStream));
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogWarning("Cancellation for gRPC {name} method was requested by client {peer}", nameof(GetSamplesBidirectionalStream), context.Peer);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured during gRPC {name} method", nameof(GetSamplesBidirectionalStream));
            throw;
        }
    }
}
