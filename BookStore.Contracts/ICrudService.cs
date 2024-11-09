namespace BookStore.Contracts;

/// <summary>
/// Интерфейс для примитивной CRUD-службы
/// </summary>
/// <typeparam name="TDto">Dto для просмотра сущности</typeparam>
/// <typeparam name="TCreateUpdateDto">Dto для апдейта сущности</typeparam>
/// <typeparam name="TKey">Тип праймари ключа сущности</typeparam>
public interface ICrudService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создание новой сущности
    /// </summary>
    /// <param name="newDto">Dto для апдейта сущности</param>
    /// <returns>Добавленная сущность</returns>
    public Task<TDto> Create(TCreateUpdateDto newDto);

    /// <summary>
    /// Обновление существующей сущности
    /// </summary>
    /// <param name="key">Идентификатор сущности</param>
    /// <param name="newDto">Dto для апдейта сущности</param>
    /// <returns>Обновленная сущность</returns>
    public Task<TDto> Update(TKey key, TCreateUpdateDto newDto);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Индикатор успешности операции</returns>
    public Task<bool> Delete(TKey id);

    /// <summary>
    /// Получение коллекции всех сущностей
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    public Task<IList<TDto>> GetList();

    /// <summary>
    /// Получение сущности по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Сущность</returns>
    public Task<TDto?> GetById(TKey id);
}
