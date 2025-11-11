using AutoMapper;
using BookStore.Api.Host;
using BookStore.Application.Contracts;
using BookStore.Application.Contracts.Authors;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Application.Contracts.Books;
using BookStore.Application.Services;
using BookStore.Domain;
using BookStore.Domain.Model.Authors;
using BookStore.Domain.Model.BookAuthors;
using BookStore.Domain.Model.Books;
using BookStore.Infrastructure.EfCore;
using BookStore.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//службы Aspire
builder.AddServiceDefaults();
//автомаппер
var mapperConfig = new MapperConfiguration(
    config => config.AddMaps([typeof(Program).Assembly, typeof(AuthorService).Assembly]),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//службы инфраструктурного слоя
builder.Services.AddTransient<IRepository<Author, int>, AuthorEfCoreRepository>();
builder.Services.AddTransient<IRepository<Book, int>, BookEfCoreRepository>();
builder.Services.AddTransient<IRepository<BookAuthor, int>, BookAuthorEfCoreRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookAuthorService, BookAuthorService>();
//службы доменного слоя
builder.Services.AddScoped<IAuthorManager, AuthorManager>();
//контроллеры презентационного слоя
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

builder.AddNpgsqlDbContext<BookStoreDbContext>("Database", configureDbContextOptions: builder => builder.UseLazyLoadingProxies());
//клиент сервиса генерации данных
builder.AddGeneratorService(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();
    db.Database.Migrate();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
