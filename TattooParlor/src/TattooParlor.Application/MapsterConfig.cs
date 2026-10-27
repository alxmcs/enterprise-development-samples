using Mapster;
using TattooParlor.Application.Contracts.BodyZonePricings;
using TattooParlor.Application.Contracts.Customers;
using TattooParlor.Application.Contracts.StylePricings;
using TattooParlor.Application.Contracts.TattooArtists;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.References;
using TattooParlor.Domain.Model.TattooArtists;
using TattooParlor.Domain.Model.TattooSessions;

namespace TattooParlor.Application;

/// <summary>
/// Конфигурация Mapster для преобразования DTO в доменные классы и обратно
/// </summary>
public class MapsterConfig : IRegister
{
    /// <inheritdoc/>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CustomerCreateUpdateDto, Customer>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Sessions);
        config.NewConfig<CustomerDto, Customer>()
            .Ignore(dest => dest.Sessions)
            .TwoWays();
        config.NewConfig<(Customer Customer, int Visits), CustomerVisitsDto>()
            .MapWith(src => new CustomerVisitsDto(
                src.Customer.Id, src.Customer.FirstName, src.Customer.LastName, src.Visits));

        config.NewConfig<TattooArtistCreateUpdateDto, TattooArtist>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Sessions);
        config.NewConfig<TattooArtistDto, TattooArtist>()
            .Ignore(dest => dest.Sessions)
            .TwoWays();
        config.NewConfig<(TattooArtist Artist, decimal Money), TattooArtistRevenueDto>()
            .MapWith(src => new TattooArtistRevenueDto(
                 src.Artist.Id, src.Artist.FirstName, src.Artist.LastName, src.Money));

        config.NewConfig<BodyZonePricingCreateUpdateDto, BodyZonePricing>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Sessions);
        config.NewConfig<BodyZonePricingDto, BodyZonePricing>()
            .Ignore(dest => dest.Sessions)
            .TwoWays();

        config.NewConfig<StylePricingCreateUpdateDto, StylePricing>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Sessions);
        config.NewConfig<StylePricingDto, StylePricing>()
            .Ignore(dest => dest.Sessions)
            .TwoWays();

        config.NewConfig<TattooSessionCreateUpdateDto, TattooSession>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Customer)
            .Ignore(dest => dest.TattooArtist)
            .Ignore(dest => dest.BodyZoneId)
            .Ignore(dest => dest.StyleId)
            .Map(dest => dest.Style, src => new StylePricing { Id = 0, Style = src.Style, PricePerSqCm = 0 })
            .Map(dest => dest.BodyZone, src => new BodyZonePricing { Id = 0, BodyZone = src.BodyZone, Multiplier = 0 });
        config.NewConfig<TattooSession, TattooSessionDto>()
            .Map(dest => dest.CustomerId, src => src.Customer.Id)
            .Map(dest => dest.TattooArtistId, src => src.TattooArtist.Id)
            .Map(dest => dest.BodyZone, src => src.BodyZone)
            .Map(dest => dest.Style, src => src.Style);
        config.NewConfig<(decimal MinCost, decimal MaxCost, decimal AverageCost), TattooSessionRange>()
            .MapWith(src => new TattooSessionRange(src.MinCost, src.MaxCost, src.AverageCost));
    }
}
