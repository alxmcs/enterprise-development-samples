using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.References;
using TattooParlor.Domain.Model.TattooArtists;

namespace TattooParlor.Domain.Model.TattooSessions;

public class TattooSession
{
    public required Guid Id { get; set; }
    public required Customer CustomerId { get; set; }
    public required TattooArtist TattooArtistId { get; set; }
    public required DateTime SessionDate { get; set; }
    public required BodyZonePricing BodyZone { get; set; }
    public required StylePricing Style { get; set; }
    public required double HeightInCm { get; set; }
    public required double WidthInCm { get; set; }
    public bool IsMulticolor { get; set; }
    public DateTime? CancelDate { get; set; }
    public decimal Price => (decimal)(HeightInCm * WidthInCm)
        * Style.PricePerCm
        * BodyZone.Multiplier
        * (IsMulticolor ? Style.MulticolorMultipilier ?? 1m : 1m)
        * (TattooArtistId.FavoriteStyle.HasValue ? (TattooArtistId.FavoriteStyle == Style.Style ? 0.7m : 1m) : 1m);
    public decimal Prepayment => Price * 0.3m;
    public bool IsPrepaymentRefundable => CancelDate.HasValue && CancelDate.Value < SessionDate.AddDays(-3);
}
