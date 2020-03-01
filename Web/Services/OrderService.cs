using Broker.Manager;
using Broker.Message;
using System;
using Web.Models;

namespace Web.Services
{
    public class OrderService : IOrderService
    {
        #region Private Fields

        private readonly IRabbitMqManager manager;

        #endregion Private Fields

        #region Public Constructors

        public OrderService(IRabbitMqManager manager)
        {
            this.manager = manager;
        }

        #endregion Public Constructors

        #region Public Methods

        public void CreateOrder(OrderInputModel orderInputModel)
        {
            CreateSale(orderInputModel);
            CreateBilling(orderInputModel);
        }

        #endregion Public Methods

        #region Private Methods

        private void CreateBilling(OrderInputModel orderInputModel)
            => manager.PublishBilling(new BillingMessage
            {
                Price = orderInputModel.Price,
                BillingDate = DateTime.Now,
                Customer = orderInputModel.Customer,
                Quantity = orderInputModel.Quantity,
                Product = orderInputModel.Product
            });

        private void CreateSale(OrderInputModel orderInputModel)
                    => manager.PublishSale(new SaleMessage
                    {
                        SaleDate = DateTime.Now,
                        Customer = orderInputModel.Customer,
                        Price = orderInputModel.Price,
                        Product = orderInputModel.Product,
                        Quantity = orderInputModel.Quantity
                    });

        #endregion Private Methods
    }
}