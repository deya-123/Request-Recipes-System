using Microsoft.AspNetCore.Mvc;

namespace OurRecipes.Controllers
{
    public class ChiefDashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
