var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MessagePublisher>("messagepublisher");

builder.AddProject<Projects.MessageReceiver>("messagereceiver");

builder.Build().Run();
