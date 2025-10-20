using BookStore.Generator.Nats.Host;
using BookStore.Generator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddNatsClient("bookstore-nats");
builder.Services.AddScoped<IProducerService, BookStoreNatsProducer>();
builder.Services.AddHostedService<GeneratorService>();

var app = builder.Build();
app.Run();
