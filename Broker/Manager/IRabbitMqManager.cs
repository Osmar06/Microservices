using Broker.Message;
using Broker.Message.Base;
using RabbitMQ.Client;
using System;

namespace Broker.Manager
{
    public interface IRabbitMqManager
    {
        #region Public Methods

        void PublishBilling(BillingMessage message);

        void PublishDelivery(DeliveryMessage message);

        void PublishSale(SaleMessage message);

        void ProcessMessage<T>(string queueName, Func<MessageBase, bool> onRecive = null) where T : MessageBase;

        IModel GetChannel();

        #endregion Public Methods
    }
}