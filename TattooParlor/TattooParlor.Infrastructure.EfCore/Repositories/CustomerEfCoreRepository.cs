using TattooParlor.Domain.Model.Customers;

namespace TattooParlor.Infrastructure.EfCore.Repositories;

public class CustomerEfCoreRepository(TattooParlorDbContext context) : ICustomerManager
{

}
