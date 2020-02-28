using Broker.Manager;
using Broker.Pool;
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
            var rabbitConfig = configuration.GetSection("RabbitMQ");
            services.Configure<RabbitMqOptions>(rabbitConfig);

            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitMqModelPooledObjectPolicy>();

            services.AddSingleton<IRabbitMqManager, RabbitMqManager>();

            return services;
        }

        #endregion Public Methods
    }
}