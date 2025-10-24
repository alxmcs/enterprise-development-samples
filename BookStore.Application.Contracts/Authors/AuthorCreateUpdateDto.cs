namespace BookStore.Application.Contracts.Authors;
/// <summary>
/// DTO для POST/PUT запросов к авторам
/// </summary>
/// <param name="LastName">Имя автора</param>
/// <param name="FirstName">Фамилия автора</param>
/// <param name="Patronymic">Отчество автора</param>
/// <param name="Biography">Биография автора</param>
public record AuthorCreateUpdateDto(string? LastName, string? FirstName, string? Patronymic, string? Biography);
