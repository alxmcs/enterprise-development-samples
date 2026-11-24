using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace TattooParlor.ServiceDefaults.Metrics;

public class GeneratorApiMeter : IApiMeter
{
    private readonly Counter<long> _apiCounter;

    public GeneratorApiMeter(IMeterFactory factory)
    {
        var meter = factory.Create(nameof(GeneratorApiMeter));
        _apiCounter = meter.CreateCounter<long>("generator.api.counter", "calls", "Counts REST API calls and groups them by controller method");
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