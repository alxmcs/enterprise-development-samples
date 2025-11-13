using AutoMapper;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Application.Contracts.Protos;

namespace BookStore.Generator.Grpc.Host;

/// <summary>
/// Профайл для маппинга gRPC контрактов
/// </summary>
public class BookStoreGrpcProfile : Profile
{
    /// <inheritdoc/>
    public BookStoreGrpcProfile()
    {
        CreateMap<BookAuthorCreateUpdateDto, BookAuthorResponse>();
    }
}
