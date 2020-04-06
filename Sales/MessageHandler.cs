using Broker.Manager;
using Broker.Message;
using Broker.Message.Base;
using Broker.Pool;
using Broker.Util;
using Microsoft.Extensions.Options;
using System;

namespace Sales
{
    public class MessageHandler
    {
        #region Private Fields

        private readonly IRabbitMqManager rabbitMqManager;

        #endregion Private Fields

        #region Public Constructors

        public MessageHandler()
        {
            var options = Options.Create(BrokerUtil.GetRabbitMqDefaultOptions());

            rabbitMqManager = new RabbitMqManager(new RabbitMqModelPooledObjectPolicy(options));
        }

        #endregion Public Constructors

        #region Public Methods

        public void ProcessMessage()
            => rabbitMqManager.ProcessMessage<SaleMessage>(RabbitMqConstants.SalesQueueName, 
                message => PublishBilling(message));
        private bool PublishBilling(MessageBase messageBase)
        {
            if (!(messageBase is SaleMessage message))
                return false;

            rabbitMqManager.PublishBilling(new BillingMessage
            {
                Price = message.Price,
                BillingDate = DateTime.Now,
                Customer = message.Customer,
                Quantity = message.Quantity,
                Product = message.Product
            });

            return true;
        }

        #endregion Public Methods

    }
}
