namespace BookStore.Application.Contracts.Authors;
public record AuthorCreateUpdateDto(string? LastName, string? FirstName, string? Patronymic, string? Biography);
