using System.ComponentModel.DataAnnotations.Schema;
using TattooParlor.Domain.Model.Common;
using TattooParlor.Domain.Model.TattooSessions;

namespace TattooParlor.Domain.Model.Customers;

/// <summary>
/// Клиент тату-салона
/// </summary>
[Table("customers", Schema = "tp")]
public class Customer : Person
{
    /// <summary>
    /// Дата первого визита
    /// </summary>
    [Column("first_visit_date")]
    public required DateOnly FirstVisitDate { get; set; }

    /// <summary>
    /// Число визитов
    /// </summary>
    [Column("visit_count")]
    public int VisitCount { get; set; }

    /// <summary>
    /// Наличие аллергий
    /// </summary>
    [Column("has_allergies")]
    public bool HasAllergies { get; set; }

    /// <summary>
    /// Сессии клиента
    /// </summary>
    public virtual ICollection<TattooSession> Sessions { get; set; } = [];
}
