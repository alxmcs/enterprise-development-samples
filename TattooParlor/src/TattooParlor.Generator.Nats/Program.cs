using TattooParlor.Generator;
using TattooParlor.Generator.Nats.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddGeneratorServices<TattooParlorNatsProducer>();
builder.AddNatsClient("tattooparlor-nats");

var app = builder.Build();

app.UseGeneratorServices();
app.MapDefaultEndpoints();
app.Run();
