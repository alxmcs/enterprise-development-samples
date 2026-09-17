using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.TattooArtists;

/// <summary>
/// Тату-мастер
/// </summary>
public class TattooArtist : Person
{
    /// <summary>
    /// Дата начала работы
    /// </summary>
    public required DateOnly WorkStartDate { get; set; }

    /// <summary>
    /// Рейтинг
    /// </summary>
    public double Rating { get; set; }

    /// <summary>
    /// Предпочитаемый стиль
    /// </summary>
    public TattooStyle? FavoriteStyle { get; set; }
}
