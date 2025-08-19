using BookStore.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var postgresPassword = builder.AddParameter("PostgresPassword");
var postgresUserName = builder.AddParameter("PostgresLogin");
var dbName = "book-store";
var bookStoreDb = builder
    .AddPostgres("bookstore-postgres", userName: postgresUserName, password: postgresPassword)
    .AddDatabase(dbName);

var apiHost = builder.AddProject<Projects.BookStore_Api_Host>("bookstore-api-host")
    .WithReference(bookStoreDb, "Database")
    .WaitFor(bookStoreDb)
    .WithEnvironment("MessageBroker", builder.Environment.EnvironmentName);

var batchSize = builder.AddParameter("GeneratorBatchSize");
var payloadLimit = builder.AddParameter("GeneratorPayloadLimit");
var waitTime = builder.AddParameter("GeneratorWaitTime");

if (builder.Environment.IsRabbitMq())
{
    var rabbitUserName = builder.AddParameter("RabbitMQLogin");
    var rabbitPassword = builder.AddParameter("RabbitMQPassword");
    var rabbitMq = builder.AddRabbitMQ("book-store-rabbitmq", rabbitUserName, rabbitPassword)
        .WithManagementPlugin();

    var rabbiMqQueue = builder.AddParameter("RabbitMQQueue");
    builder.AddProject<Projects.BookStore_Generator_RabbitMq_Host>("bookstore-generator-rabbitmq-host")
        .WithReference(rabbitMq)
        .WaitFor(rabbitMq)
        .WithEnvironment("Generator:BatchSize", batchSize)
        .WithEnvironment("Generator:PayloadLimit", payloadLimit)
        .WithEnvironment("Generator:WaitTime", waitTime)
        .WithEnvironment("RabbitMq:QueueName", rabbiMqQueue);

    apiHost.WithEnvironment("RabbitMq:QueueName", rabbiMqQueue)
        .WithReference(rabbitMq)
        .WaitFor(rabbitMq);
}
if (builder.Environment.IsKafka())
{

    var kafka = builder.AddKafka("book-store-kafka")
        .WithKafkaUI();

    var kafkaTopic = builder.AddParameter("KafkaTopic");
    builder.AddProject<Projects.BookStore_Generator_Kafka_Host>("bookstore-generator-kafka-host")
        .WithReference(kafka)
        .WaitFor(kafka)
        .WithEnvironment("Generator:BatchSize", batchSize)
        .WithEnvironment("Generator:PayloadLimit", payloadLimit)
        .WithEnvironment("Generator:WaitTime", waitTime)
        .WithEnvironment("Kafka:TopicName", kafkaTopic);

    apiHost.WithEnvironment("Kafka:TopicName", kafkaTopic)
        .WithReference(kafka)
        .WaitFor(kafka);
}
builder.Build().Run();
