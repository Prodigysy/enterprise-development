var builder = DistributedApplication.CreateBuilder(args);

var realEstateAgencyDb = builder
    .AddPostgres("real-estate-agency-db")
    .AddDatabase("real-estate-agency");

var rabbitMqQueue = builder.AddParameter("RabbitMQQueue");
var rabbitMqUsername = builder.AddParameter("RabbitMQLogin");
var rabbitMqPassword = builder.AddParameter("RabbitMQPassword");

var rabbitMq = builder.AddRabbitMQ("real-estate-agency-rabbitmq", userName: rabbitMqUsername, password: rabbitMqPassword)
    .WithManagementPlugin();

builder.AddProject<Projects.RealEstateAgency_Api_Host>("realestateagency-api-host")
    .WithReference(realEstateAgencyDb, "DatabaseConnection")
    .WithReference(rabbitMq)
    .WithEnvironment("RabbitMq:QueueName", rabbitMqQueue)
    .WaitFor(realEstateAgencyDb)
    .WaitFor(rabbitMq);

builder.AddProject<Projects.RealEstateAgency_Generator_RabbitMq_Host>("realestateagency-generator-rabbitmq-host")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithEnvironment("RabbitMq:QueueName", rabbitMqQueue);

builder.Build().Run();
