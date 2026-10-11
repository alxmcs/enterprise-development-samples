using Mapster;
using Scalar.AspNetCore;
using TattooParlor.Api.OpenApi;
using MapsterMapper;
using TattooParlor.Application;
using TattooParlor.Application.Contracts;
using TattooParlor.Application.Contracts.BodyZonePricings;
using TattooParlor.Application.Contracts.Customers;
using TattooParlor.Application.Contracts.StylePricings;
using TattooParlor.Application.Contracts.TattooArtists;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Application.Services;
using TattooParlor.Domain.Data;
using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.References;
using TattooParlor.Domain.Model.TattooArtists;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Infrastructure.InMemory.Repositories;

var config = new TypeAdapterConfig();
config.Apply(new MapsterConfig());
config.Compile();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DataSeeder>();
builder.Services.AddSingleton<ICustomerManager, CustomerInMemoryRepository>();
builder.Services.AddSingleton<ITattooArtistManager, TattooArtistsInMemoryRepository>();
builder.Services.AddSingleton<ITattooSessionManager, TattooSessionsInMemoryRepository>();
builder.Services.AddSingleton<ICrudRepository<StylePricing, int>, StylePricingInMemoryRepository>();
builder.Services.AddSingleton<ICrudRepository<BodyZonePricing, int>, BodyZonePricingInMemoryRepository>();

builder.Services.AddSingleton<MapsterConfig>();
builder.Services.AddSingleton<IMapper>(new Mapper(config));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ITattooArtistService, TattooArtistService>();
builder.Services.AddScoped<ITattooSessionService, TattooSessionService>();
builder.Services.AddScoped<IApplicationService<BodyZonePricingDto, BodyZonePricingCreateUpdateDto, int>, BodyZonePricingService>();
builder.Services.AddScoped<IApplicationService<StylePricingDto, StylePricingCreateUpdateDto, int>, StylePricingService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi(options => options.AddSchemaTransformer<EnumSchemaTransformer>());

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
