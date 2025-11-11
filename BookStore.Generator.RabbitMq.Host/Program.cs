using BookStore.Generator.RabbitMq.Host;
using BookStore.Generator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRabbitMQClient("bookstore-rabbitmq");
builder.Services.AddScoped<IProducerService, BookStoreRabbitMqProducer>();
builder.Services.AddHostedService<GeneratorService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
    .Where(a => a.GetName().Name!.StartsWith("BookStore"))
    .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            options.IncludeXmlComments(xmlPath);
    }
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
