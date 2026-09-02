using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.InMemory.Repositories;

public class TattooSessionsInMemoryRepository : ITattooSessionManager
{
    public TattooSession Create(TattooSession entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public TattooSession? Read(Guid entityId)
    {
        throw new NotImplementedException();
    }

    public IList<TattooSession> ReadAll()
    {
        throw new NotImplementedException();
    }

    public IList<TattooSession> ShowAllNonRefundable()
    {
        throw new NotImplementedException();
    }

    public (decimal MinCost, decimal MaxCost, decimal AverageCost) ShowCostRange(TattooStyle style)
    {
        throw new NotImplementedException();
    }

    public TattooSession Update(TattooSession entity)
    {
        throw new NotImplementedException();
    }
}
