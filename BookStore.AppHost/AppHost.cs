var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("DatabasePassword");
var dbName = "book-store";
var bookStoreDb = builder
    .AddPostgres("bookstore-postgres", password: password)
    .AddDatabase(dbName);

builder.AddProject<Projects.BookStore_Api_Host>("bookstore-api-host")
    .WithReference(bookStoreDb, "Database")
    .WaitFor(bookStoreDb);

builder.Build().Run();
