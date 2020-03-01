using Broker.Manager;
using Broker.Message;
using Broker.Message.Base;
using Broker.Pool;
using Broker.Util;
using Microsoft.Extensions.Options;

namespace Billing
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
            => rabbitMqManager.ProcessMessage<BillingMessage>(RabbitMqConstants.BillingQueueName, 
                message => PublishDelivery(message));

        #endregion Public Methods

        private bool PublishDelivery(MessageBase messageBase)
        {
            if (!(messageBase is BillingMessage message))
                return false;

            rabbitMqManager.PublishDelivery(new DeliveryMessage
            {
                Address = string.Empty,
                Customer = message.Customer,
                MaxDeliveryDate = message.BillingDate.AddDays(5),
                Product = message.Product
            });

            return true;
        }
    }
}