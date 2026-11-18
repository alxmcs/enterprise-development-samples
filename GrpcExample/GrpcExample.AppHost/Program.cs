var builder = DistributedApplication.CreateBuilder(args);

var server = builder.AddProject<Projects.GrpcExample_Server>("grpcexample-server");

builder.AddProject<Projects.GrpcExample_Client>("grpcexample-client")
    .WithReference(server)
    .WaitFor(server);

builder.Build().Run();
