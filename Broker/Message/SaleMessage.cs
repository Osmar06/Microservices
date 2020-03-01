using Broker.Message.Base;
using System;

namespace Broker.Message
{
    public class SaleMessage : MessageBase
    {
        #region Public Properties

        public string Customer { get; set; }
        public decimal Price { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }

        #endregion Public Properties
    }
}