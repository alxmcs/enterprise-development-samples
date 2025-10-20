namespace BookStore.Domain;

/// <summary>
/// Интерфейс репозитория для CRUD операций
/// </summary>
/// <typeparam name="TEntity">Тип сущности, доступ к коллекции которых абстрагируем</typeparam>
/// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Создание новой сущности
    /// </summary>
    /// <param name="entity">Новая сущность</param>
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
    /// <returns></returns>
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
    Task<bool> Delete(TKey entityId);
}
