MassTransit

1) A free, open-source, lightweight message bus used to create distributed applications using .NET technologies.
2) An abstraction between the message brokers and the applications.
3) Suppports Rabbitmq, Azure Service Bus, and Amazon SQS/SNS etc.
4) Supports message patterns such as retry, circuit breaker, outbox.
5) Support for distribution transaction using Saga, event-driven state machines.
6) Build-In exception Handling

MessageTransit Methods

Publish() - Useful to send an event since an event can be observed by one or more listeners. 
Follows the Publish-Subscribe pattern.

When a message is published, it is not sent to a specific endpoint, but broadcasted to any consumers
which have subscribed to the message type.

Send() - Useful to send a command since a command need to be executed once. Need to specify the endpoint
i.e. which queue to go to use send method.

When a message is sent, it is delivered to a specific endpoint using a DestinationAddress.

MassTransit creates durable, fanout exchanges by default, and queues are also durable by default.

MassTransit encouranges and support the use of interfaces for message contracts, and initializers make
it easy to produce interface messages.

Message Brokers

kafka and RabbitMQ both are free - open source

Azure Service Bus and Amazon SQS both are - Cloud Managed
