using AutoMapper;
using BookStore.Generator.Grpc.Host;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
var mapperConfig = new MapperConfiguration(
    config => config.AddMaps([typeof(Program).Assembly]),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
});

var app = builder.Build();
app.MapDefaultEndpoints();
app.MapGrpcService<BookStoreGrpcGeneratorService>();
app.Run();
