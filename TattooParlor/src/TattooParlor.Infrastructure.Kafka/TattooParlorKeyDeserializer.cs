using Confluent.Kafka;
using System.Text.Json;

namespace TattooParlor.Infrastructure.Kafka;

/// <summary>
/// Десериализатор для ключа пейлода Kafka
/// </summary>
public class TattooParlorKeyDeserializer : IDeserializer<int>
{
    /// <inheritdoc/>
    public int Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context) =>
        isNull
            ? 0
            : JsonSerializer.Deserialize<int>(data);
}
