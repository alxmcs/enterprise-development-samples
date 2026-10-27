using TattooParlor.Domain.Model.TattooSessions;

namespace TattooParlor.Domain.Model.Common;

/// <summary>
/// Интерфейс для доменной службы с доступом к сессиям
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IHasSessions<TEntity>
{
    /// <summary>
    /// Получение связанных с сущностью сессий
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns>Список сессий</returns>
    Task<IList<TattooSession>> Sessions(TEntity entity);
}
