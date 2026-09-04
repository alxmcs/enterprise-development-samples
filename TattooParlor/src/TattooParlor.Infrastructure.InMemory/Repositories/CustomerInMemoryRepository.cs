using TattooParlor.Domain.Data;
using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

public class CustomerInMemoryRepository(DataSeeder data) : ICustomerManager
{
    public Customer Create(Customer entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int entityId)
    {
        throw new NotImplementedException();
    }

    public Customer? Read(int entityId)
    {
        throw new NotImplementedException();
    }

    public IList<Customer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public IList<TattooSession> Sessions(Customer entity)
    {
        throw new NotImplementedException();
    }

    public IList<Customer> ShowCustomersWithStyle(TattooStyle style) =>
         [.. data.TattooSessions.Where(s => s.Style.Style == style).Select(s => s.Customer).Distinct()];

    public IList<(Customer Customer, int Visits)> ShowTop5CustomersByVisits(DateOnly startDate, DateOnly endDate)
    {
        var start = startDate.ToDateTime(TimeOnly.MinValue);
        var end = endDate.ToDateTime(TimeOnly.MaxValue);

        if (start > end)
            throw new ArgumentException("Start date must be less than or equal to end date");

        return [..data.TattooSessions
            .Where(s => s.SessionDate >= start && s.SessionDate <= end)
            .GroupBy(s => s.Customer)
            .Select(g => (Customer:g.Key, Visits:g.Count()))
            .OrderByDescending(x => x.Visits)
            .Take(5)];
    }

    public Customer Update(Customer entity)
    {
        throw new NotImplementedException();
    }
}
