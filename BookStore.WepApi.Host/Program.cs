using AutoMapper;
using BookStore.Application;
using BookStore.Application.Services;
using BookStore.Contracts;
using BookStore.Contracts.Author;
using BookStore.Contracts.Book;
using BookStore.Contracts.BookAuthor;
using BookStore.EfCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Serilog;
using Serilog.Events;

var logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {ClientIp}] {Message:lj}{NewLine}{Exception}")
        .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(logger);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(BookDto).Assembly.GetName().Name}.xml"));
});

var mapperConfig = new MapperConfiguration(config => config.AddProfile(new AutoMapperProfile()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddDbContextFactory<BookStoreDbContext>(options => options.UseLazyLoadingProxies().UseSqlite(builder.Configuration["ConnectionString"]));

builder.Services.AddScoped<IBookService,BookCrudService>();
builder.Services.AddScoped<IAuthorService, AuthorCrudService>();
builder.Services.AddScoped<ICrudService<BookAuthorDto, BookAuthorCreateUpdateDto, int>, BookAuthorCrudService>();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => { policy.AllowAnyOrigin(); policy.AllowAnyMethod(); policy.AllowAnyHeader(); }));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
