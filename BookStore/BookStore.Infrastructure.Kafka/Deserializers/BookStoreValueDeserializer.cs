using BookStore.Application.Contracts.BookAuthors;
using Confluent.Kafka;
using System.Text.Json;

namespace BookStore.Infrastructure.Kafka.Deserializers;

/// <summary>
/// Десериализатор для значения пейлода Kafka
/// </summary>
public class BookStoreValueDeserializer : IDeserializer<IList<BookAuthorCreateUpdateDto>>
{
    public IList<BookAuthorCreateUpdateDto> Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull) return [];
        return JsonSerializer.Deserialize<IList<BookAuthorCreateUpdateDto>>(data) ?? [];
    }
}
