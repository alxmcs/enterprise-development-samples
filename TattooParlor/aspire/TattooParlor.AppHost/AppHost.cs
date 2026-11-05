var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("DatabasePassword");
var batchSize = builder.AddParameter("GeneratorBatchSize");
var payloadLimit = builder.AddParameter("GeneratorPayloadLimit");
var waitTime = builder.AddParameter("GeneratorWaitTime");
const string dbName = "tattoo-parlor";

var postgresHost = builder
    .AddPostgres("tattooparlor-postgres", password: password, port: 2345)
    .WithPgWeb()
    .WithEnvironment("POSTGRES_DB", dbName);
var postgresDb = postgresHost.AddDatabase(dbName);

var apiHost = builder.AddProject<Projects.TattooParlor_Api>("tattooparlor-api")
    .WithReference(postgresDb, "Database")
    .WaitFor(postgresDb);

if (builder.Environment.EnvironmentName == "Kafka")
{
    var kafkaTopic = builder.AddParameter("KafkaTopic");

    var kafka = builder.AddKafka("bookstore-kafka")
    .WithKafkaUI();

    apiHost.WithEnvironment("Kafka:TopicName", kafkaTopic)
    .WithReference(kafka)
    .WaitFor(kafka);
}
if (builder.Environment.EnvironmentName == "RabbitMQ")
{
    var rabbitUserName = builder.AddParameter("RabbitMQLogin");
    var rabbitPassword = builder.AddParameter("RabbitMQPassword");
    var rabbitMqQueue = builder.AddParameter("RabbitMQQueue");

    var rabbitMq = builder.AddRabbitMQ("bookstore-rabbitmq", userName: rabbitUserName, password: rabbitPassword)
        .WithManagementPlugin();

    apiHost.WithEnvironment("RabbitMq:QueueName", rabbitMqQueue)
        .WithReference(rabbitMq)
        .WaitFor(rabbitMq);
}
if (builder.Environment.EnvironmentName == "Nats")
{
    var natsUserName = builder.AddParameter("NatsLogin");
    var natsPassword = builder.AddParameter("NatsPassword");
    var natsStream = builder.AddParameter("NatsStream");
    var natsSubject = builder.AddParameter("NatsSubject");

    var nats = builder.AddNats("bookstore-nats", userName: natsUserName, password: natsPassword, port: 4222)
        .WithJetStream()
        .WithArgs("-m", "8222")
        .WithHttpEndpoint(port: 8222, targetPort: 8222);

    builder.AddContainer("bookstore-nui", "ghcr.io/nats-nui/nui")
     .WithReference(nats)
     .WaitFor(nats)
     .WithHttpEndpoint(port: 31311, targetPort: 31311);

    apiHost.WithEnvironment("Nats:SubjectName", natsSubject)
        .WithEnvironment("Nats:StreamName", natsStream)
        .WithReference(nats)
        .WaitFor(nats);
}
if (builder.Environment.EnvironmentName == "gRPC")
{

}

builder.AddProject<Projects.TattooParlor_Generator_Kafka>("tattooparlor-generator-kafka");

builder.Build().Run();
