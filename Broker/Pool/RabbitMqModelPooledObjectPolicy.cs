using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Broker.Pool
{
    public class RabbitMqModelPooledObjectPolicy : IPooledObjectPolicy<IModel>
    {
        #region Private Fields

        private readonly IConnection connection;
        private readonly RabbitMqOptions options;

        #endregion Private Fields

        #region Public Constructors

        public RabbitMqModelPooledObjectPolicy(IOptions<RabbitMqOptions> options)
        {
            this.options = options.Value;
            connection = GetConnection();
        }

        #endregion Public Constructors

        #region Public Methods

        public IModel Create() => connection.CreateModel();

        public bool Return(IModel obj)
        {
            if (obj.IsOpen)
            {
                return true;
            }
            else
            {
                obj?.Dispose();
                return false;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private IConnection GetConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = options.HostName,
                UserName = options.UserName,
                Password = options.Password,
                Port = options.Port,
                VirtualHost = options.VHost,
            };

            return factory.CreateConnection();
        }

        #endregion Private Methods
    }
}