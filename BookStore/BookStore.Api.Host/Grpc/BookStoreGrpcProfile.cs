using AutoMapper;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Application.Contracts.Protos;

namespace BookStore.Api.Host.Grpc;

public class BookStoreGrpcProfile : Profile
{
    public BookStoreGrpcProfile()
    {
        CreateMap<BookAuthorResponse, BookAuthorCreateUpdateDto>();
    }
}
