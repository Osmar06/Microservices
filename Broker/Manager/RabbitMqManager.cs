using Broker.Message;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
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

        public IModel GetChannel() => channel;

        #endregion Public Constructors

        #region Public Methods

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