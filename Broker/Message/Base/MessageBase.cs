using System;

namespace Broker.Message.Base
{
    public class MessageBase
    {
        #region Public Properties

        public string Id { get; set; } = new Guid().ToString();

        #endregion Public Properties
    }
}