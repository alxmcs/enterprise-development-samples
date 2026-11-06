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

    var kafka = builder.AddKafka("tattooparlor-kafka")
        .WithKafkaUI();

    var kafkaGenerator = builder.AddProject<Projects.TattooParlor_Generator_Kafka>("tattooparlor-generator-kafka")
        .WithEnvironment("Kafka:TopicName", kafkaTopic)
        .WithReference(kafka)
        .WaitFor(kafka);

    apiHost.WithEnvironment("Kafka:TopicName", kafkaTopic)
        .WithReference(kafka)
        .WaitFor(kafka);
}
if (builder.Environment.EnvironmentName == "RabbitMQ")
{
    var rabbitUserName = builder.AddParameter("RabbitMQLogin");
    var rabbitPassword = builder.AddParameter("RabbitMQPassword");
    var rabbitMqExchange = builder.AddParameter("RabbitMQExchangeName");
    var rabbitMQCanceledRoutingKey = builder.AddParameter("RabbitMQConfirmedRoutingKey");
    var rabbitMQConfirmedRoutingKey = builder.AddParameter("RabbitMQCanceledRoutingKey");
    var rabbitMQConfirmedQueueName = builder.AddParameter("RabbitMQConfirmedQueueName");
    var rabbitMQCanceledQueueName = builder.AddParameter("RabbitMQCanceledQueueName");

    var rabbitMq = builder.AddRabbitMQ("tattooparlor-rabbitmq", userName: rabbitUserName, password: rabbitPassword)
        .WithManagementPlugin();

    builder.AddProject<Projects.TattooParlor_Generator_Rabbitmq>("tattooparlor-generator-rabbitmq")
        .WithEnvironment("RabbitMQ:ExchangeName", rabbitMqExchange)
        .WithEnvironment("RabbitMQ:ConfirmedQueueName", rabbitMQCanceledRoutingKey)
        .WithEnvironment("RabbitMQ:CanceledQueueName", rabbitMQConfirmedRoutingKey)
        .WithEnvironment("RabbitMQ:ConfirmedRoutingKey", rabbitMQConfirmedQueueName)
        .WithEnvironment("RabbitMQ:CanceledRoutingKey", rabbitMQCanceledQueueName)
        .WithReference(rabbitMq)
        .WaitFor(rabbitMq);

    apiHost.WithEnvironment("RabbitMq:QueueName", rabbitMQConfirmedQueueName)
        .WithReference(rabbitMq)
        .WaitFor(rabbitMq);
}
if (builder.Environment.EnvironmentName == "Nats")
{
    var natsUserName = builder.AddParameter("NatsLogin");
    var natsPassword = builder.AddParameter("NatsPassword");
    var natsStream = builder.AddParameter("NatsStream");
    var natsSubjectConfirmed = builder.AddParameter("NatsConfirmedSubjectName");
    var natsSubjectCancelled = builder.AddParameter("NatsCanceledSubjectName");

    var nats = builder.AddNats("tattooparlor-nats", userName: natsUserName, password: natsPassword, port: 4222)
        .WithJetStream()
        .WithArgs("-m", "8222")
        .WithHttpEndpoint(port: 8222, targetPort: 8222);

    builder.AddContainer("tattooparlor-nui", "ghcr.io/nats-nui/nui")
        .WithReference(nats)
        .WaitFor(nats)
        .WithHttpEndpoint(port: 31311, targetPort: 31311);

    builder.AddProject<Projects.TattooParlor_Generator_Nats>("tattooparlor-generator-nats")
        .WithEnvironment("Nats:StreamName", natsStream)
        .WithEnvironment("Nats:CanceledSubjectName", natsSubjectCancelled)
        .WithEnvironment("Nats:ConfirmedSubjectName", natsSubjectConfirmed)
        .WithReference(nats)
        .WaitFor(nats);

    apiHost.WithEnvironment("Nats:SubjectName", natsSubjectConfirmed)
        .WithEnvironment("Nats:StreamName", natsStream)
        .WithReference(nats)
        .WaitFor(nats);
}
if (builder.Environment.EnvironmentName == "gRPC")
{

}



builder.Build().Run();
