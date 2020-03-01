using Broker.Manager;
using Broker.Pool;
using Broker.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace Web.Extension
{
    public static class RabbitMqServiceCollectionExtensions
    {
        #region Public Methods

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            //ToDo: the options must be in appsettings.json
            var defaultOptions = BrokerUtil.GetRabbitMqDefaultOptions();
            services.Configure<RabbitMqOptions>(opt =>
            {
                opt.HostName = defaultOptions.HostName;
                opt.Password = defaultOptions.Password;
                opt.Port = defaultOptions.Port;
                opt.UserName = defaultOptions.UserName;
                opt.VHost = defaultOptions.VHost;
            });

            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitMqModelPooledObjectPolicy>();

            services.AddSingleton<IRabbitMqManager, RabbitMqManager>();

            return services;
        }

        #endregion Public Methods
    }
}