using Confluent.Kafka;
using System.Text.Json;

namespace BookStore.Generator.Kafka.Host.Serializers;

/// <summary>
/// Сериализатор для ключа пейлода Kafka
/// </summary>
public class BookStoreKeySerializer : ISerializer<Guid>
{
    public byte[] Serialize(Guid data, SerializationContext context) =>
       JsonSerializer.SerializeToUtf8Bytes(data);
}