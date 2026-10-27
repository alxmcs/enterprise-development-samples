using MapsterMapper;
using TattooParlor.Application.Contracts.Customers;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Services;

/// <summary>
/// Аппликейшен служба для клиентов
/// </summary>
/// <param name="manager">Доменная служба для клиентов</param>
/// <param name="mapper">Маппер</param>
public class CustomerService(ICustomerManager manager, IMapper mapper) : ICustomerService
{
    /// <inheritdoc/>
    public async Task<CustomerDto> Create(CustomerCreateUpdateDto dto)
    {
        var result = await manager.Create(mapper.Map<Customer>(dto));
        return mapper.Map<CustomerDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int dtoId) => await manager.Delete(dtoId);

    /// <inheritdoc/>
    public async Task<CustomerDto?> Get(int dtoId)
    {
        var result = await manager.Read(dtoId);
        return result is null
            ? throw new KeyNotFoundException($"Клиент с Id {dtoId} не найден")
            : mapper.Map<CustomerDto>(result);
    }

    /// <inheritdoc/>
    public async Task<IList<CustomerDto>> GetAll() => mapper.Map<IList<CustomerDto>>(await manager.ReadAll());

    /// <inheritdoc/>
    public async Task<IList<CustomerDto>> GetCustomersWithStyle(TattooStyle style)
        => mapper.Map<IList<CustomerDto>>(await manager.ShowCustomersWithStyle(style));

    /// <inheritdoc/>
    public async Task<IList<TattooSessionDto>> GetSessions(CustomerDto customer)
        => mapper.Map<IList<TattooSessionDto>>(await manager.Sessions(mapper.Map<Customer>(customer)));

    /// <inheritdoc/>
    public async Task<IList<CustomerVisitsDto>> GetTop5CustomersByVisits(DateOnly startDate, DateOnly endDate)
        => mapper.Map<IList<CustomerVisitsDto>>(await manager.ShowTop5CustomersByVisits(startDate, endDate));

    /// <inheritdoc/>
    public async Task<CustomerDto> Update(CustomerCreateUpdateDto dto, int dtoId)
    {
        var entity = mapper.Map<Customer>(dto);
        entity.Id = dtoId;
        var result = await manager.Update(entity);
        return mapper.Map<CustomerDto>(result);
    }
}
