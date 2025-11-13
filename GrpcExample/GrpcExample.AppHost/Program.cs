var builder = DistributedApplication.CreateBuilder(args);

var client = builder.AddProject<Projects.GrpcExample_Client>("grpcexample-client");

builder.AddProject<Projects.GrpcExample_Server>("grpcexample-server")
    .WithReference(client)
    .WaitFor(client);


builder.Build().Run();
