using Confluent.Kafka;
using System.Text.Json;
using TattooParlor.Application.Contracts.TattooSessions;

namespace TattooParlor.Infrastructure.Kafka;

/// <summary>
/// Десериализатор для значения пейлода Kafka
/// </summary>
public class TattooParlorValueDeserializer : IDeserializer<IList<TattooSessionCreateUpdateDto>>
{
    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    /// <inheritdoc/>
    public IList<TattooSessionCreateUpdateDto> Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context) =>
        isNull? []: JsonSerializer.Deserialize<IList<TattooSessionCreateUpdateDto>>(data, Options) ?? [];
}
