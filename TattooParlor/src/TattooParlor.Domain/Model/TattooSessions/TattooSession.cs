using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.References;
using TattooParlor.Domain.Model.TattooArtists;

namespace TattooParlor.Domain.Model.TattooSessions;

/// <summary>
/// Тату-сессия
/// </summary>
public class TattooSession
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required Customer Customer { get; set; }

    /// <summary>
    /// Мастер
    /// </summary>
    public required TattooArtist TattooArtist { get; set; }

    /// <summary>
    /// Дата сессии
    /// </summary>
    public required DateTime SessionDate { get; set; }

    /// <summary>
    /// Зона татуировки
    /// </summary>
    public required BodyZonePricing BodyZone { get; set; }

    /// <summary>
    /// Стиль татуировки
    /// </summary>
    public required StylePricing Style { get; set; }

    /// <summary>
    /// Высота татуировки в сантиметрах
    /// </summary>
    public required double HeightInCm { get; set; }

    /// <summary>
    /// Ширина татуировки в сантиметрах
    /// </summary>
    public required double WidthInCm { get; set; }

    /// <summary>
    /// Индикатор многоцветности
    /// </summary>
    public bool IsMulticolor { get; set; }

    /// <summary>
    /// Дата отмены
    /// </summary>
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
