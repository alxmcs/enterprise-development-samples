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
using BookStore.Infrastructure.EfCore;
using BookStore.Infrastructure.EfCore.Repositories;
using BookStore.Infrastructure.RabbitMq;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//������ Aspire
builder.AddServiceDefaults();
//����������
var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new BookStoreProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//������ ����������������� ����
builder.Services.AddTransient<IRepository<Author, int>, AuthorEfCoreRepository>();
builder.Services.AddTransient<IRepository<Book, int>, BookEfCoreRepository>();
builder.Services.AddTransient<IRepository<BookAuthor, int>, BookAuthorEfCoreRepository>();
builder.Services.AddHostedService<RabbitMqConsumer>();
//������ ����������� ����
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IApplicationService<BookDto, BookCreateUpdateDto, int>, BookService>();
builder.Services.AddScoped<IBookAuthorService, BookAuthorService>();
//������ ��������� ����
builder.Services.AddScoped<AuthorManager>();
//����������� ���������������� ����
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//�������� ���� ������
builder.AddNpgsqlDbContext<BookStoreDbContext>("Database", configureDbContextOptions: builder => builder.UseLazyLoadingProxies());
//������ ������� ���������
builder.AddRabbitMQClient("book-store-rabbitmq");

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
