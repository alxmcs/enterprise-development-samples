namespace BookStore.ServiceDefaults.Metrics;
public interface IApiMeter
{
    public void RecordCall(string controller, string method, string verb, string code);
}
