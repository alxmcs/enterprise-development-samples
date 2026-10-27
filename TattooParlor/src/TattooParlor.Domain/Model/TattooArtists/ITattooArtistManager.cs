using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.TattooArtists;


/// <summary>
/// Интерфейс доменной службы тату-мастеров
/// </summary>
public interface ITattooArtistManager : ICrudRepository<TattooArtist, int>, IHasSessions<TattooArtist>
{
    /// <summary>
    /// Топ 5 тату-мастеров по выручке за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала периода</param>
    /// <param name="endDate">Дата окончания периода</param>
    /// <returns>Список тату-мастеров с выручкой</returns>
    Task<IList<(TattooArtist Artist, decimal Money)>> ShowTop5ArtistsByRevenue(DateOnly startDate, DateOnly endDate);

    /// <summary>
    /// Тату-мастера по их любимому стилю
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <returns>Список тату-мастеров</returns>
    Task<IList<TattooArtist>> ShowArtistsByFavoriteStyle(TattooStyle style);
}

