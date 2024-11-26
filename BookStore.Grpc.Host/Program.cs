using AutoMapper;
using BookStore.Application;
using BookStore.Application.Services;
using BookStore.Contracts;
using BookStore.Contracts.Author;
using BookStore.Contracts.Book;
using BookStore.Contracts.BookAuthor;
using BookStore.EfCore;
using BookStore.Grpc.Host;
using BookStore.Grpc.Host.GrpcServices;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {ClientIp}] {Message:lj}{NewLine}{Exception}")
        .WriteTo.LogstashHttp(builder.Configuration["Logstash:Url"])
        .CreateLogger();

builder.Services.AddSerilog(logger);
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
});
builder.Services.AddControllers();
builder.Services.AddGrpcReflection();

builder.Services.AddDbContextFactory<BookStoreDbContext>(options => options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration["ConnectionString"]));

builder.Services.AddScoped<IBookService, BookCrudService>();
builder.Services.AddScoped<IAuthorService, AuthorCrudService>();
builder.Services.AddScoped<ICrudService<BookAuthorDto, BookAuthorCreateUpdateDto, int>, BookAuthorCrudService>();

var mapperConfig = new MapperConfiguration(config => config.AddProfiles([new ContractsMappingProfile(), new GrpcMappingProfile()]));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddCors(options => options.AddPolicy("AllowAll", policy => { policy.AllowAnyOrigin(); policy.AllowAnyMethod(); policy.AllowAnyHeader(); }));

var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.UseGrpcWeb();
app.UseCors();
app.UseAuthorization();
app.MapGrpcService<BookGrpcService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<AuthorGrpcService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcService<BookAuthorGrpcService>().EnableGrpcWeb().RequireCors("AllowAll");
app.MapGrpcReflectionService();
app.MapControllers();
app.Run();
