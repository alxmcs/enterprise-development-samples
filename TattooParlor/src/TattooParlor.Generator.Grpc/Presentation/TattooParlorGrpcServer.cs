using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using TattooParlor.Application.Contracts.Protos;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Generator.Domain;

namespace TattooParlor.Generator.Grpc.Presentation;

/// <summary>
/// Имплементация gRPC серверной части службы для отправки контрактов
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="logger">Логгер</param>
public class TattooParlorGrpcServer(IConfiguration configuration, ILogger<TattooParlorGrpcServer> logger)
    : TattooSessionGrpcService.TattooSessionGrpcServiceBase
{
    private readonly string _batchSize = configuration.GetSection("Generator")["BatchSize"] ?? throw new KeyNotFoundException("BatchSize section of Generator is missing");
    private readonly string _payloadLimit = configuration.GetSection("Generator")["PayloadLimit"] ?? throw new KeyNotFoundException("PayloadLimit section of Generator is missing");
    private readonly string _waitTime = configuration.GetSection("Generator")["WaitTime"] ?? throw new KeyNotFoundException("WaitTime section of Generator is missing");

    /// <summary>
    /// Служба для отправки батчей контрактов клиенту при помощи серверного стриминга
    /// </summary>
    /// <param name="request">Запрос на начало передачи стрима от клиента</param>
    /// <param name="responseStream">Серверный стрим</param>
    /// <param name="context">Контекст вызова</param>
    /// <exception cref="FormatException">Если параметры передачи не парсятся</exception>
    public override async Task GetTattooSessionsStream(Empty request, IServerStreamWriter<TattooSessionResponse> responseStream, ServerCallContext context)
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
                var batch = SessionGenerator.GenerateLinks(batchSize);
                var payload = new TattooSessionResponse { IsFinal = counter >= payloadLimit };
                payload.TattooSessions.AddRange(batch.Select(Map));
                await responseStream.WriteAsync(payload);
                await Task.Delay(waitTime * 1000, context.CancellationToken);
            }
            catch (OperationCanceledException) when (context.CancellationToken.IsCancellationRequested)
            {
                logger.LogWarning("Cancellation requested from client {peer}", context.Peer);
                break;
            }
        }
    }

    /// <summary>
    /// Маппинг DTO контракта в protobuf-сообщение
    /// </summary>
    /// <param name="dto">DTO контракта</param>
    /// <returns>Protobuf-сообщение</returns>
    private static TattooSessionCreateUpdateMessage Map(TattooSessionCreateUpdateDto dto) =>
        new()
        {
            CustomerId = dto.CustomerId,
            TattooArtistId = dto.TattooArtistId,
            SessionDate = Timestamp.FromDateTime(dto.SessionDate.ToUniversalTime()),
            BodyZone = (BodyZone)(int)dto.BodyZone,
            Style = (TattooStyle)(int)dto.Style,
            HeightInCm = dto.HeightInCm,
            WidthInCm = dto.WidthInCm,
            IsMulticolor = dto.IsMulticolor,
            CancelDate = dto.CancelDate.HasValue ? Timestamp.FromDateTime(dto.CancelDate.Value.ToUniversalTime()) : null
        };
}
