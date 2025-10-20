var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.GrpcExample_Server>("grpcexample-server");

builder.AddProject<Projects.GrpcExample_Client>("grpcexample-client");

builder.Build().Run();
