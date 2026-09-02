namespace TattooParlor.Domain.Model.Common;

public abstract class Person
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}
