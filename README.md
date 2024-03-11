# Dapr with RabbitMQ in Aspire example

## Introduction
The purpose of this project is to investigate running RabbitMQ as part of an Aspire project with the RabbitMQ instance being available as the message broker for Dapr pub/sub.

The simple fact is that this isn't currently working and when the Dapr side cars start they end up in a failed state. 

## About the project
This project consists of the following projects:
- Dapr-RabbitMq-Example.AppHost. This is the Aspire project used to launching the two other projects.
- MessagePublisher. A simple web API that takes a message and publishes it.
- MessReceiver. A simple Web API that subscribes and listens for published messages and prints the result to the console.

For Dapr this project is using version 1.13. The folder `compontents` contains the configuration files used by Dapr to set the pubsub message broker and define the subscriptions.

## Validate the project works.
To validate the everything is working we launch RabbitMq separately using the following docker command:
```
docker run -d --hostname simple-rabbit --name simple-rabbit -p 15672:15672 -p 5672:5672 -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=ThisIsAPassword rabbitmq:3-management

```

Once the container is running run the AppHost project which start Aspire and spin up the two Apis an their Dapr side-cars.

In the `MessagePublisher` open the MessagePublisher.http file and click `send request`. Then open the console logs for `MessageReceiver` and confirm the message was received.

## Testing with RabbitMQ from Aspire.

Stop the external RabbitMQ instance and then in the program file in the AppHost uncomment the code for running the RabbitMq Container. Run the application.

After a short period you will see that dapr side-cars fail.


