using BookStore.Application.Contracts.BookAuthors;
using BookStore.Generator.Kafka.Host;
using BookStore.Generator.Kafka.Host.Serializers;
using BookStore.Generator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddKafkaProducer<Guid, IList<BookAuthorCreateUpdateDto>>(
    "bookstore-kafka",
    builder => 
    {
        builder.SetKeySerializer(new BookStoreKeySerializer());
        builder.SetValueSerializer(new BookStoreValueSerializer());
    });
builder.Services.AddScoped<IProducerService, BookStoreKafkaProducer>();
builder.Services.AddHostedService<GeneratorService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

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
