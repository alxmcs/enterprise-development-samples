using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Generator;
using TattooParlor.Generator.Kafka.Infrastructure;
using TattooParlor.Generator.Kafka.Infrastructure.Serializers;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddGeneratorServices<TattooParlorKafkaProducer>();
builder.AddKafkaProducer<int, IList<TattooSessionCreateUpdateDto>>("tattooparlor-kafka", producerBuilder =>
    producerBuilder.SetKeySerializer(new TattooParlorKeySerializer())
        .SetValueSerializer(new TattooParlorValueSerializer()));

var app = builder.Build();

app.UseGeneratorServices();
app.MapDefaultEndpoints();
app.Run();
