using TattooParlor.Generator;
using TattooParlor.Generator.Rabbitmq.Infractructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddGeneratorServices<TattooParlorRabbitMqProducer>();
builder.AddRabbitMQClient("tattooparlor-rabbitmq");

var app = builder.Build();

app.UseGeneratorServices();
app.MapDefaultEndpoints();
app.Run();
