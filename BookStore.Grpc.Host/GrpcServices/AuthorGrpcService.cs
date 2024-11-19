using AutoMapper;
using BookStore.Contracts.Author;
using BookStore.Contracts.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static BookStore.Contracts.Protos.AuthorService;

namespace BookStore.Grpc.Host.GrpcServices;

public class AuthorGrpcService(IAuthorService crudService, ILogger<BookGrpcService> logger, IMapper mapper) : AuthorServiceBase
{
    public override async Task<AuthorResponse> Create(AuthorCreateRequest request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(Create), GetType().Name, request);
        try
        {
            var dto = mapper.Map<AuthorCreateUpdateDto>(request);
            var res = await crudService.Create(dto, context.CancellationToken);
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(Create), GetType().Name);
            return res == null ? new AuthorResponse() : mapper.Map<AuthorResponse>(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(Create), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<AuthorResponse> Update(AuthorUpdateRequest request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(Update), GetType().Name, request);
        try
        {
            var dto = mapper.Map<AuthorCreateUpdateDto>(request.Author);
            var res = await crudService.Update(request.Id, dto, context.CancellationToken);
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(Update), GetType().Name);
            return res == null ? new AuthorResponse() : mapper.Map<AuthorResponse>(res);
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

    public override async Task<AuthorListResponse> GetList(Empty request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(GetList), GetType().Name, request);
        try
        {
            var res = await crudService.GetList(context.CancellationToken);
            var response = new AuthorListResponse();
            if (res != null && res.Count > 0)
                response.Authors.AddRange(mapper.Map<IList<AuthorResponse>>(res));
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(GetList), GetType().Name);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(GetList), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<AuthorResponse> GetById(Int32Value request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(GetById), GetType().Name, request);
        try
        {
            var res = await crudService.GetById(request.Value, context.CancellationToken);
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(GetById), GetType().Name);
            return res == null ? new AuthorResponse() : mapper.Map<AuthorResponse>(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(GetById), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<AuthorListResponse> GetBookAuthors(Int32Value request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(GetBookAuthors), GetType().Name, request);
        try
        {
            var res = await crudService.GetBookAuthors(request.Value, context.CancellationToken);
            var response = new AuthorListResponse();
            if (res != null && res.Count > 0)
                response.Authors.AddRange(mapper.Map<IList<AuthorResponse>>(res));
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(GetBookAuthors), GetType().Name);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(GetBookAuthors), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }
}
