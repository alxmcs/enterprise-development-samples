using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using TattooParlor.Application.Contracts.Protos;
using TattooParlor.Application.Contracts.TattooSessions;

namespace TattooParlor.Api.Grpc;

/// <summary>
/// Клиентская gRPC-служба для приёма контрактов
/// </summary>
/// <param name="client">gRPC-клиент</param>
/// <param name="scopeFactory">Фабрика контекста</param>
/// <param name="logger">Логгер</param>
public class TattooParlorGrpcClient(
    TattooSessionGrpcService.TattooSessionGrpcServiceClient client,
    IServiceScopeFactory scopeFactory,
    ILogger<TattooParlorGrpcClient> logger) : BackgroundService
{
    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var ctx = new CancellationTokenSource();
        while (!ctx.Token.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation("Connecting to gRPC server stream");

                using var call = client.GetTattooSessionsStream(new Empty(), cancellationToken: ctx.Token);
                await foreach (var response in call.ResponseStream.ReadAllAsync(cancellationToken: ctx.Token))
                {
                    var batch = response.TattooSessions.Select(Map).ToList();

                    using var scope = scopeFactory.CreateScope();
                    var sessionService = scope.ServiceProvider.GetRequiredService<ITattooSessionService>();
                    await sessionService.ReceiveSessions(batch, ctx.Token);

                    logger.LogInformation("Successfully received {count} contracts from gRPC stream message", batch.Count);
                    if (response.IsFinal)
                    {
                        await ctx.CancelAsync();
                        call.Dispose();
                        break;
                    }
                }
                logger.LogInformation("Finished receiving messages from gRPC server stream");
            }
            catch (RpcException ex)
            {
                logger.LogError(ex, "Stream error: {code} - {status}", ex.StatusCode, ex.Status);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected exception occurred during receiving contracts from gRPC stream");
                break;
            }
        }
    }

    /// <summary>
    /// Маппинг protobuf-сообщения в DTO контракта
    /// </summary>
    /// <param name="message">Protobuf-сообщение</param>
    /// <returns>DTO контракта</returns>
    private static TattooSessionCreateUpdateDto Map(TattooSessionCreateUpdateMessage message) =>
        new(
            message.CustomerId,
            message.TattooArtistId,
            DateTime.SpecifyKind(message.SessionDate.ToDateTime(), DateTimeKind.Unspecified),
            (Domain.Shared.Enums.BodyZone)(int)message.BodyZone,
            (Domain.Shared.Enums.TattooStyle)(int)message.Style,
            message.HeightInCm,
            message.WidthInCm,
            message.IsMulticolor,
            message.CancelDate is null ? null : DateTime.SpecifyKind(message.CancelDate.ToDateTime(), DateTimeKind.Unspecified)
        );
}
