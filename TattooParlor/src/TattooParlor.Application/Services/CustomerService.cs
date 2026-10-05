using MapsterMapper;
using TattooParlor.Application.Contracts.Customers;
using TattooParlor.Application.Contracts.TattooSessions;
using TattooParlor.Domain.Model.Customers;
using TattooParlor.Domain.Shared.Enums;

namespace TattooParlor.Application.Services;

public class CustomerService(ICustomerManager manager, IMapper mapper) : ICustomersService
{
    /// <inheritdoc/>
    public CustomerDto Create(CustomerCreateUpdateDto dto)
    {
        var result = manager.Create(mapper.Map<Customer>(dto));
        return mapper.Map<CustomerDto>(result);
    }

    /// <inheritdoc/>
    public bool Delete(int dtoId) => manager.Delete(dtoId);

    /// <inheritdoc/>
    public CustomerDto? Get(int dtoId)
    {
        var result = manager.Read(dtoId);
        return result is null
            ? throw new KeyNotFoundException($"Customer with Id {dtoId} not found.")
            : mapper.Map<CustomerDto>(result);
    }

    /// <inheritdoc/>
    public IList<CustomerDto> GetAll() => mapper.Map<IList<CustomerDto>>(manager.ReadAll());

    /// <inheritdoc/>
    public IList<CustomerDto> GetCustomersWithStyle(TattooStyle style) 
        => mapper.Map<IList<CustomerDto>>(manager.ShowCustomersWithStyle(style));

    /// <inheritdoc/>
    public IList<TattooSessionDto> GetSessions(CustomerDto customer) 
        => mapper.Map<IList<TattooSessionDto>>(manager.Sessions(mapper.Map<Customer>(customer)));

    /// <inheritdoc/>
    public IList<CustomerVisitsDto> GetTop5CustomersByVisits(DateOnly startDate, DateOnly endDate) 
        => mapper.Map<IList<CustomerVisitsDto>>(manager.ShowTop5CustomersByVisits(startDate, endDate));

    /// <inheritdoc/>
    public CustomerDto Update(CustomerCreateUpdateDto dto, int dtoId)
    {
        var entity = mapper.Map<Customer>(dto);
        entity.Id = dtoId;
        var result = manager.Update(entity);
        return mapper.Map<CustomerDto>(result);
    }
}
