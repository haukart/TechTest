using System.Text;
using EmitEvent;
using RabbitMQ.Client;

namespace EmitEvent
{
    public class EmitEventLog : IEmitEventLog
    {
        public void EventPublish(string eventName, string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "event",
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

                var output = eventName + " - " + message;

                var data = Encoding.UTF8.GetBytes(output);

                channel.BasicPublish(exchange: "logs",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: data);
            }
        }
    }
}
