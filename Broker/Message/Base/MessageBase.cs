using System;

namespace Broker.Message.Base
{
    public class MessageBase
    {
        public MessageBase()
        {
            Id = Guid.NewGuid().ToString();
        }
        #region Public Properties

        public string Id { get; set; }

        #endregion Public Properties
    }
}