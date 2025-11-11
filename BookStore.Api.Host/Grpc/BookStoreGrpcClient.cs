using AutoMapper;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Application.Contracts.Protos;
using Grpc.Core;

namespace BookStore.Api.Host.Grpc;

public class BookStoreGrpcClient(BookAuthorGrpcService.BookAuthorGrpcServiceClient client, IServiceScopeFactory scopeFactory, IMapper mapper, ILogger<BookStoreGrpcClient> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var ctx = new CancellationTokenSource();
        while (!ctx.Token.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation("Connecting to gRPC server stream");

                using var call = client.BookAuthorGetStream(new(), cancellationToken: ctx.Token);
                await foreach (var response in call.ResponseStream.ReadAllAsync(cancellationToken: ctx.Token))
                {
                    var contracts = mapper.Map<IList<BookAuthorCreateUpdateDto>>(response.BookAuthors.ToList());

                    using var scope = scopeFactory.CreateScope();
                    var bookAuthorService = scope.ServiceProvider.GetRequiredService<IBookAuthorService>();
                    await bookAuthorService.ReceiveContractList(contracts);

                    logger.LogInformation("Successfully received {count} contracts from gRPC stream message", contracts.Count);
                    if (response.IsFinal)
                    {
                        ctx.Cancel();
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
                logger.LogError(ex, "Unexpected exception occured during receiving contracts from gRPC stream");
                break;
            }
        }
    }
}

