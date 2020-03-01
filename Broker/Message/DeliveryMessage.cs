using Broker.Message.Base;
using System;

namespace Broker.Message
{
    public class DeliveryMessage : MessageBase
    {
        #region Public Properties

        public string Address { get; set; }
        public string Customer { get; set; }
        public DateTime MaxDeliveryDate { get; set; }
        public string Product { get; set; }

        #endregion Public Properties
    }
}