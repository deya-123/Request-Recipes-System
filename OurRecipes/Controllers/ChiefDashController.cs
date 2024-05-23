using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using OurRecipes.Data;
using OurRecipes.Models;
using OurRecipes.ViewModels;

namespace OurRecipes.Controllers
{
    public class ChiefDashController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IToastNotification _toastNotification;
        public ChiefDashController(AppDbContext context, IMapper mapper, IToastNotification notyf)
        {


            _mapper = mapper;
            _context = context;
            _toastNotification = notyf;
        }
        public IActionResult Index()
        {
            return RedirectToAction("RecipesPage");
        }
        public IActionResult OrdersPage() => View();
        public async Task<IActionResult> RecipesPage()
        {
            ViewBag.RecipeCategoryTypes = await _context.RecipeCategoryTypes.ToListAsync();
            ViewBag.RecipeCategories = await _context.RecipeCategories.ToListAsync();
            ViewBag.IngredientUnits = await _context.IngredientUnits.ToListAsync();
            return View();

        }
        public async Task<IActionResult> Profile()
        {
            ViewBag.Countries = _context.Countries.ToList();
            var user = await _context.Users.FirstOrDefaultAsync(e => e.UserId == HttpContext.Session.GetInt32("userId"));

            if (user != null)
            {
                return View(_mapper.Map<User, UserProfileViewModel>(user));

            }
            return View(new UserProfileViewModel());
        }
    }
}
