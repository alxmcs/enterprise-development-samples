using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace BookStore.ServiceDefaults.Metrics;
public class BookStoreApiMeter : IApiMeter
{
    private readonly Counter<long> _apiCounter;

    public BookStoreApiMeter(IMeterFactory factory)
    {
        var meter = factory.Create(nameof(BookStoreApiMeter));
        _apiCounter = meter.CreateCounter<long>("bookstore.api.counter", "calls", "Counts REST API calls and groups them by controller method");
    }

    public void RecordCall(string controller, string method, string verb, string code)
    {
        var tags = new TagList
        {
            {"api.controller", controller },
            {"api.method", method },
            {"http.verb", verb },
            {"http.code", code }
        };

        _apiCounter.Add(1, tags);
    }
}