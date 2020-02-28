using Broker.Manager;
using Broker.Message;
using Broker.Pool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Sales
{
    public class MessageHandler
    {
        private readonly IRabbitMqManager rabbitMqManager;
        public MessageHandler()
        {
            var options = Options.Create(new RabbitMqOptions
            {
                HostName = "localhost",
                Password = "guest",
                Port = 5672,
                UserName = "guest",
                VHost = "/"
            });

            rabbitMqManager = new RabbitMqManager(new RabbitMqModelPooledObjectPolicy(options));
        }

        public void ProcessMessage()
        {
            var channel = rabbitMqManager.GetChannel();

            channel.BasicQos(0, 10, false);
            var subscription = new Subscription(channel,
                RabbitMqConstants.SalesQueueName, false);

            while (true)
            {
                var deliveryArguments = subscription.Next();
                var body = Encoding.Default.GetString(deliveryArguments.Body);
                var message = JsonSerializer.Deserialize<SaleMessage>(body);

                var routingKey = deliveryArguments.RoutingKey;

                Console.WriteLine($"\nSales - Routing Key <{routingKey}>");
                Console.WriteLine($"---- Sale Info ---- \n");
                Console.WriteLine($"Customer: {message.Customer}");
                Console.WriteLine($"Product: {message.Product}");
                Console.WriteLine($"Price: {message.Price:C2}");
                Console.WriteLine($"Quantity: {message.Quantity}");
                Console.WriteLine($"Sale Date: {message.SaleDate}");
                Console.WriteLine();

                subscription.Ack(deliveryArguments);
            }
        }
            }
}
