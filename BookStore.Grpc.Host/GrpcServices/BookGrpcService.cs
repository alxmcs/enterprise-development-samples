using AutoMapper;
using BookStore.Contracts.Book;
using BookStore.Contracts.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static BookStore.Contracts.Protos.BookService;

namespace BookStore.Grpc.Host.GrpcServices;

public class BookGrpcService(IBookService crudService, ILogger<BookGrpcService> logger, IMapper mapper) : BookServiceBase
{
    public override async Task<BookResponse> Create(BookCreateRequest request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(Create), GetType().Name, request);
        try
        {
            var dto = mapper.Map<BookCreateUpdateDto>(request);
            var res = await crudService.Create(dto, context.CancellationToken);
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(Create), GetType().Name);
            return res == null? new BookResponse(): mapper.Map<BookResponse>(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(Create), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<BookResponse> Update(BookUpdateRequest request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(Update), GetType().Name, request);
        try
        {
            var dto = mapper.Map<BookCreateUpdateDto>(request.Book);
            var res = await crudService.Update(request.Id, dto, context.CancellationToken);
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(Update), GetType().Name);
            return res == null ? new BookResponse() : mapper.Map<BookResponse>(res);
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

    public override async Task<BookListResponse> GetList(Empty request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(GetList), GetType().Name, request);
        try
        {
            var res = await crudService.GetList(context.CancellationToken);
            var response = new BookListResponse();
            if (res !=null && res.Count > 0)
                response.Books.AddRange(mapper.Map<IList<BookResponse>>(res));
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(GetList), GetType().Name);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(GetList), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<BookResponse> GetById(Int32Value request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(GetById), GetType().Name, request);
        try
        {
            var res = await crudService.GetById(request.Value, context.CancellationToken);
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(GetById), GetType().Name);
            return res == null ? new BookResponse() : mapper.Map<BookResponse>(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(GetById), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<BookListResponse> GetAuthorBooks(Int32Value request, ServerCallContext context)
    {
        logger.LogInformation("{method} method of {gpcService} is called with {@request} parameter", nameof(GetAuthorBooks), GetType().Name, request);
        try
        {
            var res = await crudService.GetAuthorBooks(request.Value, context.CancellationToken);
            var response = new BookListResponse();
            if (res != null && res.Count > 0)
                response.Books.AddRange(mapper.Map<IList<BookResponse>>(res));
            logger.LogInformation("{method} method of {gpcService} executed successfully", nameof(GetAuthorBooks), GetType().Name);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {gpcService}: {@exception}", nameof(GetAuthorBooks), GetType().Name, ex);
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }
}
