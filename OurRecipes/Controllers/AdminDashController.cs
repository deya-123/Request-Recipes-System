using Microsoft.AspNetCore.Mvc;

namespace OurRecipes.Controllers
{
    public class AdminDashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
