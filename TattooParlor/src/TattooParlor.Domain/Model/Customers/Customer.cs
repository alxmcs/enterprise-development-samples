using TattooParlor.Domain.Model.Common;

namespace TattooParlor.Domain.Model.Customers;

public class Customer : Person
{
    public required DateOnly FirstVisitDate { get; set; }
    public int VisitCount { get; set; }
    public bool HasAllergies { get; set; }
}
