namespace TattooParlor.Application.Contracts.TattooSessions;

/// <summary>
/// DTO для аналитических запросов по сессиям
/// </summary>
/// <param name="MinCost">Минимальная стоимость</param>
/// <param name="MaxCost">Максимальная стоимость</param>
/// <param name="AverageCost">Средняя стоимость</param>
public record TattooSessionRange(decimal MinCost, decimal MaxCost, decimal AverageCost);
