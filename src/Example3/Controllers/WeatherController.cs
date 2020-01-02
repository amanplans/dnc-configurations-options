using Microsoft.AspNetCore.Mvc;

namespace Example3.Controllers
{
    public class WeatherController : Controller
    {
        // GET: Weather
        public ActionResult Index()
        {
            return View();
        }
    }
}