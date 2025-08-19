using BookStore.Application.Contracts.BookAuthors;
using BookStore.Infrastructure.Kafka;
using BookStore.Infrastructure.Kafka.Deserializers;
using BookStore.Infrastructure.RabbitMq;

namespace BookStore.Api.Host;
/// <summary>
/// Класс-расширение для регистрации в di-контейнере подходящего брокара сообщений
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Регистрирует клиент брокера сообщений и необходимые для его работы службы
    /// </summary>
    /// <param name="builder">Веб-билдер приложения</param>
    /// <param name="configuration">Конфигурация</param>
    /// <returns>Веб-билдер приложения с зареганными службами</returns>
    /// <exception cref="ArgumentNullException">Если параметр конфигурации MessageBroker не найден</exception>
    /// <exception cref="FormatException">Если параметр конфигурации MessageBroker неизвестен</exception>
    public static WebApplicationBuilder AddMessageBroker(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        if (!configuration.GetSection("MessageBroker").Exists()) throw new ArgumentNullException("MessageBroker", "MessageBroker section is missing");

        _ = configuration["MessageBroker"] switch
        {
            "RabbitMq" => AddRabbitMq(builder),
            "Kafka" => AddKafka(builder),
            "Nats" => AddNats(builder),
            _ => throw new FormatException("Unknown parameter in MessageBroker section")
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
        builder.AddRabbitMQClient("book-store-rabbitmq");
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
        builder.AddKafkaConsumer<Guid, IList<BookAuthorCreateUpdateDto>>("book-store-kafka",
            configureBuilder: builder =>
            {
                builder.SetKeyDeserializer(new BookStoreKeyDeserializer());
                builder.SetValueDeserializer(new BookStoreValueDeserializer());
            },
            configureSettings: settings =>
            { 
                settings.Config.GroupId = "book-store-consumer";
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
        return builder;
    }
}
