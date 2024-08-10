using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using OurRecipes.Data;
using OurRecipes.Models;
using OurRecipes.ViewModels;
using System.Linq;

namespace OurRecipes.Controllers
{
    public class AdminDashController : Controller
    {
        private readonly IMapper _mapper;


        private readonly AppDbContext _context;
        private readonly IToastNotification _toastNotification;
        public AdminDashController(AppDbContext context, IMapper mapper, IToastNotification notyf)
        {


            _mapper = mapper;
            _context = context;
            _toastNotification = notyf;
        }
        public IActionResult Index()
        {
            var home = _context.Homes.FirstOrDefault();
            SharedData.Logo = home?.HomeLogo ?? "";

            return RedirectToAction("StatisticsPage");
        }


        public async Task<IActionResult> UsersPage()
        {


            ViewBag.Roles = _context.Roles.Where(e => e.RoleName != null && e.RoleName.ToLower() != "admin").ToList();
            ViewBag.Countries = _context.Countries.ToList();
            return View(/*await _context.Users.ToListAsync()*/);
        }



        public IActionResult ContactInfoPage() => View();
        public IActionResult HomeInfoPage() => View();
        public IActionResult AboutPage() => View();
        public IActionResult TestimonialsPage() => View();
        public IActionResult OrdersPage() => View();
        public async Task<IActionResult> StatisticsPage()
        {

            ViewBag.ChiefsCount = await _context.Chiefs.CountAsync();
            ViewBag.UsersCount = await _context.Users.Where(e => e.RoleId == 2).CountAsync();
            ViewBag.RecipesCount = await _context.Recipes.CountAsync();
            ViewBag.OrdersCount = await _context.Orders.CountAsync();



            var today = DateTime.Today.AddDays(1);
            var tenDaysAgo = today.AddDays(-9);


            var ordersPerDay = _context.Orders
      .Where(o => o.CreatedAt >= tenDaysAgo && o.CreatedAt <= today && o.CreatedAt != null)
      .GroupBy(o => o.CreatedAt!.Value.Date)
      .OrderBy(g => g.Key)
      .Select(g => new { Day = g.Key, OrderCount = g.Count() })
      .ToList();


            var labels = ordersPerDay.Select(d => d.Day.ToString("yyyy-MM-dd")).ToArray();
            var orderCounts = ordersPerDay.Select(d => d.OrderCount).ToArray();


            ViewBag.ChartLabels = labels;
            ViewBag.OrderCounts = orderCounts;
            ViewBag.OrderRevenue = await _context.Orders.Where(o => o.CreatedAt >= tenDaysAgo && o.CreatedAt <= today && o.CreatedAt != null).SumAsync(e=>e.OrderPrice);



            return View();

        }

        public IActionResult ContactsPage() => View();
        public async Task<IActionResult> CategoriesPage()
        {
            ViewBag.RecipeCategoryTypes = await _context.RecipeCategoryTypes.ToListAsync();
            return View();

        }
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
