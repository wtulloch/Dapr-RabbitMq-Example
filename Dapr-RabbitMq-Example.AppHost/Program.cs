using System.Collections.Immutable;
using Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

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
