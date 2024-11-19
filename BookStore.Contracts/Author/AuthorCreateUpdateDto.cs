namespace BookStore.Contracts.Author;

/// <summary>
/// Dto для создания или изменения автора
/// </summary>
public class AuthorCreateUpdateDto
{
    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; set; }
    /// <summary>
    /// Имя
    /// </summary>
    public string? FirstName { get; set; }
    /// <summary>
    /// Отчество
    /// </summary>
    public string? Patronymic { get; set; }
    /// <summary>
    /// Биография
    /// </summary>
    public string? Biography { get; set; }
}
