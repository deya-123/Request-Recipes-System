using Microsoft.AspNetCore.Mvc;

namespace OurRecipes.Controllers
{
    public class UserDashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
