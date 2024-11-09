namespace BookStore.Contracts.Book;

/// <summary>
/// Dto для создания или изменения издания
/// </summary>
/// <param name="Title">Название</param>
/// <param name="Annotation">Аннотация</param>
/// <param name="PageCount">Число страниц</param>
/// <param name="Year">Год</param>
/// <param name="Publisher">Издательство</param>
/// <param name="Isbn">ISBN</param>
public record BookCreateUpdateDto(string? Title, string? Annotation, int? PageCount, int? Year, string? Publisher, string? Isbn);
