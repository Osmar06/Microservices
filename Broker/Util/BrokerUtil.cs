using Broker.Pool;

namespace Broker.Util
{
    public class BrokerUtil
    {
        #region Public Methods

        public static RabbitMqOptions GetRabbitMqDefaultOptions()
            => new RabbitMqOptions
            {
                HostName = "localhost",
                Password = "guest",
                Port = 5672,
                UserName = "guest",
                VHost = "/",
            };

        #endregion Public Methods
    }
}