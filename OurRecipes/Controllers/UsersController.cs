using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using OurRecipes.Data;
using OurRecipes.Models;
using OurRecipes.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OurRecipes.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IToastNotification _toastNotification;
        public UsersController(AppDbContext context, IToastNotification notyf)
        {
            _context = context;
            _toastNotification = notyf;
        }
       
     
        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(/*await _context.Users.ToListAsync()*/) :
                          Problem("Entity set 'AppDbContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
    
        public async Task<IActionResult> Create([Bind("UserEmail,UserPassword,UserGender,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();  // Create an instance of Random

                // Generate a random integer from 0 to 99
                int randomNumber = random.Next(100);
                user.UserPhone = EmailService.GenerateEmailVerificationToken();
                Console.WriteLine("Random number (0-99): " + randomNumber);
                _context.Add(user);
                await _context.SaveChangesAsync();
                var callbackUrl = Url.Action("ConfirmEmail", "Users", new { userId = user.UserId, token = user.UserPhone }, protocol: HttpContext.Request.Scheme);
                await EmailService.SendEmailAsync(user.UserEmail, "Verification Email",$"<strong>Please confirm your account by clicking <a href='{callbackUrl}'>here</a>.</strong>");

                TempData["SuccessMessage"] = $"User created successfully.{randomNumber}";
               
                _toastNotification.AddSuccessToastMessage("User created successfully", new NotyOptions()
                {
                    ProgressBar = true,
                    Timeout = 200000,
                    Theme = "metroui",
                    Layout = "bottomLeft",
                    
                    


                });
             
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
 
       
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("UserId,UserEmail,UserPassword,UserGender,RoleId,CreatedAt,ModifiedAt,DeletedAt")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(decimal id)
        {
          return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

     
        public async Task<IActionResult> ConfirmEmail(decimal userId, string token)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserPhone != token)
            {
                return RedirectToAction("Error", "Users", new { message ="Error verification" });

            }

            user.UserCountryId = 10;
            user.UserPhone = null; // Clear the token once confirmed
            _context.Update(user);
            await _context.SaveChangesAsync();

            return View("ConfirmEmailSuccess");
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message
            };
            return View(viewModel);
        }

    }
}
