using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShoppingCartApp.Shared.Domain;
using System.Text;

namespace ShoppingCartApp.Shared.Infrastructure;

public class RabbitMQEventBus : IEventBus, IDisposable
{
    private IDictionary<Type, List<object>> handlers;

    private IConnection connection;
    private RabbitMQ.Client.IModel channel;
    private string exchangeName = "DomainEvents";

    public RabbitMQEventBus(IConfiguration configuration)
    {
        handlers = new Dictionary<Type, List<object>>();
        var factory = new ConnectionFactory()
        {
            HostName = configuration["RabbitMQ:Host"],
            Port = int.Parse(configuration["RabbitMQ:Port"]),
            UserName = configuration["RabbitMQ:Username"],
            Password = configuration["RabbitMQ:Password"]
        };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
    }

    public Task Publish<T>(IReadOnlyCollection<T> domainEvents) where T : IDomainEvent
    {
        foreach (var domainEvent in domainEvents)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(domainEvent));
            var eventFullPathName = domainEvent.GetType().ToString();
            var routingKey = eventFullPathName.Substring(eventFullPathName?.LastIndexOf(".") + 1 ?? 0);
            channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);
        }

        return Task.CompletedTask;
    }

    public void Subscribe<T>(IEventHandler<T> eventHandler) where T : IDomainEvent
    {
        Type eventType = typeof(T);
        if (!handlers.ContainsKey(eventType))
            handlers.Add(eventType, new List<object>());

        handlers[eventType].Add(eventHandler);

    }

    public void StartConsuming(string queueName, Type eventType)
    {
        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, args) =>
        {
            var message = Encoding.UTF8.GetString(args.Body.ToArray());
            dynamic domainEvent = Activator.CreateInstance(eventType) ?? throw new InvalidOperationException();

            JsonConvert.PopulateObject(message, domainEvent);
            var eventHandlers = handlers[eventType];
            foreach (var eventHandler in eventHandlers)
            {
                dynamic concreteEventHandler = eventHandler;
                await concreteEventHandler.Handle(domainEvent);
            }
            // Process the received message
            Console.WriteLine("Received Message: " + message);
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }

    public void Dispose()
    {
        channel?.Dispose();
        connection?.Dispose();
    }

    
}