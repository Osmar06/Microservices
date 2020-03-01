using Broker.Message.Base;
using System;

namespace Broker.Message
{
    public class BillingMessage : MessageBase
    {
        #region Public Properties

        public decimal Ammount => Price * Quantity;
        public DateTime BillingDate { get; set; }
        public string Customer { get; set; }
        public decimal IVA => Ammount * 0.15M;
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total => Ammount + IVA;

        public string Product { get; set; }

        #endregion Public Properties
    }
}