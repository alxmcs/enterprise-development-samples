using TattooParlor.Domain.Data;
using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

/// <summary>
/// Ин-мемори репозиторий для клиетов
/// </summary>
public class CustomerInMemoryRepository(DataSeeder data) : ICustomerManager
{
    /// <inheritdoc/> 
    public Customer Create(Customer entity)
    {
        entity.Id = data.Customers.MaxBy(c => c.Id)!.Id + 1;
        data.Customers.Add(entity);
        return entity;
    }

    /// <inheritdoc/> 
    public bool Delete(int entityId)
    {
        var res = data.Customers.RemoveAll(s => s.Id == entityId);
        return res > 0;
    }

    /// <inheritdoc/> 
    public Customer? Read(int entityId) => data.Customers.FirstOrDefault(s => s.Id == entityId);

    /// <inheritdoc/> 
    public IList<Customer> ReadAll() => [.. data.Customers];

    /// <inheritdoc/> 
    public IList<TattooSession> Sessions(Customer entity)
        => [.. data.TattooSessions.Where(s => s.Customer.Id == entity.Id)];

    /// <inheritdoc/> 
    public IList<Customer> ShowCustomersWithStyle(TattooStyle style) =>
        [.. data.TattooSessions.Where(s => s.Style.Style == style && s.CancelDate == null).Select(s => s.Customer).DistinctBy(c => c.Id)];

    /// <inheritdoc/> 
    public IList<(Customer Customer, int Visits)> ShowTop5CustomersByVisits(DateOnly startDate, DateOnly endDate)
    {
        var start = startDate.ToDateTime(TimeOnly.MinValue);
        var end = endDate.ToDateTime(TimeOnly.MaxValue);

        if (start > end)
            throw new ArgumentException("Start date must be less than or equal to end date");

        return [..data.TattooSessions
            .Where(s => s.SessionDate >= start && s.SessionDate <= end && s.CancelDate == null)
            .GroupBy(s => s.Customer)
            .Select(g => (Customer:g.Key, Visits:g.Count()))
            .OrderByDescending(x => x.Visits)
            .Take(5)];
    }

    /// <inheritdoc/> 
    public Customer Update(Customer entity)
    {
        Delete(entity.Id);
        Create(entity);
        return entity;
    }
}
