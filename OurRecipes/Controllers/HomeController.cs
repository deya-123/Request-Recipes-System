using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OurRecipes.Data;
using OurRecipes.Models;
using System.Diagnostics;
using System.Transactions;

namespace OurRecipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _modelContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext modelContext)
        {
            _logger = logger;
            _modelContext = modelContext;
        }
       
        public IActionResult Index()
        {

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