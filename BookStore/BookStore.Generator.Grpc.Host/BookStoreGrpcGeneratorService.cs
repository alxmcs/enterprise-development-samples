using AutoMapper;
using BookStore.Application.Contracts.Protos;
using BookStore.Generator.Generator;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace BookStore.Generator.Grpc.Host;

/// <summary>
/// Имплементация gRPC серверной части службы для отправки контрактов
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="mapper">Профиль маппинга</param>
/// <param name="logger">Логгер</param>
public class BookStoreGrpcGeneratorService(IConfiguration configuration, IMapper mapper, ILogger<BookStoreGrpcGeneratorService> logger) : BookAuthorGrpcService.BookAuthorGrpcServiceBase
{
    private readonly string _batchSize = configuration.GetSection("Generator")["BatchSize"] ?? throw new KeyNotFoundException("BatchSize section of Generator is missing");
    private readonly string _payloadLimit = configuration.GetSection("Generator")["PayloadLimit"] ?? throw new KeyNotFoundException("PayloadLimit section of Generator is missing");
    private readonly string _waitTime = configuration.GetSection("Generator")["WaitTime"] ?? throw new KeyNotFoundException("WaitTime section of Generator is missing");

    /// <summary>
    /// Служба для отправки батча контрактов ан клиент при помощи серверного стриминга
    /// </summary>
    /// <param name="request">Запрос на начало передачи стрима от клиента</param>
    /// <param name="responseStream">Серверный стрим</param>
    /// <param name="context">Контекст вызова</param>
    /// <exception cref="FormatException">Если параметры передачи не парсятся</exception>
    public override async Task BookAuthorGetStream(Empty request, IServerStreamWriter<BookAuthorListResponse> responseStream, ServerCallContext context)
    {
        logger.LogInformation("Starting to send {total} messages with {time}s interval with {batch} messages in batch", _payloadLimit, _waitTime, _batchSize);

        if (!int.TryParse(_batchSize, out var batchSize)) throw new FormatException("Unable to parse BatchSize");
        if (!int.TryParse(_payloadLimit, out var payloadLimit)) throw new FormatException("Unable to parse PayloadLimit");
        if (!int.TryParse(_waitTime, out var waitTime)) throw new FormatException("Unable to parse WaitTime");

        var counter = 0;
        while (counter < payloadLimit && !context.CancellationToken.IsCancellationRequested)
        {
            try
            {
                counter += batchSize;
                var batch = BookAuthorGenerator.GenerateLinks(batchSize);
                var payload = new BookAuthorListResponse();
                payload.BookAuthors.AddRange(mapper.Map<IList<BookAuthorResponse>>(batch));
                payload.IsFinal = counter>=payloadLimit;
                await responseStream.WriteAsync(payload);
                await Task.Delay(waitTime * 1000, context.CancellationToken);        
            }
            catch(TaskCanceledException c)
            {
                logger.LogWarning(c, "Cancellation requested from client {peer}", context.Peer);
                break;
            }
        }
    }
}