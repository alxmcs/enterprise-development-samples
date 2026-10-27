using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.References;
using TattooParlor.Domain.Model.TattooArtists;

namespace TattooParlor.Domain.Model.TattooSessions;

/// <summary>
/// Тату-сессия
/// </summary>
[Table("tattoo_sessions", Schema = "tp")]
public class TattooSession
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public required Guid Id { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Column("customer_id")]
    [ForeignKey(nameof(Customer))]
    public int CustomerId { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public virtual required Customer Customer { get; set; }

    /// <summary>
    /// Идентификатор мастера
    /// </summary>
    [Column("tattoo_artist_id")]
    [ForeignKey(nameof(TattooArtist))]
    public int TattooArtistId { get; set; }

    /// <summary>
    /// Мастер
    /// </summary>
    public virtual required TattooArtist TattooArtist { get; set; }

    /// <summary>
    /// Дата сессии
    /// </summary>
    [Column("session_date", TypeName = "timestamp without time zone")]
    public required DateTime SessionDate { get; set; }

    /// <summary>
    /// Идентификатор зоны татуировки
    /// </summary>
    [Column("body_zone_id")]
    [ForeignKey(nameof(BodyZone))]
    public int BodyZoneId { get; set; }

    /// <summary>
    /// Зона татуировки
    /// </summary>
    public virtual required BodyZonePricing BodyZone { get; set; }

    /// <summary>
    /// Идентификатор стиля татуировки
    /// </summary>
    [Column("style_id")]
    [ForeignKey(nameof(Style))]
    public int StyleId { get; set; }

    /// <summary>
    /// Стиль татуировки
    /// </summary>
    public virtual required StylePricing Style { get; set; }

    /// <summary>
    /// Высота татуировки в сантиметрах
    /// </summary>
    [Column("height_in_cm")]
    public required double HeightInCm { get; set; }

    /// <summary>
    /// Ширина татуировки в сантиметрах
    /// </summary>
    [Column("width_in_cm")]
    public required double WidthInCm { get; set; }

    /// <summary>
    /// Индикатор многоцветности
    /// </summary>
    [Column("is_multicolor")]
    public bool IsMulticolor { get; set; }

    /// <summary>
    /// Дата отмены
    /// </summary>
    [Column("cancel_date", TypeName = "timestamp without time zone")]
    public DateTime? CancelDate { get; set; }

    /// <summary>
    /// Стоимость
    /// </summary>
    public decimal Price => (decimal)HeightInCm * (decimal)WidthInCm
        * Style.PricePerSqCm
        * BodyZone.Multiplier
        * (IsMulticolor ? Style.MulticolorMultiplier ?? 1m : 1m)
        * (TattooArtist.FavoriteStyle == Style.Style ? 0.7m : 1m);

    /// <summary>
    /// Аванс
    /// </summary>
    public decimal Prepayment => Price * 0.3m;

    /// <summary>
    /// Индикатор возврата аванса
    /// </summary>
    public bool IsPrepaymentNonRefundable => CancelDate.HasValue && CancelDate.Value >= SessionDate.AddDays(-3);
}
