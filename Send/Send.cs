using System;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    Message msg = new Message();
                    msg.Seen = false;
                    msg.Id = 1;
                    msg.Comment="Yeni mesage";
                    var message = JsonConvert.SerializeObject(msg);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);

                    Console.WriteLine("[x] Sent {0}", msg.Id);
                }

                Console.WriteLine("Press Enter to exit");
                Console.ReadLine();
            }
            Console.WriteLine("Stoped");
        }
    }
}
