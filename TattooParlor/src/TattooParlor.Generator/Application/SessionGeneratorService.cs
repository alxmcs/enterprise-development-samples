using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TattooParlor.Generator.Domain;
using TattooParlor.Generator.Infrasturucture;

namespace TattooParlor.Generator.Application;

/// <summary>
/// Служба для генерации и отправки заданного числа контрактов через заданные интервалы
/// </summary>
/// <param name="configuration">Конфигурация</param>
/// <param name="scopeFactory">Фабрика контекста</param>
/// <param name="logger">Логгер</param>
public class SessionGeneratorService(IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<SessionGeneratorService> logger) : BackgroundService
{
    private readonly string _batchSize = configuration.GetSection("Generator")["BatchSize"] ?? throw new KeyNotFoundException("BatchSize section of Generator is missing");
    private readonly string _payloadLimit = configuration.GetSection("Generator")["PayloadLimit"] ?? throw new KeyNotFoundException("PayloadLimit section of Generator is missing");
    private readonly string _waitTime = configuration.GetSection("Generator")["WaitTime"] ?? throw new KeyNotFoundException("WaitTime section of Generator is missing");

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting to send {total} messages with {time}s interval with {batch} messages in batch", _payloadLimit, _waitTime, _batchSize);

        if (!int.TryParse(_batchSize, out var batchSize) || batchSize <= 0) throw new FormatException("BatchSize must be a positive integer");
        if (!int.TryParse(_payloadLimit, out var payloadLimit)) throw new FormatException("Unable to parse PayloadLimit");
        if (!int.TryParse(_waitTime, out var waitTime)) throw new FormatException("Unable to parse WaitTime");

        var counter = 0;
        using var scope = scopeFactory.CreateScope();
        var producer = scope.ServiceProvider.GetRequiredService<IProducerService>();
        while (counter < payloadLimit)
        {

            var batch = SessionGenerator.GenerateLinks(Math.Min(batchSize, payloadLimit - counter));
            await producer.SendAsync(batch, cancellationToken);
            await Task.Delay(waitTime * 1000, cancellationToken);
            counter += batch.Count;
        }
        logger.LogInformation("Finished sending {total} messages with {time}s interval with {batch} messages in batch", _payloadLimit, _waitTime, _batchSize);
    }

}