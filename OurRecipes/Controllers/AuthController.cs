using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using OurRecipes.Data;
using OurRecipes.Models;
using OurRecipes.Services;
using OurRecipes.ViewModels;

namespace OurRecipes.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IToastNotification _toastNotification;
        public AuthController(AppDbContext context, IToastNotification notyf)
        {
            _context = context;
            _toastNotification = notyf;
        }
        [HttpGet]
        public IActionResult LoginByGmail()
        {
           return Challenge(new AuthenticationProperties { RedirectUri = "https://localhost:7203/signin-google" }, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {

            if (!ModelState.IsValid) { 
            
            
            }

            var user=_context.Users.FirstOrDefault(e => e.UserEmail == loginViewModel.UserEmail && /*PasswordHasher.VerifyPassword(e.UserPassword, */ e.UserPassword==loginViewModel.UserPassword);
            if (user != null) {

                HttpContext.Session.SetInt32("userId",(int) user.UserId);
                HttpContext.Session.SetString("userName",user.UserName);
                HttpContext.Session.SetInt32("roleId", (int) user.RoleId);
                switch (user.RoleId) {
                    case 1:
                      return  RedirectToAction("Index", "AdminDash");
                    case 2:
                      return RedirectToAction("Index", "UserDash");
                    case 3:
                      return   RedirectToAction("Index", "ChiefDash");

                }
            }


            return View(loginViewModel);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {

                // Check if the username or email already exists
                if (_context.Users.Any(u => u.UserEmail == registerViewModel.UserEmail))
                {
                    ModelState.AddModelError("", "email already exists.");
                    return View();
                }


                var user = new User();
                user.RoleId = registerViewModel.RoleId;
                user.UserName = registerViewModel.UserName;
                user.UserPassword = /*PasswordHasher.HashPassword*/(registerViewModel.UserPassword);
                user.UserEmail = registerViewModel.UserEmail;
                user.EmailVerificationToken = EmailService.GenerateEmailVerificationToken();
                user.EmailVerificationTokenExpireDate = DateTime.UtcNow.AddDays(2);
                _context.Add(user);
                var effect =await _context.SaveChangesAsync();

                if (effect > 0) {
                    if (registerViewModel.RoleId == 3)
                    {
                        var chief = new Chief();
                        _context.Add(chief);
                        await _context.SaveChangesAsync();

                    }

                    var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { userId = user.UserId, token = user.EmailVerificationToken }, protocol: HttpContext.Request.Scheme);
                    await EmailService.SendEmailAsync(user.UserEmail, "Verification Email", $"<strong>Please confirm your account by clicking <a href='{callbackUrl}'>here</a>.</strong>");

                    TempData["SuccessMessage"] = $"You Registerd Successfully, Now Verificate Your Email, You have 2 days for verification  your email ";

                    _toastNotification.AddSuccessToastMessage("You Registerd Successfully, Now Verificate Your Email, You have 2 days for verification  your email ", new NotyOptions()
                    {
                        ProgressBar = true,
                        Timeout = 2000,
                        Theme = "metroui",
                        Layout = "bottomCenter",

                    });

                    return RedirectToAction(nameof(Login));

                }
               
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(decimal userId, string token)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user != null && user.IsEmailVerification==true) {
                return View("Error", new ErrorViewModel { Message = "Error verification, email is already verificated" });
            }
            else if (user == null || user.EmailVerificationToken != token )
            {
                return View("Error", new ErrorViewModel { Message = "Error verification, the token is not correct" });

            }
            else if (user.EmailVerificationTokenExpireDate < DateTime.UtcNow)
            {
                return View("ResendVerificationEmail", new ResendVerificationEmailViewModel
                {
                    Email = user.UserEmail, // Assuming you have this property
                    EmailToken = user.EmailVerificationToken // Add this property in your ViewModel if not present
                });

            }
            user.IsEmailVerification = true;
            user.EmailVerificationToken = null;
            user.EmailVerificationTokenExpireDate = null;
            // Clear the token once confirmed
            _context.Update(user);
            await _context.SaveChangesAsync();

            return View("ConfirmEmailSuccess");
        }
        [HttpPost]
        public async Task<IActionResult> ResendVerificationEmail(ResendVerificationEmailViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == model.Email && u.EmailVerificationToken == model.EmailToken);
            if (user == null)
            {
                // Optionally handle not found case
                return View("Error", new ErrorViewModel { Message = "Error verification, Email is not correct" });
            }

            user.EmailVerificationToken = EmailService.GenerateEmailVerificationToken();
            user.EmailVerificationTokenExpireDate = DateTime.UtcNow.AddDays(2);
            _context.Update(user);
            await _context.SaveChangesAsync();
            
            var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { userId = user.UserId, token = user.EmailVerificationToken }, protocol: HttpContext.Request.Scheme);
            await EmailService.SendEmailAsync(user.UserEmail, "Verification Email", $"<strong>Please confirm your account by clicking <a href='{callbackUrl}'>here</a>.</strong>");

            return View("VerificationEmailResent");
        }

        [HttpGet]
        public IActionResult PasswordResetRequest()
        {
            return View();
        }
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PasswordResetRequest(PasswordResetRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == model.Email);
                if (user != null)
                {
                    var token = Guid.NewGuid().ToString(); // Generate a reset token
                    user.PasswordVerificationToken = token;
                    user.PasswordVerificationTokenExpireDate = DateTime.UtcNow.AddHours(1); // Token expiry time
                    await _context.SaveChangesAsync();

                    var resetLink = Url.Action("ResetPassword", "Auth", new { userId = user.UserId, token = token }, Request.Scheme);
                    // Send the email
                    await EmailService.SendEmailAsync(user.UserEmail, "Reset Your Password", $"Please reset your password by clicking here: <a href='{resetLink}'>link</a>");
                }

                // Inform the user to check their email
                return View("ResetPasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(decimal userId,string token)
        {

            var user = await _context.Users.FindAsync(userId);

            if (user != null && user.PasswordVerificationToken == null)
            {
                return View("Error", new ErrorViewModel { Message = "Error verification, password is already reseted" });
            }
           else if (user == null || user.PasswordVerificationToken != token )
            {
                return View("Error", new ErrorViewModel { Message = "Error verification, the token is not correct" });

            }
            else if (user.PasswordVerificationTokenExpireDate < DateTime.UtcNow)
            {
                return View("Error", new ErrorViewModel { Message = "Error verification, the token is expire" });

            }
          
            return View(new ResetPasswordViewModel { Token = token });
        }


        [HttpPost]
      
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordVerificationToken == model.Token && u.PasswordVerificationTokenExpireDate < DateTime.UtcNow);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid or expired reset token");
                    return View(model);
                }

                user.UserPassword = PasswordHasher.HashPassword(model.Password); // Implement your hashing here
                user.PasswordVerificationToken = null;
                user.PasswordVerificationTokenExpireDate = null;
                await _context.SaveChangesAsync();

                return View("ResetPasswordConfirmation");
            }

            return View(model);
        }

        //public IActionResult Error(string message)
        //{
        //    var viewModel = new ErrorViewModel
        //    {
        //        Message = message
        //    };
        //    return View(viewModel);
        //}

        //public async Task<IActionResult> Create([Bind("UserEmail,UserPassword,UserGender,RoleId")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Random random = new Random();  // Create an instance of Random

        //        // Generate a random integer from 0 to 99
        //        int randomNumber = random.Next(100);
        //        user.UserPhone = EmailService.GenerateEmailVerificationToken();
        //        Console.WriteLine("Random number (0-99): " + randomNumber);
        //        _context.Add(user);
        //        await _context.SaveChangesAsync();
        //        var callbackUrl = Url.Action("ConfirmEmail", "Users", new { userId = user.UserId, token = user.UserPhone }, protocol: HttpContext.Request.Scheme);
        //        await EmailService.SendEmailAsync(user.UserEmail, "Verification Email", $"<strong>Please confirm your account by clicking <a href='{callbackUrl}'>here</a>.</strong>");

        //        TempData["SuccessMessage"] = $"User created successfully.{randomNumber}";

        //        _toastNotification.AddSuccessToastMessage("User created successfully", new NotyOptions()
        //        {
        //            ProgressBar = true,
        //            Timeout = 200000,
        //            Theme = "metroui",
        //            Layout = "bottomLeft",




        //        });

        //        return RedirectToAction(nameof(Index));
        //    }
        //    return RedirectToAction(nameof(Index));
        //}


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
