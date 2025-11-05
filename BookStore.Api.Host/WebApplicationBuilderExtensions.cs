using BookStore.Api.Host.Grpc;
using BookStore.Application.Contracts.BookAuthors;
using BookStore.Application.Contracts.Protos;
using BookStore.Infrastructure.Kafka;
using BookStore.Infrastructure.Kafka.Deserializers;
using BookStore.Infrastructure.Nats;
using BookStore.Infrastructure.RabbitMq;

namespace BookStore.Api.Host;
/// <summary>
/// Класс-расширение для регистрации в di-контейнере подходящего клиента для сервиса генерации
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Регистрирует клиент для взаимодейсвия с сервисом генерации данных
    /// </summary>
    /// <param name="builder">Веб-билдер приложения</param>
    /// <param name="configuration">Конфигурация</param>
    /// <returns>Веб-билдер приложения с зареганными службами</returns>
    /// <exception cref="ArgumentNullException">Если параметр конфигурации Generator не найден</exception>
    /// <exception cref="FormatException">Если параметр конфигурации Generator неизвестен</exception>
    public static WebApplicationBuilder AddGeneratorService(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        if (!configuration.GetSection("Generator").Exists()) throw new ArgumentNullException("Generator", "Generator section is missing");

        _ = configuration["Generator"] switch
        {
            "RabbitMq" => AddRabbitMq(builder),
            "Kafka" => AddKafka(builder),
            "Nats" => AddNats(builder),
            "Grpc" => AddGrpc(builder),
            _ => throw new FormatException("Unknown parameter in Generator section")
        };
        return builder;
    }

    /// <summary>
    /// Регистрирует клиент брокера сообщений и необходимые для его работы службы
    /// </summary>
    /// <param name="builder">Веб-билдер приложения</param>
    /// <returns>Веб-билдер приложения с зареганными службами RabbitMq</returns>
    private static WebApplicationBuilder AddRabbitMq(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<BookStoreRabbitMqConsumer>();
        builder.AddRabbitMQClient("bookstore-rabbitmq");
        return builder;
    }

    /// <summary>
    /// Регистрирует клиент брокера сообщений Kafka и необходимые для его работы службы
    /// </summary>
    /// <param name="builder">Веб-билдер приложения</param>
    /// <returns>Веб-билдер приложения с зареганными службами Kafka</returns>
    private static WebApplicationBuilder AddKafka(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<BookStoreKafkaConsumer>();
        builder.AddKafkaConsumer<Guid, IList<BookAuthorCreateUpdateDto>>("bookstore-kafka",
            configureBuilder: builder =>
            {
                builder.SetKeyDeserializer(new BookStoreKeyDeserializer());
                builder.SetValueDeserializer(new BookStoreValueDeserializer());
            },
            configureSettings: settings =>
            { 
                settings.Config.GroupId = "bookstore-consumer";
                settings.Config.AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
            }
            );
        return builder;
    }

    /// <summary>
    /// Регистрирует клиент брокера сообщений Nats и необходимые для его работы службы
    /// </summary>
    /// <param name="builder">Веб-билдер приложения</param>
    /// <returns>Веб-билдер приложения с зареганными службами Nats</returns>
    private static WebApplicationBuilder AddNats(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<BookStoreNatsConsumer>();
        builder.AddNatsClient("bookstore-nats");
        return builder;
    }

    /// <summary>
    /// Регистрирует клиент gRPC и необходимые для его работы службы
    /// </summary>
    /// <param name="builder">Веб-билдер приложения</param>
    /// <returns>Веб-билдер приложения с зареганным клиентом gRPC</returns>
    private static WebApplicationBuilder AddGrpc(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<BookStoreGrpcClient>();
        builder.Services.AddGrpc(options => 
        {
            options.EnableDetailedErrors = builder.Environment.IsDevelopment();
        });
        builder.Services.AddGrpcClient<BookAuthorGrpcService.BookAuthorGrpcServiceClient>(options =>
        {
            options.Address = new Uri("https://bookstore-generator-grpc-host:5000");
        });
        return builder;
    }
}
