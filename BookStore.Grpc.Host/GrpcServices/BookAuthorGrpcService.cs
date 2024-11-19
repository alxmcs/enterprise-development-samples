using BookStore.Contracts.Protos;
using BookStore.Contracts;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using AutoMapper;
using static BookStore.Contracts.Protos.BookAuthorService;
using BookStore.Contracts.BookAuthor;

namespace BookStore.Grpc.Host.GrpcServices;

public class BookAuthorGrpcService(ICrudService<BookAuthorDto, BookAuthorCreateUpdateDto, int> crudService, ILogger<BookGrpcService> logger, IMapper mapper) : BookAuthorServiceBase
{
    public override async Task<BookAuthorResponse> Create(BookAuthorCreateRequest request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(Create), GetType().Name, request);
        try
        {
            var dto = mapper.Map<BookAuthorCreateUpdateDto>(request);
            var res = await crudService.Create(dto, context.CancellationToken);
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(Create), GetType().Name);
            return res == null ? new BookAuthorResponse() : mapper.Map<BookAuthorResponse>(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(Create), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<BookAuthorResponse> Update(BookAuthorUpdateRequest request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(Update), GetType().Name, request);
        try
        {
            var dto = mapper.Map<BookAuthorCreateUpdateDto>(request.BookAuthor);
            var res = await crudService.Update(request.Id, dto, context.CancellationToken);
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(Update), GetType().Name);
            return res == null ? new BookAuthorResponse() : mapper.Map<BookAuthorResponse>(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(Update), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<BoolValue> Delete(Int32Value request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(Delete), GetType().Name, request);
        try
        {
            var res = await crudService.Delete(request.Value, context.CancellationToken);
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(Delete), GetType().Name);
            return new BoolValue { Value = res };
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(Delete), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<BookAuthorListResponse> GetList(Empty request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(GetList), GetType().Name, request);
        try
        {
            var res = await crudService.GetList(context.CancellationToken);
            var response = new BookAuthorListResponse();
            if (res != null && res.Count > 0)
                response.BookAuthors.AddRange(mapper.Map<IList<BookAuthorResponse>>(res));
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(GetList), GetType().Name);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(GetList), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<BookAuthorResponse> GetById(Int32Value request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(GetById), GetType().Name, request);
        try
        {
            var res = await crudService.GetById(request.Value, context.CancellationToken);
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(GetById), GetType().Name);
            return res == null ? new BookAuthorResponse() : mapper.Map<BookAuthorResponse>(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(GetById), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }
}
