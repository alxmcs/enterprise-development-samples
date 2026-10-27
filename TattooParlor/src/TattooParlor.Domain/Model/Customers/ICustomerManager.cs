using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.Customers;

/// <summary>
/// Интерфейс доменной службы клиентов
/// </summary>
public interface ICustomerManager : ICrudRepository<Customer, int>, IHasSessions<Customer>
{
    /// <summary>
    /// Топ-5 клиентов по количеству визитов за указанный период
    /// </summary>
    /// <param name="startDate">Дата начала</param>
    /// <param name="endDate">Дата окночания</param>
    /// <returns>Список клиентов с количеством визитов</returns>
    Task<IList<(Customer Customer, int Visits)>> ShowTop5CustomersByVisits(DateOnly startDate, DateOnly endDate);

    /// <summary>
    /// Получение списка клиентов с указанным стилем
    /// </summary>
    /// <param name="style">Стиль</param>
    /// <returns>Список клиентов с указанным стилем</returns>
    Task<IList<Customer>> ShowCustomersWithStyle(TattooStyle style);
}
