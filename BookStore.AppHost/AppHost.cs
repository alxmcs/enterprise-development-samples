var builder = DistributedApplication.CreateBuilder(args);

var postgresPassword = builder.AddParameter("PostgresPassword");
var postgresUserName = builder.AddParameter("PostgresLogin");
var dbName = "bookstore";
var bookStoreDb = builder
    .AddPostgres("bookstore-postgres", userName: postgresUserName, password: postgresPassword)
    .AddDatabase(dbName);

var apiHost = builder.AddProject<Projects.BookStore_Api_Host>("bookstore-api-host")
    .WithReference(bookStoreDb, "Database")
    .WaitFor(bookStoreDb)
    .WithEnvironment("Generator", builder.Environment.EnvironmentName);

var batchSize = builder.AddParameter("GeneratorBatchSize");
var payloadLimit = builder.AddParameter("GeneratorPayloadLimit");
var waitTime = builder.AddParameter("GeneratorWaitTime");

if (builder.Environment.EnvironmentName == "RabbitMq")
{
    var rabbitUserName = builder.AddParameter("RabbitMQLogin");
    var rabbitPassword = builder.AddParameter("RabbitMQPassword");
    var rabbitMq = builder.AddRabbitMQ("bookstore-rabbitmq", userName: rabbitUserName, password: rabbitPassword)
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
if (builder.Environment.EnvironmentName == "Kafka")
{
    var kafka = builder.AddKafka("bookstore-kafka")
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
if (builder.Environment.EnvironmentName == "Nats")
{
    var natsUserName = builder.AddParameter("NatsLogin");
    var natsPassword = builder.AddParameter("NatsPassword");
    var nats = builder.AddNats("bookstore-nats", userName: natsUserName, password: natsPassword, port: 4222)
        .WithJetStream()
        .WithArgs("-m", "8222")
        .WithHttpEndpoint(port: 8222, targetPort: 8222);

    builder.AddContainer("bookstore-nui", "ghcr.io/nats-nui/nui")
        .WithReference(nats)
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
if (builder.Environment.EnvironmentName == "Grpc")
{
    var grpcServer = builder.AddProject<Projects.BookStore_Generator_Grpc_Host>("bookstore-generator-grpc-host")
        .WithReference(apiHost)
        .WithHttpsEndpoint(5000)
        .WithEnvironment("Generator:BatchSize", batchSize)
        .WithEnvironment("Generator:PayloadLimit", payloadLimit)
        .WithEnvironment("Generator:WaitTime", waitTime);

    apiHost.WithReference(grpcServer)
        .WaitFor(grpcServer);
}

builder.Build().Run();
