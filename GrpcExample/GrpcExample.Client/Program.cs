using Grpc.Net.Client;
using GrpcExample.Protos;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(serviceProvider =>
{
    var grpcServiceUrl = "https://localhost:7133";
    var httpHandler = new HttpClientHandler();

    if (builder.Environment.IsDevelopment())
    {
        httpHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
    }

    var channel = GrpcChannel.ForAddress(grpcServiceUrl, new GrpcChannelOptions
    {
        HttpHandler = httpHandler,
        DisposeHttpClient = true
    });

    return new ExampleService.ExampleServiceClient(channel);
});

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
