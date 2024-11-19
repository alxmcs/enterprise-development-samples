using AutoMapper;
using BookStore.Contracts.Author;
using BookStore.Contracts.Book;
using BookStore.Contracts.BookAuthor;
using BookStore.Domain.Model;

namespace BookStore.Application;
public class ContractsMappingProfile : Profile
{
    public ContractsMappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorCreateUpdateDto, Author>();

        CreateMap<Book, BookDto>();
        CreateMap<BookCreateUpdateDto, Book>();

        CreateMap<BookAuthor, BookAuthorDto>();
        CreateMap<BookAuthorCreateUpdateDto, BookAuthor>();
    }
}
