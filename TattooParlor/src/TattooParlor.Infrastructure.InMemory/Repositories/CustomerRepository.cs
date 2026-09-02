using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

public class CustomerRepository : ICustomerManager
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

    public IList<Customer> ShowCustomersWithStyle(TattooStyle style)
    {
        throw new NotImplementedException();
    }

    public IList<(Customer Customer, int Visits)> ShowTop5CustomersByVisits(DateOnly startDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public Customer Update(Customer entity)
    {
        throw new NotImplementedException();
    }
}
