using AutoMapper;
using BookStore.Contracts.Author;
using BookStore.Contracts.Book;
using BookStore.Contracts.BookAuthor;
using BookStore.Contracts.Protos;

namespace BookStore.Grpc.Host;

public class GrpcMappingProfile : Profile
{

    public GrpcMappingProfile()
    {
        CreateMap<BookCreateRequest, BookCreateUpdateDto>();
        CreateMap<BookDto, BookResponse>();

        CreateMap<AuthorCreateRequest, AuthorCreateUpdateDto>();
        CreateMap<AuthorDto, AuthorResponse>();

        CreateMap<BookAuthorCreateRequest, BookAuthorCreateUpdateDto>();
        CreateMap<BookAuthorDto, BookAuthorResponse>();
    }
}