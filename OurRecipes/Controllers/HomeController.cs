using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurRecipes.Data;
using OurRecipes.Models;
using OurRecipes.Services;
using OurRecipes.ViewModels;
using Rotativa.AspNetCore;
using System.Diagnostics;
using System.Transactions;

namespace OurRecipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext modelContext)
        {
            _logger = logger;
            _context = modelContext;
        }





        //_modelContext.Usertables.Add(new Usertable() { Username = "deaa" });
        //var user = _modelContext.Usertables.FirstOrDefault(e => e.Username == "asd");
        //var userTow = _modelContext.Usertables.FirstOrDefault(e => e.Username == "deaa");
        //if ((user is not null ) && (userTow is not null)) { 

        //    user.Password="asd";

        //    _modelContext.Usertables.Remove(userTow);
        //    _modelContext.Usertables.Update(user);

        //}

        //using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
        //{
        //    var userVer = _modelContext.Usertables.FirstOrDefault(e => e.Salary == 700);
        //    Debug.WriteLine("deaa");
        //    if (userVer is not null) { Debug.WriteLine(userVer.Salary); }

        //    var userVer2 = _modelContext.Usertables.FirstOrDefault(e => e.Salary == 700);
        //    Debug.WriteLine("deaa");
        //    if (userVer2 is not null) { Debug.WriteLine(userVer2.Salary); }

        //    // Commit the read transaction before starting another one
        //    scope.Complete();
        //}

        //_modelContext.SaveChanges();




        //using (var transaction = _modelContext.Database.BeginTransaction())
        //{
        //    try
        //    {
        //        // Retrieve the existing user within the transaction
        //        var existingUser =  _modelContext.Usertables.FirstOrDefault(e=>e.Username=="aa");

        //        if (existingUser == null)
        //        {
        //            return NotFound();
        //        }

        //        // Update the existing user with the new values
        //        existingUser.Password = "aa";

        //        // Save changes to the database within the transaction
        //         _modelContext.SaveChanges();

        //        // Commit the transaction
        //        transaction.Commit();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        // Rollback the transaction if a concurrency conflict occurs
        //        transaction.Rollback();
        //        return Conflict();
        //    }
        //}

        public IActionResult About()
        {

            ViewBag.Home = _context.Homes.FirstOrDefault();
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.ContactInfo = _context.ContactInfos.FirstOrDefault();
            ViewBag.RecipesCount = _context.Recipes.Count();
            ViewBag.UsersCount = _context.Users.Count(e => e.RoleId == 2);
            ViewBag.ChiefsCount = _context.Chiefs.Count();




            return View();
        }
        public IActionResult RecipeBlog()
        {

            ViewBag.Home = _context.Homes.FirstOrDefault();
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.ContactInfo = _context.ContactInfos.FirstOrDefault();
            ViewBag.Categories = _context.RecipeCategories.ToList();
            ViewBag.Chiefs = _context.Chiefs.Include(e=>e.User).ToList();
            ViewBag.Testimonials = _context.Testimonials.Where(e => e.TestimonialStatus == "Accepted").Include(e => e.User).ThenInclude(e => e.UserCountry)
                .ToList();
            ViewBag.RecipesCount = _context.Recipes.Count();
            ViewBag.UsersCount = _context.Users.Count(e => e.UserId == 1);
            ViewBag.ChiefsCount = _context.Chiefs.Count();

            ViewBag.RecipeCategoryTypes = _context.RecipeCategoryTypes.ToList();


            var innerJoinQuery = from recipe in _context.Recipes
                                 join category in _context.RecipeCategories
                                 on recipe.RecipeCategoryId equals category.CategoryId
                                 join type in _context.RecipeCategoryTypes
                                 on category.CategoryTypeId equals type.RecipeCategoryTypeId
                                 where recipe.RecipeStatus== "Accepted"
                                 group recipe by new { type.RecipeCategoryTypeId, type.RecipeCategoryTypeName } into g
                                 select new
                                 {
                                     RecipeCategoryTypeId = g.Key.RecipeCategoryTypeId,
                                     RecipeCategoryTypeName = g.Key.RecipeCategoryTypeName,
                                     RecipeCount = g.Count()
                                 };


            ViewBag.TypeWithRecipes= innerJoinQuery.ToList();

            ViewBag.RecipeCategories =  _context.RecipeCategories.ToList();
            ViewBag.IngredientUnits = _context.IngredientUnits.ToList();


            return View();
        }



        public IActionResult RecipeDetails(int recipeId) {

           Recipe recipe= _context.Recipes.Include(e=>e.Chief).ThenInclude(e=>e.User).Include(e => e.RecipePreparationSteps).Include(e => e.RecipeNotes).Include(e=>e.Ingredients).ThenInclude(e=>e.IngredientUnit).First(e => e.RecipeId == recipeId);


            ViewBag.Home = _context.Homes.FirstOrDefault();



            SharedData.Logo = (string)ViewBag.Home.HomeLogo;

            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.ContactInfo = _context.ContactInfos.FirstOrDefault();
            ViewBag.Categories = _context.RecipeCategories.ToList();
            ViewBag.Chiefs = _context.Chiefs.Include(e => e.User).ToList();
            ViewBag.Testimonials = _context.Testimonials.Where(e => e.TestimonialStatus == "Accepted").Include(e => e.User).ThenInclude(e => e.UserCountry)
                .ToList();
            ViewBag.RecipesCount = _context.Recipes.Count();
            ViewBag.UsersCount = _context.Users.Count(e => e.RoleId == 3);
            ViewBag.ChiefsCount = _context.Chiefs.Count();






            return View(recipe);
        }


        public IActionResult Contact()
        {

            ViewBag.Home = _context.Homes.FirstOrDefault();
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.ContactInfo = _context.ContactInfos.FirstOrDefault();
    



            return View();
        }
        [HttpGet]
        public IActionResult Checkout(decimal id) {



            if (HttpContext.Session.GetInt32("userId") == null) {

                return RedirectToAction("Login", "Auth");

            }

            else if (_context.Orders.Any(e => e.UserId == HttpContext.Session.GetInt32("userId") && e.RecipeId == id))
            {
                return RedirectToAction("RecipeDetails", "Home",new { recipeId=id});
                 
            }




           

           var recipe= _context.Recipes.FirstOrDefault(e => e.RecipeId == id);
            ViewBag.Home = _context.Homes.FirstOrDefault();
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.ContactInfo = _context.ContactInfos.FirstOrDefault();

            ViewBag.RecipeForOrdering = recipe;

            return View();        
        }


        //public IActionResult RecipePdfTemplate()
        //{
            

        //    return new ViewAsPdf("RecipePdfTemplate")
        //    {
        //        FileName = "RecipePdfTemplate.pdf"
        //    };
        //}


        public async Task<IActionResult> RecipePdfTemplate(int recipeId)
        {

            Recipe recipe = _context.Recipes.Include(e => e.Chief).ThenInclude(e => e.User).Include(e => e.RecipePreparationSteps).Include(e => e.RecipeNotes).Include(e => e.Ingredients).ThenInclude(e => e.IngredientUnit).First(e => e.RecipeId == recipeId);

           return new ViewAsPdf("RecipePdfTemplate", recipe)
            {
                FileName = "Recipe.pdf"
            };
          
        }


        //public async Task<IActionResult> RecipePdfTemplate()
        //{

        //    var pdfBytes = await new ViewAsPdf("RecipePdfTemplate").BuildFile(ControllerContext);


        //    using (var memoryStream = new MemoryStream(pdfBytes))
        //    {
        //        await EmailService.
        //           SendEmailWithAttachment(memoryStream, "deaa.albettar@gmail.com", "Recipe PDF", "See attached PDF for recipe");


        //        return RedirectToAction("Index");
        //    }
        //}
        public async Task<IActionResult> Index()
        {


          

         






            ViewBag.Home=_context.Homes.FirstOrDefault();



            SharedData.Logo= (string)ViewBag.Home.HomeLogo;
         
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.ContactInfo = _context.ContactInfos.FirstOrDefault();
            ViewBag.Categories=_context.RecipeCategories.ToList();
            ViewBag.Chiefs = _context.Chiefs.Include(e => e.User).ToList();
            ViewBag.Testimonials = _context.Testimonials.Where(e=>e.TestimonialStatus=="Accepted").Include(e=>e.User).ThenInclude(e=>e.UserCountry)
                .ToList();
            ViewBag.RecipesCount= _context.Recipes.Count();
            ViewBag.UsersCount= _context.Users.Count(e=>e.RoleId==2);
            ViewBag.ChiefsCount= _context.Chiefs.Count();




            return View();
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