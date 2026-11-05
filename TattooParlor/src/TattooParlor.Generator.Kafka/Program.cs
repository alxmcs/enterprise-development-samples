using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Generator;
using TattooParlor.Generator.Kafka.Infrastructure;
using TattooParlor.Generator.Kafka.Infrastructure.Serializers;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddGeneratorServices<TattooParlorKafkaProducer>();

var kafkaConnectionName = builder.Configuration.GetSection("Kafka")["ConnectionName"]
    ?? throw new KeyNotFoundException("ConnectionName section of Kafka is missing");

builder.AddKafkaProducer<int, IList<TattooSessionCreateUpdateDto>>(kafkaConnectionName, producerBuilder =>
    producerBuilder.SetKeySerializer(new TattooParlorKeySerializer())
        .SetValueSerializer(new TattooParlorValueSerializer()));

var app = builder.Build();

app.UseGeneratorServices();
app.MapDefaultEndpoints();

app.Run();
