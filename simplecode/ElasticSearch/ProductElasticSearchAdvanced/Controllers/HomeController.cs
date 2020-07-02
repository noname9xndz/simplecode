using Microsoft.AspNetCore.Mvc;

namespace ProductElasticSearchAdvanced.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}