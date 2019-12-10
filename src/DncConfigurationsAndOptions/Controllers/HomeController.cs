using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DncConfigurationsAndOptions.Models;
using DncConfigurationsAndOptions.ViewModels;
using Microsoft.Extensions.Configuration;

namespace DncConfigurationsAndOptions.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IConfiguration configuration,
            ILogger<HomeController> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Index()
        {
            var configurations = new Configurations
            {
                Example1 = new Example1
                {
                    SiteConfiguration = new SiteConfiguration
                    {
                        BaseUrl = _configuration["Example1:SiteConfiguration:BaseUrl"]
                    }
                }
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
