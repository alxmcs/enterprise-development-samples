using TattooParlor.Domain.Model.TattooSessions;

namespace TattooParlor.Domain.Model.Common;

/// <summary>
/// Интерфейс для доменной службы с доступом к сессиям
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IHasSessions<TEntity>
{
    /// <summary>
    /// Получение связвнных с сущностью сессий
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns>Список сессий</returns>
    IList<TattooSession> Sessions(TEntity entity);
}
