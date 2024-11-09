namespace BookStore.Contracts.Author;

/// <summary>
/// Dto для просмотра сведений об авторе
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="LastName">Фамилия</param>
/// <param name="FirstName">Имя</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="Biography">Биография</param>
/// <param name="WorkCount">Число работ</param>
public record AuthorDto(int Id, string? LastName, string? FirstName, string? Patronymic, string? Biography, int? WorkCount);
