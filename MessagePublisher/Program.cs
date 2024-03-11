using Dapr.Client;
using SampleMessages;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddDaprClient();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/", async (DaprClient daprClient, SimpleMessage message, CancellationToken cancellationToken) =>
{
    var newMessage = message with
    {
        Name = message.Name.ToUpper()
    };

    await daprClient.PublishEventAsync<SimpleMessage>("pubsub", "messages", newMessage,
        cancellationToken: cancellationToken);

    Console.WriteLine("message published");
});

app.Run();

