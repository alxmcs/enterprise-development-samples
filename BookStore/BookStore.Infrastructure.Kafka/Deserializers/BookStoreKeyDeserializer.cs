using Confluent.Kafka;
using System.Text.Json;

namespace BookStore.Infrastructure.Kafka.Deserializers;

/// <summary>
/// Десериализатор для ключа пейлода Kafka
/// </summary>
public class BookStoreKeyDeserializer : IDeserializer<Guid>
{
    public Guid Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context) =>
        JsonSerializer.Deserialize<Guid>(data);
}
