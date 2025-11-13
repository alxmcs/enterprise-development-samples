using Microsoft.Extensions.Hosting;
namespace BookStore.AppHost;
internal static class AppHostExtensions
{
    private const string RabbitMq = "RabbitMq";
    private const string Kafka = "Kafka";
    private const string Nats = "Nats";
    private const string Grpc = "Grpc";

    public static bool IsRabbitMq(this IHostEnvironment hostEnvironment)
    {
        ArgumentNullException.ThrowIfNull(hostEnvironment);
        return hostEnvironment.IsEnvironment(RabbitMq);
    }

    public static bool IsKafka(this IHostEnvironment hostEnvironment)
    {
        ArgumentNullException.ThrowIfNull(hostEnvironment);
        return hostEnvironment.IsEnvironment(Kafka);
    }

    public static bool IsNats(this IHostEnvironment hostEnvironment)
    {
        ArgumentNullException.ThrowIfNull(hostEnvironment);
        return hostEnvironment.IsEnvironment(Nats);
    }

    public static bool IsGrpc(this IHostEnvironment hostEnvironment)
    {
        ArgumentNullException.ThrowIfNull(hostEnvironment);
        return hostEnvironment.IsEnvironment(Grpc);
    }
}
