using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.References;
using TattooParlor.Domain.Model.TattooArtists;

namespace TattooParlor.Domain.Model.TattooSessions;

public class TattooSession
{
    public required Guid Id { get; set; }
    public required Customer Customer { get; set; }
    public required TattooArtist TattooArtist { get; set; }
    public required DateTime SessionDate { get; set; }
    public required BodyZonePricing BodyZone { get; set; }
    public required StylePricing Style { get; set; }
    public required double HeightInCm { get; set; }
    public required double WidthInCm { get; set; }
    public bool IsMulticolor { get; set; }
    public DateTime? CancelDate { get; set; }
    public decimal Price => (decimal)HeightInCm * (decimal)WidthInCm
        * Style.PricePerSqCm
        * BodyZone.Multiplier
        * (IsMulticolor ? Style.MulticolorMultiplier ?? 1m : 1m)
        * (TattooArtist.FavoriteStyle == Style.Style ? 0.7m : 1m);
    public decimal Prepayment => Price * 0.3m;
    public bool IsPrepaymentNonRefundable => CancelDate.HasValue && CancelDate.Value >= SessionDate.AddDays(-3);
}
