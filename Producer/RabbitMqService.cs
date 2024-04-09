using System.Text;
using Producer.Interfaces;
using System.Text.Json;
using RabbitMQ.Client;

namespace Producer;

public class RabbitMqService : IRabbitMqService
{
    public void SendMessage(object obj)
    {
        SendMessage(JsonSerializer.Serialize(obj));
    }

    public void SendMessage(string message)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "MyQueue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
            routingKey: "MyQueue",
            basicProperties: null,
            body: body);
    }
}