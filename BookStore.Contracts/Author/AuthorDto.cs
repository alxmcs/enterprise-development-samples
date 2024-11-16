namespace BookStore.Contracts.Author;

/// <summary>
/// Dto для просмотра сведений об авторе
/// </summary>
public class AuthorDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
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
    /// <summary>
    /// Число работ
    /// </summary>
    public int? WorkCount { get; set; }
}
