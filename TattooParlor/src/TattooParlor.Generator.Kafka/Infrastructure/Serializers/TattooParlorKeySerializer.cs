using Confluent.Kafka;
using System.Text.Json;

namespace TattooParlor.Generator.Kafka.Infrastructure.Serializers;

/// <summary>
/// Сериализатор для ключа пейлода Kafka
/// </summary>
public class TattooParlorKeySerializer : ISerializer<int>
{
    /// <inheritdoc/>
    public byte[] Serialize(int data, SerializationContext context) =>
       JsonSerializer.SerializeToUtf8Bytes(data);
}