using TattooParlor.Domain.Data;
using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

/// <summary>
/// Ин-мемори репозиторий для клиентов
/// </summary>
public class CustomerInMemoryRepository(DataSeeder data) : ICustomerManager
{
    /// <inheritdoc/>
    public Task<Customer> Create(Customer entity)
    {
        entity.Id = data.Customers.Count == 0 ? 1 : data.Customers.Max(c => c.Id) + 1;
        data.Customers.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(int entityId)
    {
        var res = data.Customers.RemoveAll(s => s.Id == entityId);
        return Task.FromResult(res > 0);
    }

    /// <inheritdoc/>
    public Task<Customer?> Read(int entityId) => Task.FromResult(data.Customers.FirstOrDefault(s => s.Id == entityId));

    /// <inheritdoc/>
    public Task<IList<Customer>> ReadAll() => Task.FromResult<IList<Customer>>([.. data.Customers]);

    /// <inheritdoc/>
    public Task<IList<TattooSession>> Sessions(Customer entity)
        => Task.FromResult<IList<TattooSession>>([.. data.TattooSessions.Where(s => s.Customer.Id == entity.Id)]);

    /// <inheritdoc/>
    public Task<IList<Customer>> ShowCustomersWithStyle(TattooStyle style) =>
        Task.FromResult<IList<Customer>>(
            [.. data.TattooSessions.Where(s => s.Style.Style == style && s.CancelDate == null).Select(s => s.Customer).DistinctBy(c => c.Id)]);

    /// <inheritdoc/>
    public Task<IList<(Customer Customer, int Visits)>> ShowTop5CustomersByVisits(DateOnly startDate, DateOnly endDate)
    {
        var start = startDate.ToDateTime(TimeOnly.MinValue);
        var end = endDate.ToDateTime(TimeOnly.MaxValue);

        if (start > end)
            throw new ArgumentException("Дата начала не может быть позже даты окончания");

        return Task.FromResult<IList<(Customer Customer, int Visits)>>([..data.TattooSessions
            .Where(s => s.SessionDate >= start && s.SessionDate <= end && s.CancelDate == null)
            .GroupBy(s => s.Customer)
            .Select(g => (Customer:g.Key, Visits:g.Count()))
            .OrderByDescending(x => x.Visits)
            .Take(5)]);
    }

    /// <inheritdoc/>
    public Task<Customer> Update(Customer entity)
    {
        Delete(entity.Id);
        Create(entity);
        return Task.FromResult(entity);
    }
}
