using TattooParlor.Generator.Grpc.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapGrpcService<TattooParlorGrpcServer>();
app.Run();
