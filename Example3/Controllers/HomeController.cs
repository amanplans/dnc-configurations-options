using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Example3.Models;
using Microsoft.Extensions.Options;

namespace Example3.Controllers
{
    public class HomeController : Controller
    {
        private readonly Models.Example3 _example3;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IOptions<Models.Example3> settings,
            ILogger<HomeController> logger)
        {
            _example3 = settings.Value;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var configurations = new Configurations
            {
                Example3 = _example3
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
