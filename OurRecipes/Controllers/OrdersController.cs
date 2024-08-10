using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using OurRecipes.Data;
using OurRecipes.Models;
using OurRecipes.Services;
using OurRecipes.ViewModels;
using Rotativa.AspNetCore;

namespace OurRecipes.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IToastNotification _toastNotification;
     


        public OrdersController(AppDbContext context, IToastNotification notyf)
        {
            _context = context;
            _toastNotification = notyf;
       

        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
              return _context.Orders != null ? 
                          View(await _context.Orders.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Orders'  is null.");
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderStatus,UserId,RecipeId,OrderPrice,CreatedAt,ModifiedAt,DeletedAt")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("OrderId,OrderStatus,UserId,RecipeId,OrderPrice,CreatedAt,ModifiedAt,DeletedAt")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'AppDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(decimal id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }


        public async Task RecipePdfTemplate(int recipeId)
        {

            Recipe recipe = _context.Recipes.Include(e => e.Chief).ThenInclude(e => e.User).Include(e => e.RecipePreparationSteps).Include(e => e.RecipeNotes).Include(e => e.Ingredients).ThenInclude(e => e.IngredientUnit).First(e => e.RecipeId == recipeId);

            var pdf = new ViewAsPdf("RecipePdfTemplate", recipe)
            {
                FileName = "Recipe.pdf"
            };
            var pdfBytes = await pdf.BuildFile(ControllerContext);


            var userEmail = "albettardeaa@gmail.com";


            if (HttpContext.Session.GetInt32("userId") != null) {

                userEmail = _context.Users.First(e => e.UserId == HttpContext.Session.GetInt32("userId")).UserEmail;


            }


            using (var memoryStream = new MemoryStream(pdfBytes))
            {
                await EmailService.
                   SendEmailWithAttachment(memoryStream, userEmail, "Recipe PDF", "See attached PDF for recipe","Recipe");
            }

        }

        public async Task InvoicePdfTemplate(int orderId)
        {



            Order order  = _context.Orders.Where(e=>e.OrderId== orderId).Include(e=>e.Recipe).Include(e => e.User).First();

           var pdf= new ViewAsPdf("InvoicePdfTemplate", order)
            {
                
                FileName = "Invoice.pdf",
                
            };
            var pdfBytes = await pdf.BuildFile(ControllerContext);

            var userEmail = "albettardeaa@gmail.com";


            if (HttpContext.Session.GetInt32("userId") != null)
            {

                userEmail = _context.Users.First(e => e.UserId == HttpContext.Session.GetInt32("userId")).UserEmail;


            }



            using (var memoryStream = new MemoryStream(pdfBytes))
            {
                await EmailService.
                   SendEmailWithAttachment(memoryStream, userEmail, "Invoice PDF", "See attached PDF for Invoice", "Invoice");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(PlaceOrderViewModel placeOrderViewModel)
        {
            if (ModelState.IsValid)
            {
                var recipe = _context.Recipes.FirstOrDefault(e => e.RecipeId==placeOrderViewModel.RecipeId);
                var userId = HttpContext.Session.GetInt32("userId");
                if (recipe is not null && userId is not null) {

                    var card = _context.Cards.FirstOrDefault(e => e.CardHolderName == placeOrderViewModel.CardHolderName &&
                     e.CardExpireDate == placeOrderViewModel.CardExpireDate && e.CardCvv == placeOrderViewModel.CardCvv);
                    if (card is not null)
                    {
                        if (card.CardValue < recipe.RecipePrice) {
                            _toastNotification.AddErrorToastMessage("ther is a problem ", new NotyOptions()
                            {
                                ProgressBar = true,
                                Timeout = 2000,
                                Theme = "metroui",
                                Layout = "bottomCenter",

                            });
                            return RedirectToAction("Checkout", "Home", new { id = placeOrderViewModel.RecipeId });
                        } 
                        card.CardValue -= recipe.RecipePrice;
                        var order = new Order();

                        order.OrderPrice = recipe.RecipePrice;
                        order.UserId = userId;
                        order.OrderStatus = "The payment was made";
                        order.RecipeId = recipe.RecipeId;
                        _context.Add(order);
                        await _context.SaveChangesAsync();

                       await RecipePdfTemplate((int)placeOrderViewModel.RecipeId);
                       await InvoicePdfTemplate((int)order.OrderId);
                  
                        _toastNotification.AddSuccessToastMessage("The operation was completed successfully", new NotyOptions()
                        {
                            ProgressBar = true,
                            Timeout = 2000,
                            Theme = "metroui",
                            Layout = "bottomCenter",

                        });


                        return RedirectToAction("Checkout", "Home", new { id = placeOrderViewModel.RecipeId });
                    }

                }
               



              
            }
            _toastNotification.AddErrorToastMessage("ther is a problem ", new NotyOptions()
            {
                ProgressBar = true,
                Timeout = 2000,
                Theme = "metroui",
                Layout = "bottomCenter",

            });
            return RedirectToAction("Checkout", "Home",new { id= placeOrderViewModel.RecipeId});
        }


        //orderId
        //orderStatus
        //orderPrice
        //recipeName
        //userName
        //chiefName


        public ActionResult LoadData()
        {


            return Json(new
            {
                data = _context.Orders.Include(e => e.User).Include(e=>e.Recipe).ThenInclude(e=>e.Chief).Select(e => new
                {
                    e.OrderId,
                    e.OrderStatus,
                    e.OrderPrice,
                    e.UserId,
                    e.Recipe.RecipeName,
                    UserName=e.User.UserName,
                    ChiefName=e.Recipe.Chief.User.UserName,
                    e.RecipeId,
                    CreatedAt = e.CreatedAt != null ? e.CreatedAt.Value.Date.ToShortDateString() : null,

                })

                .ToList()
            });
        }




        [HttpGet]
        public ActionResult LoadDataByMonth(string month)
        {
            if (string.IsNullOrEmpty(month) || !DateTime.TryParse(month + "-01", out DateTime startDate))
            {
                return BadRequest("Invalid month format");
            }

            var endDate = startDate.AddMonths(1);

            var orders = _context.Orders
                .Include(e => e.User)
                .Include(e => e.Recipe)
                    .ThenInclude(e => e.Chief)
                .Where(e => e.CreatedAt >= startDate && e.CreatedAt < endDate)
                .Select(e => new
                {
                    e.OrderId,
                    e.OrderStatus,
                    e.OrderPrice,
                    e.UserId,
                    RecipeName = e.Recipe.RecipeName,
                    UserName = e.User.UserName,
                    ChiefName = e.Recipe.Chief.User.UserName,
                    e.RecipeId,
                    CreatedAt = e.CreatedAt != null ? e.CreatedAt.Value.Date.ToShortDateString() : null,
                })
                .ToList();

            return Json(new { data = orders });
        }

        [HttpGet]
        public ActionResult LoadDataByYear(int? year)
        {
            if (!year.HasValue)
            {
                return BadRequest("Year parameter is required");
            }

            var startDate = new DateTime(year.Value, 1, 1);
            var endDate = startDate.AddYears(1);

            var orders = _context.Orders
                .Include(e => e.User)
                .Include(e => e.Recipe)
                    .ThenInclude(e => e.Chief)
                .Where(e => e.CreatedAt >= startDate && e.CreatedAt < endDate)
                .Select(e => new
                {
                    e.OrderId,
                    e.OrderStatus,
                    e.OrderPrice,
                    e.UserId,
                    RecipeName = e.Recipe.RecipeName,
                    UserName = e.User.UserName,
                    ChiefName = e.Recipe.Chief.User.UserName,
                    e.RecipeId,
                    CreatedAt = e.CreatedAt != null ? e.CreatedAt.Value.Date.ToShortDateString() : null,
                })
                .ToList();

            return Json(new { data = orders });
        }



        public ActionResult LoadDataByChiefId(decimal id)
        {


            return Json(new
            {
                data = _context.Orders.Where(e => e.Recipe.ChiefId == id).Include(e => e.User).Include(e => e.Recipe).ThenInclude(e => e.Chief).Select(e => new
                {
                    e.OrderId,
                    e.OrderStatus,
                    e.OrderPrice,
                    e.UserId,
                    e.Recipe.RecipeName,
                    UserName = e.User.UserName,
                    ChiefName = e.Recipe.Chief.User.UserName,
                    e.RecipeId,
                    CreatedAt = e.CreatedAt != null ? e.CreatedAt.Value.Date.ToShortDateString() : null,

                })

                .ToList()
            });
        }
        public ActionResult LoadDataByUserId(decimal id)
        {


            return Json(new
            {
                data = _context.Orders.Where(e=>e.UserId==id).Include(e => e.User).Include(e => e.Recipe).ThenInclude(e => e.Chief).Select(e => new
                {
                    e.OrderId,
                    e.OrderStatus,
                    e.OrderPrice,
                    e.UserId,
                    e.Recipe.RecipeName,
                    UserName = e.User.UserName,
                    ChiefName = e.Recipe.Chief.User.UserName,
                    e.RecipeId,
                    CreatedAt = e.CreatedAt != null ? e.CreatedAt.Value.Date.ToShortDateString() : null,

                })

                .ToList()
            });
        }
        //public ActionResult LoadDataByChiefId(decimal id)
        //{


        //    return Json(new
        //    {
        //        data = _context.Recipes.Include(e => e.RecipeCategory).Where(e => e.ChiefId == id).Select(e => new
        //        {
        //            e.RecipeId,
        //            e.RecipeName,
        //            e.RecipePrice,
        //            CategoryName = e.RecipeCategory != null ? e.RecipeCategory.CategoryName : null, // Check if UserCountry is null
        //            e.RecipeCookingTimeMinutes,
        //            e.RecipePreparingTimeMinutes,
        //            e.RecipeServings,
        //            e.RecipeDescription,
        //            e.RecipeExplanation,
        //            e.RecipeStatus,
        //            CreatedAt = e.CreatedAt != null ? e.CreatedAt.Value.Date.ToShortDateString() : null,

        //        })

        //        .ToList()
        //    }); ;
        //}



    }
}
