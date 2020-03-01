using Broker.Manager;
using Broker.Message;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        #region Private Fields

        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService orderService;

        #endregion Private Fields

        #region Public Constructors

        public HomeController(ILogger<HomeController> logger, IOrderService orderService)
        {
            _logger = logger;
            this.orderService = orderService;
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
            orderService.CreateOrder(orderInputModel);

            return RedirectToAction("Index");
        }

        #endregion Public Methods
    }
}