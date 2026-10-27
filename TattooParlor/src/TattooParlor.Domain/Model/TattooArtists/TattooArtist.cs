using System.ComponentModel.DataAnnotations.Schema;
using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.TattooArtists;

/// <summary>
/// Тату-мастер
/// </summary>
[Table("tattoo_artists", Schema = "tp")]
public class TattooArtist : Person
{
    /// <summary>
    /// Дата начала работы
    /// </summary>
    [Column("work_start_date")]
    public required DateOnly WorkStartDate { get; set; }

    /// <summary>
    /// Рейтинг
    /// </summary>
    [Column("rating")]
    public double Rating { get; set; }

    /// <summary>
    /// Предпочитаемый стиль
    /// </summary>
    [Column("favorite_style")]
    public TattooStyle? FavoriteStyle { get; set; }

    /// <summary>
    /// Сессии мастера
    /// </summary>
    public virtual ICollection<TattooSession> Sessions { get; set; } = [];
}
