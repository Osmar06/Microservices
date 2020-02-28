using Broker.Message.Base;
using System;
using System.Collections.Generic;

namespace Broker.Message
{
    public class OrderMessage : MessageBase
    {
        #region Public Properties

        public DateTime OrderDate { get; set; }
        public string Customer { get; set; }
        public List<OrderDetailMessage> OrderDetail { get; set; } = new List<OrderDetailMessage>();

        #endregion Public Properties
    }
}