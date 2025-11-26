using AutoMapper;
using BookStore.Application.Contracts.Authors;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Application.Contracts.Books;
using BookStore.Domain.Model.Authors;
using BookStore.Domain.Model.BookAuthors;
using BookStore.Domain.Model.Books;

namespace BookStore.Application;

/// <summary>
/// Конфигурация для маппинга контрактов и доменных сущностей
/// </summary>
public class BookStoreProfile : Profile
{
    /// <inheritdoc/>
    public BookStoreProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<AuthorCreateUpdateDto, Author>();

        CreateMap<Book, BookDto>();
        CreateMap<BookCreateUpdateDto, Book>();

        CreateMap<BookAuthor, BookAuthorDto>();
        CreateMap<BookAuthorCreateUpdateDto, BookAuthor>();
    }
}
