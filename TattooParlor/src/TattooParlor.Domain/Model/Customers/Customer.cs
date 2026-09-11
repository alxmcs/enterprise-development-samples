using TattooParlor.Domain.Model.Common;

namespace TattooParlor.Domain.Model.Customers;

/// <summary>
/// Клиент тату-салона
/// </summary>
public class Customer : Person
{
    /// <summary>
    /// Дата первого визита
    /// </summary>
    public required DateOnly FirstVisitDate { get; set; }

    /// <summary>
    /// Число визита
    /// </summary>
    public int VisitCount { get; set; }

    /// <summary>
    /// Наличие аллергий
    /// </summary>
    public bool HasAllergies { get; set; }
}
