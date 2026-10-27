namespace TattooParlor.Domain.Model.Common;

/// <summary>
/// Интерфейс репозитория для CRUD операций
/// </summary>
/// <typeparam name="TEntity">Тип сущности, доступ к коллекции которых абстрагируем</typeparam>
/// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
public interface ICrudRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Создание новой сущности
    /// </summary>
    /// <param name="entity">Новая сущность</param>
    /// <returns>Созданная сущность</returns>   
    Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Получение сущности по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <returns>Сущность</returns>
    Task<TEntity?> Read(TKey entityId);

    /// <summary>
    /// Получение всего списка сущностей
    /// </summary> 
    /// <returns>Весь список сущностей</returns>
    Task<IList<TEntity>> ReadAll();

    /// <summary>
    /// Обновление сущности в коллекции
    /// </summary>
    /// <param name="entity">Отредактированная сущность</param>
    Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Удаление сущности из коллекции
    /// </summary>
    /// <param name="entityId">Идентификатор сущности</param>
    /// <returns>True, если сущность была удалена, иначе False</returns>
    Task<bool> Delete(TKey entityId);
}