using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.TattooArtists;

/// <summary>
/// Интерфейс службы приложения для мастеров
/// </summary>
public interface ITattooArtistService : IApplicationService<TattooArtistDto, TattooArtistCreateUpdateDto, int>
{
    /// <summary>
    /// Получение связанных сессий
    /// </summary>
    /// <param name="tattooArtistId">Идентификатор мастера</param>
    /// <returns>Список DTO сессий</returns>
    IList<TattooSessionDto> GetSessions(int tattooArtistId);

    /// <summary>
    /// Тату-мастера по их любимому стилю
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <returns>Список DTOтату-мастеров с выручкой</returns>
    IList<TattooArtistDto> GetArtistsByFavoriteStyle(TattooStyle style);

    /// <summary>
    /// Топ 5 тату-мастеров по выручке за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала периода</param>
    /// <param name="endDate">Дата окончания периода</param>
    /// <returns>Список DTOтату-мастеров</returns>
    IList<TattooArtistRevenueDto> GetTop5ArtistsByRevenue(DateOnly startDate, DateOnly endDate);
}
