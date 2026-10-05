using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.Customers;

/// <summary>
/// Интерфейс службы приложения для клентов
/// </summary>
public interface ICustomersService : IApplicationService<CustomerDto, CustomerCreateUpdateDto, int>
{
    /// <summary>
    /// Получение связанных сессий
    /// </summary>
    /// <param name="customerId">Идентификатор клиента</param>
    /// <returns>Список DTO сессий</returns>
    IList<TattooSessionDto> GetSessions(int customerId);

    /// <summary>
    /// Получение списка клиентов с указанным стилем
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <returns>Список DTO клиентов с указанным стилем</returns>
    IList<CustomerDto> GetCustomersWithStyle(TattooStyle style);

    /// <summary>
    /// Топ-5 клиентов по количеству визитов за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала</param>
    /// <param name="endDate">Дата окночания</param>
    /// <returns>Список DTO клиентов с количеством визитов</returns>
    IList<CustomerVisitsDto> GetTop5CustomersByVisits(DateOnly startDate, DateOnly endDate);
}
