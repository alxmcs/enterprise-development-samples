using AutoMapper;
using BookStore.Contracts.Author;
using BookStore.Contracts.Book;
using BookStore.Contracts.BookAuthor;
using BookStore.Contracts.Protos;

namespace BookStore.Grpc.Client;

public class GrpcMappingProfile : Profile
{

    public GrpcMappingProfile()
    {
        CreateMap<BookCreateRequest, BookCreateUpdateDto>().ReverseMap();
        CreateMap<BookDto, BookResponse>().ReverseMap();

        CreateMap<AuthorCreateRequest, AuthorCreateUpdateDto>().ReverseMap();
        CreateMap<AuthorDto, AuthorResponse>().ReverseMap();

        CreateMap<BookAuthorCreateRequest, BookAuthorCreateUpdateDto>().ReverseMap();
        CreateMap<BookAuthorDto, BookAuthorResponse>().ReverseMap();
    }
}