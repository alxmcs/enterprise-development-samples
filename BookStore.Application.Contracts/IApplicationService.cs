namespace BookStore.Application.Contracts;

/// <summary>
/// Интерфейс службы приложения для CRUD операций
/// </summary>
/// <typeparam name="TDto">DTO для Get-запросов</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO для Post/Put-запросов</typeparam>
/// <typeparam name="TKey">Тип идентификатора DTO</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создание DTO
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns></returns>
    TDto Create(TCreateUpdateDto dto);

    /// <summary>
    /// Получение DTO по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор DTO</param>
    /// <returns></returns>
    TDto Get(TKey dtoId);

    /// <summary>
    /// Получение всего списка DTO
    /// </summary>
    /// <returns></returns>
    List<TDto> GetAll();

    /// <summary>
    /// Обновление DTO
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <param name="dtoId">Идентификатор DTO</param> 
    /// <returns></returns>
    TDto Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Удаление DTO
    /// </summary>
    /// <param name="dtoId">Идентификатор DTO</param>
    void Delete(TKey dtoId);
}
