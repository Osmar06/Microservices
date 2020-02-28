using Broker.Message.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Broker.Message
{
    public class DeliveryMessage : MessageBase
    {
        public string Customer { get; set; }
        public string MaxDeliveryDate { get; set; }
        public string Address { get; set; }
        public string Product { get; set; }
    }
}
