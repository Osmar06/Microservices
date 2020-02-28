using Broker.Manager;
using Broker.Message;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        #region Private Fields

        private readonly ILogger<HomeController> _logger;
        private readonly IRabbitMqManager manager;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, IRabbitMqManager manager)
        {
            _logger = logger;
            this.manager = manager;
        }

        #endregion Public Constructors

        #region Public Methods

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SendOrder(OrderInputModel orderInputModel)
        {
            manager.PublishSale(new SaleMessage
            {
                SaleDate = DateTime.Now,
                Customer = orderInputModel.Customer,
                Price = orderInputModel.Price,
                Product = orderInputModel.Product,
                Quantity = orderInputModel.Quantity
            });

            return RedirectToAction("Index");
        }

        #endregion Public Methods
    }
}