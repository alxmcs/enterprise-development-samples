using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Contracts.Customers;

/// <summary>
/// Интерфейс службы приложения для клентов
/// </summary>
public interface ICustomerService : IApplicationService<CustomerDto, CustomerCreateUpdateDto, int>
{
    /// <summary>
    /// Получение связанных сессий
    /// </summary>
    /// <param name="customer">DTO клиента</param>
    /// <returns>Список DTO сессий</returns>
    Task<IList<TattooSessionDto>> GetSessions(CustomerDto customer);

    /// <summary>
    /// Получение списка клиентов с указанным стилем
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <returns>Список DTO клиентов с указанным стилем</returns>
    Task<IList<CustomerDto>> GetCustomersWithStyle(TattooStyle style);

    /// <summary>
    /// Топ-5 клиентов по количеству визитов за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала</param>
    /// <param name="endDate">Дата окночания</param>
    /// <returns>Список DTO клиентов с количеством визитов</returns>
    Task<IList<CustomerVisitsDto>> GetTop5CustomersByVisits(DateOnly startDate, DateOnly endDate);
}
