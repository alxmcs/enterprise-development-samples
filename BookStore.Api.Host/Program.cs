using AutoMapper;
using BookStore.Application;
using BookStore.Application.Contracts;
using BookStore.Application.Contracts.Authors;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Application.Contracts.Books;
using BookStore.Application.Services;
using BookStore.Domain;
using BookStore.Domain.Model.Authors;
using BookStore.Domain.Model.BookAuthors;
using BookStore.Domain.Model.Books;
using BookStore.Infrastructure.EfCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new BookStoreProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSingleton<IRepository<Author, int>, AuthorEfCoreRepository>();
builder.Services.AddSingleton<IRepository<Book, int>, BookEfCoreRepository>();
builder.Services.AddSingleton<IRepository<BookAuthor, int>, BookAuthorEfCoreRepository>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IApplicationService<BookDto, BookCreateUpdateDto, int>, BookService>();
builder.Services.AddScoped<IApplicationService<BookAuthorDto, BookAuthorCreateUpdateDto, int>, BookAuthorService>();

builder.Services.AddScoped<AuthorManager>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
