using TattooParlor.Domain.Model.TattooSessions;

namespace TattooParlor.Infrastructure.EfCore.Repositories;

public class TattooSessionsEfCoreRepository(TattooParlorDbContext context) : ITattooSessionManager
{
}
