var builder = DistributedApplication.CreateBuilder(args);

var batchSize = builder.AddParameter("GeneratorBatchSize");
var payloadLimit = builder.AddParameter("GeneratorPayloadLimit");
var waitTime = builder.AddParameter("GeneratorWaitTime");

var rabbitUserName = builder.AddParameter("RabbitMQLogin");
var rabbitPassword = builder.AddParameter("RabbitMQPassword");
var rabbitMq = builder.AddRabbitMQ("book-store-rabbitmq", rabbitUserName, rabbitPassword)
    .WithDataVolume(isReadOnly: false);

var rabbiMqQueue = builder.AddParameter("RabbitMQQueue");
builder.AddProject<Projects.BookStore_Generator_RabbitMq_Host>("bookstore-generator-rabbitmq-host")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithEnvironment("Generator:BatchSize", batchSize)
    .WithEnvironment("Generator:PayloadLimit", payloadLimit)
    .WithEnvironment("Generator:WaitTime", waitTime)
    .WithEnvironment("RabbitMq:QueueName", rabbiMqQueue);

var postgresPassword = builder.AddParameter("PostgresPassword");
var postgresUserName = builder.AddParameter("PostgresLogin");
var dbName = "book-store";
var bookStoreDb = builder
    .AddPostgres("bookstore-postgres",userName: postgresUserName, password: postgresPassword)
    .AddDatabase(dbName);

builder.AddProject<Projects.BookStore_Api_Host>("bookstore-api-host")
    .WithReference(bookStoreDb, "Database")
    .WaitFor(bookStoreDb)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithEnvironment("RabbitMq:QueueName", rabbiMqQueue);

builder.Build().Run();
