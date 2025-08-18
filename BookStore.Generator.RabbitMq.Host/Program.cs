using BookStore.Generator.RabbitMq.Host;
using BookStore.Generator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRabbitMQClient("book-store-rabbitmq");
builder.Services.AddScoped<IProducerService, RabbitMqProducer>();
builder.Services.AddHostedService<GeneratorService>();

var app = builder.Build();
app.Run();
