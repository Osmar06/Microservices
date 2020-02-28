using Broker.Message;
using RabbitMQ.Client;

namespace Broker.Manager
{
    public interface IRabbitMqManager
    {
        #region Public Methods

        void PublishBilling(BillingMessage message);

        void PublishDelivery(DeliveryMessage message);

        void PublishSale(SaleMessage message);

        IModel GetChannel();

        #endregion Public Methods
    }
}