namespace BookStore.Application.Contracts.Books;
public record BookDto(int Id, string? Title, string? Annotation, int? PageCount, int? Year, string? Publisher, string? Isbn);
