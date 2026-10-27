using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.References;

namespace TattooParlor.Infrastructure.EfCore.Repositories;

public class BodyZonePricingEfCoreRepository(TattooParlorDbContext context) : ICrudRepository<BodyZonePricing, int>
{

}
