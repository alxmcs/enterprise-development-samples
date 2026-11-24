namespace TattooParlor.ServiceDefaults.Metrics;

/// <summary>
/// Интерфейс для замера метрик
/// </summary>
public interface IApiMeter
{
    /// <summary>
    /// Метод для замера вызова эндпоинта апи
    /// </summary>
    /// <param name="controller">Вызываемый контроллер</param>
    /// <param name="method">Вызываемый</param>
    /// <param name="verb">Вызываемый</param>
    /// <param name="code">Вызываемый</param>
    public void RecordCall(string controller, string method, string verb, string code);
}
