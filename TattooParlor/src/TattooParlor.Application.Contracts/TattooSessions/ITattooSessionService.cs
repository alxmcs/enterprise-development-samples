using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.TattooSessions;

/// <summary>
/// Интерфейс службы приложения для сессий
/// </summary>
public interface ITattooSessionService : IApplicationService<TattooSessionDto, TattooSessionCreateUpdateDto, Guid>
{
    /// <summary>
    /// Метод для получения всех сессий, которые не подлежат возврату
    /// </summary>
    /// <returns>Список DTO сессий, которые не подлежат возврату</returns>
    IList<TattooSessionDto> GetAllNonRefundable();

    /// <summary>
    /// Метод для получения диапазона стоимости татуировок по стилю
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <returns>DTO с результатом запроса</returns>
    TattooSessionRange GetCostRange(TattooStyle style);
}
