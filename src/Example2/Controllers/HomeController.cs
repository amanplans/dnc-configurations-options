using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Example2.Models;
using Microsoft.Extensions.Options;

namespace Example2.Controllers
{
    public class HomeController : Controller
    {
        private readonly Models.Example2 _example2;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IOptions<Models.Example2> settings,
            ILogger<HomeController> logger)
        {
            _example2 = settings.Value;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Index()
        {
            var configurations = new Configurations
            {
                Example2 = _example2
            };

            return View(configurations);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
