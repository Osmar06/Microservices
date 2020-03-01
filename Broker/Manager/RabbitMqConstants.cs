namespace Broker.Manager
{
    public class RabbitMqConstants
    {
        #region Public Fields

        public const string BillingQueueName = "BillingTopic_Queue";
        public const string BillingRouteKey = "order.BillingOrder";
        public const string DeliveryQueueName = "DeliveryTopic_Queue";
        public const string DeliveryRouteKey = "order.DeliveryOrder";
        public const string ExchangeName = "OrderTopic_Exchange";
        public const string SalesQueueName = "SalesTopic_Queue";
        public const string SalesRouteKey = "order.SaleOrder";

        #endregion Public Fields
    }
}