var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("DatabasePassword");
const string  dbName = "tattoo-parlor";

var postgresHost = builder
    .AddPostgres("tattooparlor-postgres", password: password, port: 2345)
    .WithPgWeb()
    .WithEnvironment("POSTGRES_DB", dbName);
var postgresDb = postgresHost.AddDatabase(dbName);

builder.AddProject<Projects.TattooParlor_Api>("tattooparlor-api")
    .WithReference(postgresDb, "Database")
    .WaitFor(postgresDb);

builder.Build().Run();
