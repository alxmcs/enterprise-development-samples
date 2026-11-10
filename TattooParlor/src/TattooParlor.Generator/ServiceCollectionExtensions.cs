using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using TattooParlor.Generator.Application;
using TattooParlor.Generator.Infrasturucture;
using TattooParlor.Generator.Presentation;

namespace TattooParlor.Generator;

/// <summary>
/// Расширения коллекции служб для регистрации компонентов генератора контрактов
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация служб генератора контрактов
    /// </summary>
    /// <typeparam name="TProducer">Наследник IProducerService, реализующий отправку сообщений по шине</typeparam>
    /// <param name="services">Коллекция служб</param>
    /// <returns>Коллекция служб</returns>
    public static IServiceCollection AddGeneratorServices<TProducer>(this IServiceCollection services)
        where TProducer : class, IProducerService
    {
        services.AddHostedService<SessionGeneratorService>();
        services.AddControllers().AddApplicationPart(typeof(GeneratorController).Assembly);
        services.AddOpenApi();
        services.AddScoped<IProducerService, TProducer>();
        services.AddSingleton<TattooParlor.ServiceDefaults.Metrics.IApiMeter, TattooParlor.ServiceDefaults.Metrics.GeneratorApiMeter>();

        return services;
    }

    /// <summary>
    /// Конфигурация конвейера обработки запросов генератора контрактов
    /// </summary>
    /// <param name="app">Веб-приложение</param>
    /// <returns>Веб-приложение</returns>
    public static WebApplication UseGeneratorServices(this WebApplication app)
    {
        app.MapControllers();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        return app;
    }
}
