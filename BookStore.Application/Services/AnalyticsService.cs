using AutoMapper;
using BookStore.Application.Contracts;
using BookStore.Application.Contracts.Books;
using BookStore.Domain.Model.Authors;

namespace BookStore.Application.Services;
/// <summary>
/// Аналитическая служба
/// </summary>
/// <param name="authorManager">Доменная служба для запуска юзкейсов авторов</param>
/// <param name="mapper">Профиль маппига</param>
public class AnalyticsService(AuthorManager authorManager, IMapper mapper) : IAnalyticsService
{
    /// <inheritdoc/>
    public List<BookDto> GetLast5AuthorsBook(int dtoId) =>
        mapper.Map<List<BookDto>>(authorManager.GetLast5AuthorsBook(dtoId));

    /// <inheritdoc/>
    public List<KeyValuePair<string, int?>> GetTop5AuthorsByPageCount() =>
        authorManager.GetTop5AuthorsByPageCount();
}
