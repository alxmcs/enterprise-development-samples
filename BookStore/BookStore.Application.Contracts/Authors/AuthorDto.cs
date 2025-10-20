namespace BookStore.Application.Contracts.Authors;
public record AuthorDto(int Id, string? LastName, string? FirstName, string? Patronymic, string? Biography);
