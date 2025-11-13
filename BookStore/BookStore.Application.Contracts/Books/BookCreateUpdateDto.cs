namespace BookStore.Application.Contracts.Books;
/// <summary>
/// DTO для POST/PUT запросов к книгам
/// </summary>
/// <param name="Title">Название издания</param>
/// <param name="Annotation">Аннотация</param>
/// <param name="PageCount">Число страниц</param>
/// <param name="Year">Год издания</param>
/// <param name="Publisher">Издательство</param>
/// <param name="Isbn">ISBN</param>
public record BookCreateUpdateDto(string? Title, string? Annotation, int? PageCount, int? Year, string? Publisher, string? Isbn);
