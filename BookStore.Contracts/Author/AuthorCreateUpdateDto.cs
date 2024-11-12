namespace BookStore.Contracts.Author;

/// <summary>
/// Dto для создания или изменения автора
/// </summary>
/// <param name="LastName">Фамилия</param>
/// <param name="FirstName">Имя</param>
/// <param name="Patronymic">Отчество</param>
/// <param name="Biography">Биография</param>
public record AuthorCreateUpdateDto(string? LastName, string? FirstName, string? Patronymic, string? Biography);
