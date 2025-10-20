using GrpcExample.Protos;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.MapDefaultEndpoints();

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
});
builder.Services.AddGrpcClient<ExampleService.ExampleServiceClient>(options =>
{
    options.Address = new Uri("https://localhost:7133");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
