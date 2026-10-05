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
public class MapsterConfig
{
    public MapsterConfig()
    {
        TypeAdapterConfig<CustomerCreateUpdateDto, Customer>.NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.VisitCount);
        TypeAdapterConfig<CustomerDto, Customer>.NewConfig()
            .TwoWays();
        TypeAdapterConfig<(Customer Customer, int Visits), CustomerVisitsDto>.NewConfig()
            .MapWith(src => new CustomerVisitsDto(
                src.Customer.Id, src.Customer.FirstName, src.Customer.LastName, src.Visits));

        TypeAdapterConfig<TattooArtistCreateUpdateDto, TattooArtist>.NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Rating);
        TypeAdapterConfig<TattooArtistDto, TattooArtist>.NewConfig()
            .TwoWays();
        TypeAdapterConfig<(TattooArtist Artist, decimal Money), TattooArtistRevenueDto>.NewConfig()
            .MapWith(src => new TattooArtistRevenueDto(
                 src.Artist.Id, src.Artist.FirstName, src.Artist.LastName, src.Money));

        TypeAdapterConfig<BodyZonePricingCreateUpdateDto, BodyZonePricing>.NewConfig()
            .Ignore(dest => dest.Id);
        TypeAdapterConfig<BodyZonePricingDto, BodyZonePricing>.NewConfig()
            .TwoWays();

        TypeAdapterConfig<StylePricingCreateUpdateDto, StylePricing>.NewConfig()
            .Ignore(dest => dest.Id);
        TypeAdapterConfig<StylePricingDto, StylePricing>.NewConfig()
            .TwoWays();

        TypeAdapterConfig<TattooSessionCreateUpdateDto, TattooSession>.NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Customer)
            .Ignore(dest => dest.TattooArtist)
            .Ignore(dest => dest.BodyZone)
            .Ignore(dest => dest.Style);
        TypeAdapterConfig<TattooSession, TattooSessionDto>.NewConfig()
            .Map(dest => dest.CustomerId, src => src.Customer.Id)
            .Map(dest => dest.TattooArtistId, src => src.TattooArtist.Id)
            .Map(dest => dest.BodyZone, src => src.BodyZone)
            .Map(dest => dest.Style, src => src.Style)
            .TwoWays();
        TypeAdapterConfig<(decimal MinCost, decimal MaxCost, decimal AverageCost), TattooSessionRange>.NewConfig()
            .MapWith(src => new TattooSessionRange(src.MinCost, src.MaxCost, src.AverageCost));
    }
}
