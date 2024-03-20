using System.Collections.Immutable;
using Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);


builder.AddContainer("test-rabbit", "rabbitmq", "3-management")
    .WithEnvironment("RABBITMQ_DEFAULT_PASS", "ThisIsAPassword")
    .WithEnvironment("RABBITMQ_DEFAULT_USER", "user")
    .WithEndpoint(scheme: "tcp", hostPort: 5672, containerPort: 5672,  isProxied: false)
    .WithEndpoint(scheme: "http", hostPort: 15672, containerPort:15672, isProxied: false);

builder.AddDapr();

builder.AddProject<Projects.MessagePublisher>("publisher")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "message-publish-service",
        ResourcesPaths = ImmutableHashSet.Create("../components/"),
        LogLevel = "warn"
    });
   

builder.AddProject<Projects.MessageReceiver>("subscriber")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "message-receiver-service",
        ResourcesPaths = ImmutableHashSet.Create("../components/"),
        LogLevel = "warn"
    });



builder.Build().Run();
