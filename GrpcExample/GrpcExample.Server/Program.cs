using GrpcExample.Server.Grpc;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
});

var app = builder.Build();
app.MapDefaultEndpoints();
app.UseHttpsRedirection();
app.MapGrpcService<ExampleServer>();
app.Run();
