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
            .Ignore(dest => dest.VisitCount);
        config.NewConfig<CustomerDto, Customer>()
            .TwoWays();
        config.NewConfig<(Customer Customer, int Visits), CustomerVisitsDto>()
            .MapWith(src => new CustomerVisitsDto(
                src.Customer.Id, src.Customer.FirstName, src.Customer.LastName, src.Visits));

        config.NewConfig<TattooArtistCreateUpdateDto, TattooArtist>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Rating);
        config.NewConfig<TattooArtistDto, TattooArtist>()
            .TwoWays();
        config.NewConfig<(TattooArtist Artist, decimal Money), TattooArtistRevenueDto>()
            .MapWith(src => new TattooArtistRevenueDto(
                 src.Artist.Id, src.Artist.FirstName, src.Artist.LastName, src.Money));

        config.NewConfig<BodyZonePricingCreateUpdateDto, BodyZonePricing>()
            .Ignore(dest => dest.Id);
        config.NewConfig<BodyZonePricingDto, BodyZonePricing>()
            .TwoWays();

        config.NewConfig<StylePricingCreateUpdateDto, StylePricing>()
            .Ignore(dest => dest.Id);
        config.NewConfig<StylePricingDto, StylePricing>()
            .TwoWays();

        config.NewConfig<TattooSessionCreateUpdateDto, TattooSession>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Customer)
            .Ignore(dest => dest.TattooArtist)
            .Ignore(dest => dest.BodyZone)
            .Ignore(dest => dest.Style);
        config.NewConfig<TattooSession, TattooSessionDto>()
            .Map(dest => dest.CustomerId, src => src.Customer.Id)
            .Map(dest => dest.TattooArtistId, src => src.TattooArtist.Id)
            .Map(dest => dest.BodyZone, src => src.BodyZone)
            .Map(dest => dest.Style, src => src.Style)
            .TwoWays();
        config.NewConfig<(decimal MinCost, decimal MaxCost, decimal AverageCost), TattooSessionRange>()
            .MapWith(src => new TattooSessionRange(src.MinCost, src.MaxCost, src.AverageCost));
    }
}
