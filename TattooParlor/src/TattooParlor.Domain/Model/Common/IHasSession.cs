using TattooParlor.Domain.Model.TattooSessions;

namespace TattooParlor.Domain.Model.Common;

public interface IHasSessions<TEntity> 
{
    IList<TattooSession> Sessions(TEntity entity);
}
