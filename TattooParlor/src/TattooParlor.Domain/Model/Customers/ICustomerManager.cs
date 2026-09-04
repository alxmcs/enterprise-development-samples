using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.Customers;

public interface ICustomerManager : ICrudRepository<Customer, int>, IHasSessions<Customer>
{
    IList<(Customer Customer, int Visits)> ShowTop5CustomersByVisits(DateOnly startDate, DateOnly endDate);

    IList<Customer> ShowCustomersWithStyle(TattooStyle style);
}
