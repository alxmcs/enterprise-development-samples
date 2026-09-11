using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.TattooSessions;

/// <summary>
/// Интерфейс доменной службы сессий
/// </summary>
public interface ITattooSessionManager : ICrudRepository<TattooSession, Guid>
{
    /// <summary>
    /// Метод для получения диапазона стоимости татуировок по стилю
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <returns>Кортеж из минимальной, максимальной и средней стоимости</returns>
    (decimal MinCost, decimal MaxCost, decimal AverageCost) ShowCostRange(TattooStyle style);

    /// <summary>
    /// Метод для получения всех сессий, которые не подлежат возврату
    /// </summary>
    /// <returns>Список сессий, которые не подлежат возврату</returns>
    IList<TattooSession> ShowAllNonRefundable();
}
