using Confluent.Kafka;
using System.Text.Json;

namespace TattooParlor.Generator.Kafka.Infrastructure.Serializers;

/// <summary>
/// Сериализатор для ключа пейлода Kafka
/// </summary>
public class TattooParlorKeySerializer : ISerializer<Guid>
{
    /// <inheritdoc/>
    public byte[] Serialize(Guid data, SerializationContext context) =>
       JsonSerializer.SerializeToUtf8Bytes(data);
}