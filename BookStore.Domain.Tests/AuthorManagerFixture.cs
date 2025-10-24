namespace BookStore.Domain.Tets;

/// <summary>
/// Фикстура для того, чтобы расшарить контекст с доменной службой между всеми юнит-тестами
/// </summary>
public class AuthorManagerFixture
{
    private readonly AuthorInMemoryRepository _authorRepository = new(); 
    private readonly BookAuthorInMemoryRepository _bookAuthorRepository = new(); 
    private readonly BookInMemoryRepository _bookRepository = new();

    public AuthorManager AuthorManager { get; init; }

    public AuthorManagerFixture() => AuthorManager = new(_authorRepository, _bookAuthorRepository, _bookRepository);
}
