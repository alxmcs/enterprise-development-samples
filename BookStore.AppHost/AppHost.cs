var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BookStore_Api_Host>("bookstore-api-host");

builder.Build().Run();
