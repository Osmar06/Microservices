using Broker.Message.Base;
using System;

namespace Broker.Message
{
    public class BillingMessage : MessageBase
    {
        #region Public Properties

        public decimal Amount { get; set; }
        public DateTime BillingDate { get; set; }
        public string Customer { get; set; }
        public decimal IVA { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }

        #endregion Public Properties
    }
}