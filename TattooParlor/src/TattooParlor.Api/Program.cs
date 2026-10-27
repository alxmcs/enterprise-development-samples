using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TattooParlor.Api.OpenApi;
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
using TattooParlor.Infrastructure.EfCore;
using TattooParlor.Infrastructure.EfCore.Repositories;

var config = new TypeAdapterConfig();
config.Apply(new MapsterConfig());
config.Compile();

var builder = WebApplication.CreateBuilder(args);

//Cлужбы персистенс слоя
builder.Services.AddSingleton<DataSeeder>();
builder.Services.AddScoped<ICustomerManager, CustomerEfCoreRepository>();
builder.Services.AddScoped<ITattooArtistManager, TattooArtistsEfCoreRepository>();
builder.Services.AddScoped<ITattooSessionManager, TattooSessionsEfCoreRepository>();
builder.Services.AddScoped<ICrudRepository<StylePricing, int>, StylePricingEfCoreRepository>();
builder.Services.AddScoped<ICrudRepository<BodyZonePricing, int>, BodyZonePricingEfCoreRepository>();
builder.AddNpgsqlDbContext<TattooParlorDbContext>("Database", configureDbContextOptions: builder => builder.UseLazyLoadingProxies());

//Cлужбы аппликейшен слоя
builder.Services.AddSingleton<MapsterConfig>();
builder.Services.AddSingleton<IMapper>(new Mapper(config));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ITattooArtistService, TattooArtistService>();
builder.Services.AddScoped<ITattooSessionService, TattooSessionService>();
builder.Services.AddScoped<IApplicationService<BodyZonePricingDto, BodyZonePricingCreateUpdateDto, int>, BodyZonePricingService>();
builder.Services.AddScoped<IApplicationService<StylePricingDto, StylePricingCreateUpdateDto, int>, StylePricingService>();

//Cлужбы презентейшен слоя
builder.Services.AddControllers();
builder.Services.AddOpenApi(options => options.AddSchemaTransformer<EnumSchemaTransformer>());

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<TattooParlorDbContext>();
    db.Database.Migrate();

    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
