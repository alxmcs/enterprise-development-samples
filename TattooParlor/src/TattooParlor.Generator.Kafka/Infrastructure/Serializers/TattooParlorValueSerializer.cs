using Confluent.Kafka;
using System.Text.Json;
using TattooParlor.Application.Contracts.TattooSessions;

namespace TattooParlor.Generator.Kafka.Infrastructure.Serializers;

/// <summary>
/// Сериализатор для значения пейлода Kafka
/// </summary>
public class TattooParlorValueSerializer : ISerializer<IList<TattooSessionCreateUpdateDto>>
{
    /// <inheritdoc/>
    public byte[] Serialize(IList<TattooSessionCreateUpdateDto> data, SerializationContext context) =>
        JsonSerializer.SerializeToUtf8Bytes(data);
}