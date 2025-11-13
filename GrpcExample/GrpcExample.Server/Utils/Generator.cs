using Bogus;
using GrpcExample.Protos;

namespace GrpcExample.Server.Utils;

public static class Generator
{
    public static Sample GenerateById(int id) =>
        new Faker<Sample>()
            .RuleFor(s => s.SampleId, f => id)
            .RuleFor(s => s.SampleName, f => f.Commerce.ProductName())
            .RuleFor(s => s.SampleValue, f=>f.Random.Double(-100,500))
            .RuleFor(s => s.SampleFlag, f => f.Random.Bool())
            .Generate();

    public static List<Sample> GenerateByCount(int count) =>
        new Faker<Sample>()
            .RuleFor(s => s.SampleId, f => f.Random.Int(1, count))
            .RuleFor(s => s.SampleName, f => f.Commerce.ProductName())
            .RuleFor(s => s.SampleValue, f => f.Random.Double(-100, 500))
            .RuleFor(s => s.SampleFlag, f => f.Random.Bool())
            .Generate(count);

    public static Sample GenerateSingle() =>
        new Faker<Sample>()
            .RuleFor(s => s.SampleId, f => f.Random.Int(1, 100))
            .RuleFor(s => s.SampleName, f => f.Commerce.ProductName())
            .RuleFor(s => s.SampleValue, f => f.Random.Double(-100, 500))
            .RuleFor(s => s.SampleFlag, f => f.Random.Bool())
            .Generate();
}
