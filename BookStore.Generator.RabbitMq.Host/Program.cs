using BookStore.Generator.RabbitMq.Host;
using BookStore.Generator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRabbitMQClient("bookstore-rabbitmq");
builder.Services.AddScoped<IProducerService, BookStoreRabbitMqProducer>();
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
