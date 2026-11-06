using Confluent.Kafka;
using TattooParlor.Api.Grpc;
using TattooParlor.Application.Contracts.Protos;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Infrastructure.Kafka;
using TattooParlor.Infrastructure.Nats;
using TattooParlor.Infrastructure.RabbitMq;

namespace TattooParlor.Api;

/// <summary>
/// Класс-расширение для регистрации в di-контейнере подходящего клиента для сервиса генерации
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    extension(WebApplicationBuilder builder)
    {
        /// <summary>
        /// Регистрирует клиент для взаимодейсвия с сервисом генерации данных
        /// </summary>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>Веб-билдер приложения с зареганными службами</returns>
        /// <exception cref="KeyNotFoundException">Если параметр конфигурации Generator не найден</exception>
        /// <exception cref="FormatException">Если параметр конфигурации Generator неизвестен</exception>
        public WebApplicationBuilder AddGeneratorService(IConfiguration configuration)
        {
            if (!configuration.GetSection("BusType").Exists()) throw new KeyNotFoundException("BusType");

            _ = configuration["BusType"] switch
            {
                "RabbitMQ" => AddRabbitMq(builder),
                "Kafka" => AddKafka(builder),
                "Nats" => AddNats(builder),
                "gRPC" => AddGrpc(builder),
                _ => throw new FormatException("Unknown parameter in BusType section")
            };
            return builder;
        }

        private WebApplicationBuilder AddRabbitMq()
        {
            builder.AddRabbitMQClient("tattooparlor-rabbitmq");
            builder.Services.AddHostedService<TattooParlorRabbitMqConsumer>();

            return builder;
        }
        private WebApplicationBuilder AddKafka()
        {
            builder.AddKafkaConsumer<int, IList<TattooSessionCreateUpdateDto>>("tattooparlor-kafka",
                settings =>
                {
                    settings.Config.GroupId = builder.Configuration["Kafka:ConsumerGroup"]
                        ?? throw new KeyNotFoundException("ConsumerGroup section of Kafka is missing.");
                    settings.Config.AutoOffsetReset = AutoOffsetReset.Earliest;
                },
                consumerBuilder => consumerBuilder
                    .SetKeyDeserializer(new TattooParlorKeyDeserializer())
                    .SetValueDeserializer(new TattooParlorValueDeserializer()));
            builder.Services.AddHostedService<TattooParlorKafkaConsumer>();

            return builder;
        }

        private WebApplicationBuilder AddNats()
        {
            builder.AddNatsClient("tattooparlor-nats");
            builder.Services.AddHostedService<TattooParlorNatsConsumer>();

            return builder;
        }

        private WebApplicationBuilder AddGrpc()
        {
            builder.Services.AddHostedService<TattooParlorGrpcClient>();
            builder.Services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = builder.Environment.IsDevelopment();
            });
            builder.Services.AddGrpcClient<TattooSessionGrpcService.TattooSessionGrpcServiceClient>(options =>
            {
                options.Address = new Uri("https://tattooparlor-generator-grpc:5000");
            });

            return builder;
        }
    }
}
