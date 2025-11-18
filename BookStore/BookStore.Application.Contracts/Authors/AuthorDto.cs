namespace BookStore.Application.Contracts.Authors;
/// <summary>
/// DTO для GET запросов к авторам
/// </summary>
/// <param name="Id">Идентификатор автора</param>
/// <param name="LastName">Имя автора</param>
/// <param name="FirstName">Фамилия автора</param>
/// <param name="Patronymic">Отчество автора</param>
/// <param name="Biography">Биография автора</param>
public record AuthorDto(int Id, string? LastName, string? FirstName, string? Patronymic, string? Biography);
