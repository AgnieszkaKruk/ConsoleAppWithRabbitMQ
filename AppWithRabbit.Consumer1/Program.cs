using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;
using Newtonsoft.Json;

using AppWithRabbitmq.Consumer;


class Program
{
    static void Main()
    {
        Console.Title = "Console App with Rabbitmq - Consumer";
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "email_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(body);

               
                var emailMessage = JsonConvert.DeserializeObject<AppWithRabbitmq.Producent.EMailMessage>(messageJson);

                Console.WriteLine("Received mail message: {0}", emailMessage.Subject);
                Console.WriteLine("Received mail message: {0}", emailMessage.Body);

                SendEmailMessage.SendEmail(emailMessage);

                channel.BasicAck(ea.DeliveryTag, false);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Done");
                Console.ForegroundColor = ConsoleColor.Gray;
            };

            channel.BasicConsume(queue: "email_queue",
                                 autoAck: false,
                                 consumer: consumer);

            Console.WriteLine("Waiting for mail messages. Press any key to exit.");
            Console.ReadKey(true);
        }
    }

  
}
