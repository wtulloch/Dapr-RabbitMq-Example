using System.Collections.Immutable;
using System.Net.Sockets;
using Aspire.Hosting;
using Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

//Uncomment the following to try with rabbitmq from Aspire

//builder.AddRabbitMQContainer("messaging", password: "ThisIsAPassword")
//    .WithEndpoint(containerPort: 5672, hostPort: 5672, scheme: "http");


builder.AddDapr();

builder.AddProject<Projects.MessagePublisher>("MessagePublisher")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "message-publisher-service",
        ResourcesPaths = ImmutableHashSet.Create("../components/"),
        LogLevel = "warn"
    });

builder.AddProject<Projects.MessageReceiver>("MessageReceiver")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "message-receiver-service",
        ResourcesPaths = ImmutableHashSet.Create("../components/"),
        LogLevel = "warn"
    });

builder.Build().Run();
