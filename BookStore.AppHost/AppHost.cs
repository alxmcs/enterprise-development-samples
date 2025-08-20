using BookStore.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var postgresPassword = builder.AddParameter("PostgresPassword");
var postgresUserName = builder.AddParameter("PostgresLogin");
var dbName = "book-store";
var bookStoreDb = builder
    .AddPostgres("book-store-postgres", userName: postgresUserName, password: postgresPassword)
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
    var rabbitMq = builder.AddRabbitMQ("book-store-rabbitmq", userName: rabbitUserName, password: rabbitPassword)
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
if (builder.Environment.IsNats())
{
    var natsUserName = builder.AddParameter("NatsLogin");
    var natsPassword = builder.AddParameter("NatsPassword");
    var nats = builder.AddNats("book-store-nats", userName: natsUserName, password: natsPassword)
        .WithJetStream()
        .WithArgs("-m", "8222")
        .WithHttpEndpoint(port: 8222, targetPort: 8222);

    builder.AddContainer("book-store-nui", "ghcr.io/nats-nui/nui")
        .WaitFor(nats)
        .WithHttpEndpoint(port: 31311, targetPort: 31311);

    var natsStream = builder.AddParameter("NatsStream");
    var natsSubject = builder.AddParameter("NatsSubject");
    builder.AddProject<Projects.BookStore_Generator_Nats_Host>("bookstore-generator-nats-host")
        .WithReference(nats)
        .WaitFor(nats)
        .WithEnvironment("Generator:BatchSize", batchSize)
        .WithEnvironment("Generator:PayloadLimit", payloadLimit)
        .WithEnvironment("Generator:WaitTime", waitTime)
        .WithEnvironment("Nats:StreamName", natsStream)
        .WithEnvironment("Nats:SubjectName", natsSubject);

    apiHost.WithEnvironment("Nats:SubjectName", natsSubject)
        .WithEnvironment("Nats:StreamName", natsStream)
        .WithReference(nats)
        .WaitFor(nats);
}

builder.Build().Run();
