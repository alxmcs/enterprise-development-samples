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
public class AnalyticsService(IAuthorManager authorManager, IMapper mapper) : IAnalyticsService
{
    /// <inheritdoc/>
    public async Task<IList<BookDto>> GetLast5AuthorsBook(int dtoId) =>
        mapper.Map<List<BookDto>>(await authorManager.GetLast5AuthorsBook(dtoId));

    /// <inheritdoc/>
    public async Task<IList<KeyValuePair<string, int?>>> GetTop5AuthorsByPageCount() =>
        await authorManager.GetTop5AuthorsByPageCount();
}
