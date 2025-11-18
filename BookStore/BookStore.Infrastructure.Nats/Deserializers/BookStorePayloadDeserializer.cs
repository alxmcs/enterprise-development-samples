using BookStore.Application.Contracts.BookAuthors;
using NATS.Client.Core;
using System.Buffers;
using System.Text.Json;

namespace BookStore.Infrastructure.Nats.Deserializers;
/// <summary>
/// Десериализатор для данных пейлоада Nats
/// </summary>
internal class BookStorePayloadDeserializer : INatsDeserialize<IList<BookAuthorCreateUpdateDto>>
{
    public IList<BookAuthorCreateUpdateDto>? Deserialize(in ReadOnlySequence<byte> buffer) =>
        JsonSerializer.Deserialize<IList<BookAuthorCreateUpdateDto>>(buffer.ToArray());
}
