using Microsoft.EntityFrameworkCore;
using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Model.TattooSessions;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core репозиторий для клиентов
/// </summary>
public class CustomerEfCoreRepository(TattooParlorDbContext context) : ICustomerManager
{
    /// <inheritdoc/>
    public async Task<Customer> Create(Customer entity)
    {
        context.Customers.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
        => await context.Customers.Where(c => c.Id == entityId).ExecuteDeleteAsync() > 0;

    /// <inheritdoc/>
    public async Task<Customer?> Read(int entityId)
        => await context.Customers.FirstOrDefaultAsync(c => c.Id == entityId);

    /// <inheritdoc/>
    public async Task<IList<Customer>> ReadAll()
        => await context.Customers.ToListAsync();

    /// <inheritdoc/>
    public async Task<IList<TattooSession>> Sessions(Customer entity)
        => await context.TattooSessions
            .Where(s => s.CustomerId == entity.Id)
            .ToListAsync();

    /// <inheritdoc/>
    public async Task<IList<Customer>> ShowCustomersWithStyle(TattooStyle style)
        => await context.TattooSessions
            .Where(s => s.Style.Style == style && s.CancelDate == null)
            .Select(s => s.Customer)
            .Distinct()
            .ToListAsync();

    /// <inheritdoc/>
    public async Task<IList<(Customer Customer, int Visits)>> ShowTop5CustomersByVisits(DateOnly startDate, DateOnly endDate)
    {
        var start = startDate.ToDateTime(TimeOnly.MinValue);
        var end = endDate.ToDateTime(TimeOnly.MaxValue);

        if (start > end)
            throw new ArgumentException("Дата начала не может быть позже даты окончания");

        var top = await context.TattooSessions
            .Where(s => s.SessionDate >= start && s.SessionDate <= end && s.CancelDate == null)
            .GroupBy(s => s.CustomerId)
            .Select(g => new { CustomerId = g.Key, Visits = g.Count() })
            .OrderByDescending(x => x.Visits)
            .Take(5)
            .ToListAsync();

        var ids = top.Select(x => x.CustomerId).ToList();
        var customers = await context.Customers.Where(c => ids.Contains(c.Id)).ToDictionaryAsync(c => c.Id);

        return [.. top.Select(x => (customers[x.CustomerId], x.Visits))];
    }

    /// <inheritdoc/>
    public async Task<Customer> Update(Customer entity)
    {
        var existing = await context.Customers.FirstOrDefaultAsync(c => c.Id == entity.Id)
            ?? throw new KeyNotFoundException($"Клиент с Id {entity.Id} не найден");
        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }
}
