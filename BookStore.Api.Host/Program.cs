using AutoMapper;
using BookStore.Application;
using BookStore.Application.Contracts;
using BookStore.Application.Contracts.Authors;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Application.Contracts.Books;
using BookStore.Application.Services;
using BookStore.Domain.Model.Authors;
using BookStore.Domain.Model.BookAuthors;
using BookStore.Domain.Model.Books;
using BookStore.Infrastructure.InMemory;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new BookStoreProfile()), 
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSingleton<IRepository<Author, int>, AuthorInMemoryRepository>();
builder.Services.AddSingleton<IRepository<Book, int>, BookInMemoryRepository>();
builder.Services.AddSingleton<IRepository<BookAuthor, int>, BookAuthorInMemoryRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IApplicationService<BookAuthorDto, BookAuthorCreateUpdateDto, int>, BookAuthorService>();

builder.Services.AddScoped<AuthorManager>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    var assembly = Assembly.GetExecutingAssembly();
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml"));
    foreach (var refAssembly in assembly.GetReferencedAssemblies())
    {
        if (refAssembly.Name!.StartsWith("System.") || refAssembly.Name.StartsWith("Microsoft."))
            continue;

        var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{refAssembly.Name}.xml");
        if (File.Exists(xmlPath))
            options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
