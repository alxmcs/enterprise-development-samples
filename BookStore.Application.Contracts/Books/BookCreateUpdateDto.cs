namespace BookStore.Application.Contracts.Books;
public record BookCreateUpdateDto(string? Title, string? Annotation, int? PageCount, int? Year, string? Publisher, string? Isbn);
