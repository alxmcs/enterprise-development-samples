using TattooParlor.Application.Contracts.TattooSessions;

namespace TattooParlor.Generator.Infrasturucture;

/// <summary>
/// Интерфес службы, занимающейся отправкой сообщений по шине
/// </summary>
public interface IProducerService
{
    /// <summary>
    /// Метод для отправки коллекции контрактов
    /// </summary>
    /// <param name="batch">Коллекция контрактов</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task SendAsync(IList<TattooSessionCreateUpdateDto> batch, CancellationToken cancellationToken);
}