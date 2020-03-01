using Broker.Extension;
using Broker.Message;
using Broker.Message.Base;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace Broker.Manager
{
    public class RabbitMqManager : IRabbitMqManager
    {
        #region Private Fields

        private readonly IModel channel;
        private readonly DefaultObjectPool<IModel> objectPool;

        #endregion Private Fields

        #region Public Constructors

        public RabbitMqManager(IPooledObjectPolicy<IModel> objectPolicy)
        {
            objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);
            channel = GetConnection();
        }

        #endregion Public Constructors

        #region Public Methods

        public IModel GetChannel() => channel;

        public void ProcessMessage<T>(string queueName, Func<MessageBase, bool> onRecive = null) where T : MessageBase
        {
            var channel = GetChannel();
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (ch, ea) =>
            {
                var body = Encoding.Default.GetString(ea.Body);
                var message = JsonSerializer.Deserialize<T>(body);

                Console.WriteLine($"\nRouting Key <{ea.RoutingKey}>");
                Console.WriteLine($"---- Queue {queueName} Info ---- \n");

                var values = message.ToDictionary();
                foreach (var messageValue in values)
                    Console.WriteLine($"{messageValue.Key}: {messageValue.Value}");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nEnd of message\n");
                Console.ResetColor();

                onRecive?.Invoke(message);

                channel.BasicAck(ea.DeliveryTag, false);
            };

            var consumerTag = channel.BasicConsume(queueName, false, consumer);
            Console.WriteLine($"Consumer: {consumerTag}");
        }

        public void PublishBilling(BillingMessage message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            Publish(messageBytes, RabbitMqConstants.BillingRouteKey);
        }

        public void PublishDelivery(DeliveryMessage message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            Publish(messageBytes, RabbitMqConstants.DeliveryRouteKey);
        }

        public void PublishSale(SaleMessage message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            Publish(messageBytes, RabbitMqConstants.SalesRouteKey);
        }

        #endregion Public Methods

        #region Private Methods

        private IModel GetConnection()
        {
            var channel = objectPool.Get();

            channel.ExchangeDeclare(RabbitMqConstants.ExchangeName, ExchangeType.Topic, true, false, null);

            channel.QueueDeclare(RabbitMqConstants.SalesQueueName, true, false, false, null);
            channel.QueueDeclare(RabbitMqConstants.BillingQueueName, true, false, false, null);
            channel.QueueDeclare(RabbitMqConstants.DeliveryQueueName, true, false, false, null);

            channel.QueueBind(RabbitMqConstants.SalesQueueName, RabbitMqConstants.ExchangeName, RabbitMqConstants.SalesRouteKey);
            channel.QueueBind(RabbitMqConstants.BillingQueueName, RabbitMqConstants.ExchangeName, RabbitMqConstants.BillingRouteKey);
            channel.QueueBind(RabbitMqConstants.DeliveryQueueName, RabbitMqConstants.ExchangeName, RabbitMqConstants.DeliveryRouteKey);

            return channel;
        }

        private void Publish(byte[] message, string routeKey)
        {
            if (message == null)
                return;

            try
            {
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(RabbitMqConstants.ExchangeName, routeKey, properties, message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objectPool.Return(channel);
            }
        }

        #endregion Private Methods
    }
}