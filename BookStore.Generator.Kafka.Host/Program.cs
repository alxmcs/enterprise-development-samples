using BookStore.Application.Contracts.BookAuthors;
using BookStore.Generator.Kafka.Host;
using BookStore.Generator.Kafka.Host.Serializers;
using BookStore.Generator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddKafkaProducer<Guid, IList<BookAuthorCreateUpdateDto>>(
    "book-store-kafka",
    builder => 
    {
        builder.SetKeySerializer(new BookStoreKeySerializer());
        builder.SetValueSerializer(new BookStoreValueSerializer());
    });
builder.Services.AddScoped<IProducerService, BookStoreKafkaProducer>();
builder.Services.AddHostedService<GeneratorService>();

var app = builder.Build();

app.MapDefaultEndpoints();

app.Run();
