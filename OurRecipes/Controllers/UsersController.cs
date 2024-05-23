using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using OurRecipes.Data;
using OurRecipes.Models;
using OurRecipes.Services;
using OurRecipes.Utilities;
using OurRecipes.ViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace OurRecipes.Controllers
{
 
    public class UsersController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        public UsersController(AppDbContext context, IMapper mapper,IToastNotification notyf, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _toastNotification = notyf;
            _environment = environment;
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



        public async Task<IActionResult> GetUserById(decimal id) {
        
        return Json(await _context.Users.FirstOrDefaultAsync(e=>e.UserId==id));
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
        public async Task<IActionResult> Create([Bind("UserName,UserEmail,UserPassword,UserGender,UserPhone,UserCountryId,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {

                var url = HttpContext.Request.Path;
                //var user = new User();
                //user.UserEmail = userViewModel.UserEmail;
                //user.UserGender = userViewModel.UserGender;
                //user.UserPhone = userViewModel.UserPhone;
                //user.UserCountryId=userViewModel=user
                _context.Add(user);
                await _context.SaveChangesAsync();
                //var callbackUrl = Url.Action("ConfirmEmail", "Users", new { userId = user.UserId, token = user.UserPhone }, protocol: HttpContext.Request.Scheme);
                //await EmailService.SendEmailAsync(user.UserEmail, "Verification Email",$"<strong>Please confirm your account by clicking <a href='{callbackUrl}'>here</a>.</strong>");

                TempData["SuccessMessage"] = $"User created successfully.";
               
                _toastNotification.AddSuccessToastMessage("User created successfully"+ url, new NotyOptions()
                {
                    ProgressBar = true,
                    Timeout = 2000,
                    Theme = "metroui",
                    Layout = "bottomCenter",
                    
                    


                });

                //return View("/Views/AdminDash/UsersPage.cshtml", new UserViewModel());
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("UsersPage", "AdminDash");

            }

            return RedirectToAction("UsersPage", "AdminDash");

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
        public async Task<IActionResult> Edit(decimal id, UserViewModel userViewModel)
        {
            if (id != userViewModel.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var user = _context.Users.FirstOrDefault(e => e.UserId == userViewModel.UserId);
                    if (user != null) {
                   
                        _mapper.Map<UserViewModel,User>(userViewModel, user);
                        _context.Update(user);
                        await _context.SaveChangesAsync();


                    }

                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userViewModel.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("UsersPage", "AdminDash"); ;
            }
            return RedirectToAction("UsersPage", "AdminDash");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserProfile(decimal id, UserProfileViewModel userViewModel)
        {
            if (id != userViewModel.UserId)
            {
                return NotFound();
            }
            var user = _context.Users.FirstOrDefault(e => e.UserId == userViewModel.UserId);
            if (ModelState.IsValid)
            {
                try
                {

                   
                    if (user != null)
                    {


                        if (userViewModel.UserProfileImage != null)
                        {
                            
                        var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
                        if (user != null && user.UserImage != null)
                        {

                            System.IO.File.Delete(Path.Combine(viewFolderPath, "usersImage") + "\\" + user.UserImage);
                        }

                            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(userViewModel.UserProfileImage.FileName);
                            var imagePath = Path.Combine(viewFolderPath, "usersImage") + "\\" + imageName;
                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                await userViewModel.UserProfileImage.CopyToAsync(stream);
                                user.UserImage = imageName;


                            }
                            HttpContext.Session.SetString("userImage", user.UserImage);
                        }

                        _mapper.Map<UserProfileViewModel, User>(userViewModel, user);
                        if (userViewModel.UserPassword is not null) {

                            user.UserPassword = PasswordHasher.HashPassword(userViewModel.UserPassword);
                        }
                        _context.Update(user);
                        await _context.SaveChangesAsync();

                        HttpContext.Session.SetString("userName", user.UserName);
                    }


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userViewModel.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                return RedirectHelper.RedirectByRoleId("Profile",HttpContext.Session.GetInt32("roleId")??1 ); ;
            }
            return RedirectHelper.RedirectByRoleId("Profile", HttpContext.Session.GetInt32("roleId") ?? 1);
        }




        // GET: Users/Delete/5
        //public async Task<IActionResult> Delete(decimal? id)
        //{
        //    if (_context.Users == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.Users'  is null.");
        //    }
        //    var user = await _context.Users.FindAsync(id);
        //    if (user != null)
        //    {
        //        _context.Users.Remove(user);
        //    }

        //    await _context.SaveChangesAsync();
        //    // return RedirectToAction(nameof(Index));
        //    return RedirectToAction("UsersPage", "AdminDash");
        //}

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(decimal id)
        {
            if (_context.Users == null)
            {
               
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();

            return RedirectToAction("UsersPage", "AdminDash"); ;

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
        public ActionResult LoadData()
        {
       

            return Json(new { data = _context.Users.Include(e => e.Role).Include(e => e.UserCountry).Select(e => new
            {
                e.UserId,
                e.UserName,
                e.UserPhone,
                e.UserGender,
                CountryName = e.UserCountry != null ? e.UserCountry.CountryName : null, // Check if UserCountry is null
                e.Role.RoleName,
                e.UserEmail
            })

                .ToList() }); ;
        }





        //public class PlanImage
        //{
        //    public IFormFile CoverImage { get; set; }
        //    public int PlanId { get; set; }
        //}
        //[Authorize(Roles = "Admin")]
        //[Consumes("multipart/form-data")]
        //[HttpPost]
        //public IActionResult UploadUserImages([FromForm] PlanImage planImage)
        //{
        //    try
        //    {


        //        var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
        //        var user = _context.Users.Where(e => e.UserId == planImage.PlanId).FirstOrDefault();
        //        if (user != null && user.ImagePath != null)
        //        {

        //            System.IO.File.Delete(Path.Combine(viewFolderPath, "Images") + "\\" + user.ImagePath);
        //        }


        //        var imageName = Guid.NewGuid().ToString() + Path.GetExtension(planImage.CoverImage.FileName);





        //        var imagePath = Path.Combine(viewFolderPath, "Images") + "\\" + imageName;



        //        using (var stream = new FileStream(imagePath, FileMode.Create))
        //        {
        //            planImage.CoverImage.CopyTo(stream);
        //            user.ImagePath = imageName;

        //        }


        //        _context.Update(user);
        //        _context.SaveChanges();

        //        return Ok(new { Message = "Images uploaded successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
        //    }
        //}

        //[Consumes("multipart/form-data")]
        //[HttpPost]
        //public IActionResult UploadImages([FromForm] UploadImage uploadImage)
        //{
        //    try
        //    {
        //        // Process the images (store, validate, etc.)
        //        // Replace this with your actual logic

        //        var imageUrls = new List<string>();
        //        foreach (var image in uploadImage.Images)
        //        {
        //            // Save the image to a location or database
        //            // Replace this with your actual logic
        //            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        //            //var imageUrl = "C:\\Users\\user\\source\\repos\\deaaAldeen45112\\doctor_quiz\\Api_Backend\\doctorQuizApp\\Images\\" + imageName;
        //            var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot");

        //            // Specify the folder where you want to store images
        //            var imagePath = Path.Combine(viewFolderPath, "Images") + "\\" + imageName;


        //            imageUrls.Add(imagePath);

        //            using (var stream = new FileStream(imagePath, FileMode.Create))
        //            {
        //                image.CopyTo(stream);

        //                QuestionImage questionImage = new QuestionImage();
        //                questionImage.QuestionId = uploadImage.QuestionId;
        //                questionImage.ImageType = uploadImage.ImageType;
        //                questionImage.ImagePath = imageName;
        //                _questionImageService.Insert(questionImage);
        //            }
        //        }

        //        return Ok(new { Message = "Images uploaded successfully", ImageUrls = imageUrls });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
        //    }
        //}



        //[HttpGet]
        //public void LoadDataCountries() {


        //    List<Country> countries = new List<Country>
        //{
        //    new Country { CountryName = "Afghanistan" },
        //    new Country { CountryName = "Albania" },
        //    new Country { CountryName = "Algeria" },
        //    new Country { CountryName = "Andorra" },
        //    new Country { CountryName = "Angola" },
        //    new Country { CountryName = "Antigua and Barbuda" },
        //    new Country { CountryName = "Argentina" },
        //    new Country { CountryName = "Armenia" },
        //    new Country { CountryName = "Australia" },
        //    new Country { CountryName = "Austria" },
        //    new Country { CountryName = "Azerbaijan" },
        //    new Country { CountryName = "Bahamas" },
        //    new Country { CountryName = "Bahrain" },
        //    new Country { CountryName = "Bangladesh" },
        //    new Country { CountryName = "Barbados" },
        //    new Country { CountryName = "Belarus" },
        //    new Country { CountryName = "Belgium" },
        //    new Country { CountryName = "Belize" },
        //    new Country { CountryName = "Benin" },
        //    new Country { CountryName = "Bhutan" },
        //    new Country { CountryName = "Bolivia" },
        //    new Country { CountryName = "Bosnia and Herzegovina" },
        //    new Country { CountryName = "Botswana" },
        //    new Country { CountryName = "Brazil" },
        //    new Country { CountryName = "Brunei" },
        //    new Country { CountryName = "Bulgaria" },
        //    new Country { CountryName = "Burkina Faso" },
        //    new Country { CountryName = "Burundi" },
        //    new Country { CountryName = "Cabo Verde" },
        //    new Country { CountryName = "Cambodia" },
        //    new Country { CountryName = "Cameroon" },
        //    new Country { CountryName = "Canada" },
        //    new Country { CountryName = "Central African Republic" },
        //    new Country { CountryName = "Chad" },
        //    new Country { CountryName = "Chile" },
        //    new Country { CountryName = "China" },
        //    new Country { CountryName = "Colombia" },
        //    new Country { CountryName = "Comoros" },
        //    new Country { CountryName = "Congo" },
        //    new Country { CountryName = "Costa Rica" },
        //    new Country { CountryName = "Croatia" },
        //    new Country { CountryName = "Cuba" },
        //    new Country { CountryName = "Cyprus" },
        //    new Country { CountryName = "Czechia" },
        //    new Country { CountryName = "Denmark" },
        //    new Country { CountryName = "Djibouti" },
        //    new Country { CountryName = "Dominica" },
        //    new Country { CountryName = "Dominican Republic" },
        //    new Country { CountryName = "East Timor" },
        //    new Country { CountryName = "Ecuador" },
        //    new Country { CountryName = "Egypt" },
        //    new Country { CountryName = "El Salvador" },
        //    new Country { CountryName = "Equatorial Guinea" },
        //    new Country { CountryName = "Eritrea" },
        //    new Country { CountryName = "Estonia" },
        //    new Country { CountryName = "Eswatini" },
        //    new Country { CountryName = "Ethiopia" },
        //    new Country { CountryName = "Fiji" },
        //    new Country { CountryName = "Finland" },
        //    new Country { CountryName = "France" },
        //    new Country { CountryName = "Gabon" },
        //    new Country { CountryName = "Gambia" },
        //    new Country { CountryName = "Georgia" },
        //    new Country { CountryName = "Germany" },
        //    new Country { CountryName = "Ghana" },
        //    new Country { CountryName = "Greece" },
        //    new Country { CountryName = "Grenada" },
        //    new Country { CountryName = "Guatemala" },
        //    new Country { CountryName = "Guinea" },
        //    new Country { CountryName = "Guinea-Bissau" },
        //    new Country { CountryName = "Guyana" },
        //    new Country { CountryName = "Haiti" },
        //    new Country { CountryName = "Honduras" },
        //    new Country { CountryName = "Hungary" },
        //    new Country { CountryName = "Iceland" },
        //    new Country { CountryName = "India" },
        //    new Country { CountryName = "Indonesia" },
        //    new Country { CountryName = "Iran" },
        //    new Country { CountryName = "Iraq" },
        //    new Country { CountryName = "Ireland" },
        //    new Country { CountryName = "Italy" },
        //    new Country { CountryName = "Jamaica" },
        //    new Country { CountryName = "Japan" },
        //    new Country { CountryName = "Jordan" },
        //    new Country { CountryName = "Kazakhstan" },
        //    new Country { CountryName = "Kenya" },
        //    new Country { CountryName = "Kiribati" },
        //    new Country { CountryName = "Korea, North" },
        //    new Country { CountryName = "Korea, South" },
        //    new Country { CountryName = "Kosovo" },
        //    new Country { CountryName = "Kuwait" },
        //    new Country { CountryName = "Kyrgyzstan" },
        //    new Country { CountryName = "Laos" },
        //    new Country { CountryName = "Latvia" },
        //    new Country { CountryName = "Lebanon" },
        //    new Country { CountryName = "Lesotho" },
        //    new Country { CountryName = "Liberia" },
        //    new Country { CountryName = "Libya" },
        //    new Country {  CountryName = "Liechtenstein" },
        //    new Country {  CountryName = "Lithuania" },
        //    new Country {  CountryName = "Luxembourg" },
        //    new Country {  CountryName = "Madagascar" },
        //    new Country {  CountryName = "Malawi" },
        //    new Country {  CountryName = "Malaysia" },
        //    new Country {  CountryName = "Maldives" },
        //    new Country {  CountryName = "Mali" },
        //    new Country {  CountryName = "Malta" },
        //    new Country {  CountryName = "Marshall Islands" },
        //    new Country {  CountryName = "Mauritania" },
        //    new Country {  CountryName = "Mauritius" },
        //    new Country {  CountryName = "Mexico" },
        //    new Country {  CountryName = "Micronesia" },
        //    new Country {  CountryName = "Moldova" },
        //    new Country {  CountryName = "Monaco" },
        //    new Country {  CountryName = "Mongolia" },
        //    new Country {  CountryName = "Montenegro" },
        //    new Country {  CountryName = "Morocco" },
        //    new Country {  CountryName = "Mozambique" },
        //    new Country {  CountryName = "Myanmar" },
        //    new Country {  CountryName = "Namibia" },
        //    new Country {  CountryName = "Nauru" },
        //    new Country {  CountryName = "Nepal" },
        //    new Country {  CountryName = "Netherlands" },
        //    new Country {  CountryName = "New Zealand" },
        //    new Country {  CountryName = "Nicaragua" },
        //    new Country {  CountryName = "Niger" },
        //    new Country {  CountryName = "Nigeria" },
        //    new Country {  CountryName = "North Macedonia" },
        //    new Country {  CountryName = "Norway" },
        //    new Country {  CountryName = "Oman" },
        //    new Country {  CountryName = "Pakistan" },
        //    new Country {  CountryName = "Palau" },
        //    new Country {  CountryName = "Palestine" },
        //    new Country {  CountryName = "Panama" },
        //    new Country {  CountryName = "Papua New Guinea" },
        //    new Country {  CountryName = "Paraguay" },
        //    new Country {  CountryName = "Peru" },
        //    new Country {  CountryName = "Philippines" },
        //    new Country {  CountryName = "Poland" },
        //    new Country {  CountryName = "Portugal" },
        //    new Country {  CountryName = "Qatar" },
        //    new Country {  CountryName = "Romania" },
        //    new Country {  CountryName = "Russia" },
        //    new Country {  CountryName = "Rwanda" },
        //    new Country {  CountryName = "Saint Kitts and Nevis" },
        //    new Country {  CountryName = "Saint Lucia" },
        //    new Country {  CountryName = "Saint Vincent and the Grenadines" },
        //    new Country {  CountryName = "Samoa" },
        //    new Country {  CountryName = "San Marino" },
        //    new Country {  CountryName = "Sao Tome and Principe" },
        //    new Country {  CountryName = "Saudi Arabia" },
        //    new Country {  CountryName = "Senegal" },
        //    new Country {  CountryName = "Serbia" },
        //    new Country {  CountryName = "Seychelles" },
        //    new Country {  CountryName = "Sierra Leone" },
        //    new Country {  CountryName = "Singapore" },
        //    new Country {  CountryName = "Slovakia" },
        //    new Country {  CountryName = "Slovenia" },
        //    new Country {  CountryName = "Solomon Islands" },
        //    new Country {  CountryName = "Somalia" },
        //    new Country {  CountryName = "South Africa" },
        //    new Country {  CountryName = "South Sudan" },
        //    new Country {  CountryName = "Spain" },
        //    new Country {  CountryName = "Sri Lanka" },
        //    new Country {  CountryName = "Sudan" },
        //    new Country {  CountryName = "Suriname" },
        //    new Country {  CountryName = "Sweden" },
        //    new Country {  CountryName = "Switzerland" },
        //    new Country {  CountryName = "Syria" },
        //    new Country {  CountryName = "Taiwan" },
        //    new Country {  CountryName = "Tajikistan" },
        //    new Country {  CountryName = "Tanzania" },
        //    new Country {  CountryName = "Thailand" },
        //    new Country {  CountryName = "Togo" },
        //    new Country {  CountryName = "Tonga" },
        //    new Country {  CountryName = "Trinidad and Tobago" },
        //    new Country {  CountryName = "Tunisia" },
        //    new Country {  CountryName = "Turkey" },
        //    new Country {  CountryName = "Turkmenistan" },
        //    new Country {  CountryName = "Tuvalu" },
        //    new Country {  CountryName = "Uganda" },
        //    new Country {  CountryName = "Ukraine" },
        //    new Country {  CountryName = "United Arab Emirates" },
        //    new Country {  CountryName = "United Kingdom" },
        //    new Country {  CountryName = "United States" },
        //    new Country {  CountryName = "Uruguay" },
        //    new Country {  CountryName = "Uzbekistan" },
        //    new Country {  CountryName = "Vanuatu" },
        //    new Country {  CountryName = "Vatican City" },
        //    new Country {  CountryName = "Venezuela" },
        //    new Country {  CountryName = "Vietnam" },
        //    new Country {  CountryName = "Yemen" },
        //    new Country {  CountryName = "Zambia" },
        //    new Country {  CountryName = "Zimbabwe" }
        //};

        //    _context.Countries.AddRange(countries);
        //    _context.SaveChanges();



        //}



    }
}
