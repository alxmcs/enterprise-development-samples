using BookStore.Application.Contracts.BookAuthors;
using Confluent.Kafka;
using System.Text.Json;

namespace BookStore.Generator.Kafka.Host.Serializers;

/// <summary>
/// Сериализатор для значения пейлода Kafka
/// </summary>
public class BookStoreValueSerializer : ISerializer<IList<BookAuthorCreateUpdateDto>>
{
    /// <inheritdoc/>
    public byte[] Serialize(IList<BookAuthorCreateUpdateDto> data, SerializationContext context) =>
        JsonSerializer.SerializeToUtf8Bytes(data);
}