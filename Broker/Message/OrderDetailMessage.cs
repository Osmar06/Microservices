using Broker.Message.Base;

namespace Broker.Message
{
    public class OrderDetailMessage : MessageBase
    {
        #region Public Properties

        public string OrderId { get; set; }
        public decimal Price { get; set; }
        public string Product { get; set; }

        #endregion Public Properties
    }
}