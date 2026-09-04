using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Domain.Model.TattooSessions;

public interface ITattooSessionManager : ICrudRepository<TattooSession, Guid>
{
    (decimal MinCost, decimal MaxCost, decimal AverageCost) ShowCostRange(TattooStyle style);

    IList<TattooSession> ShowAllNonRefundable();
}
