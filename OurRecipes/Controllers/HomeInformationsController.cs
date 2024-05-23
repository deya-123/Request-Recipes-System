using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OurRecipes.Data;
using OurRecipes.Models;
using OurRecipes.Utilities;
using OurRecipes.ViewModels;

namespace OurRecipes.Controllers
{
    public class HomeInformationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        private readonly IMapper _mapper;
        public HomeInformationsController(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
           _environment = environment;

        }

    // GET: HomeInformations
    public async Task<IActionResult> Index()
        {
              return _context.Homes != null ? 
                          View(await _context.Homes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Homes'  is null.");
        }

        // GET: HomeInformations/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.HomeId == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // GET: HomeInformations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HomeId,HomeImage,HomeWebsiteName,HomeTitle,WorkingDays,FacbookLink,InsLink,YoutubeLink,HomeDesc,HomeLogo")] Home home)
        {
            if (ModelState.IsValid)
            {
                _context.Add(home);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(home);
        }

        // GET: HomeInformations/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }
            return View(home);
        }

        // POST: HomeInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id,  HomeViewModel homeViewModel)
        {


      
        



            if (id != homeViewModel.HomeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var home = await _context.Homes.FirstOrDefaultAsync(e => e.HomeId == homeViewModel.HomeId);
                try
                {


                    if (home is not null)
                    {


                        if (homeViewModel.HomeImageFile != null)
                        {

                            var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot");

                            if (home.HomeImage!= null)
                            {

                                System.IO.File.Delete(Path.Combine(viewFolderPath, "images") + "\\" + home.HomeImage);
                            }

                            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(homeViewModel.HomeImageFile.FileName);
                            var imagePath = Path.Combine(viewFolderPath, "images") + "\\" + imageName;
                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                await homeViewModel.HomeImageFile.CopyToAsync(stream);
                                home.HomeImage = imageName;


                            }

                        }
                        if (homeViewModel.HomeLogoFile != null)
                        {

                            var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot");

                            if (home.HomeLogo != null)
                            {

                                System.IO.File.Delete(Path.Combine(viewFolderPath, "images") + "\\" + home.HomeLogo);
                            }

                            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(homeViewModel.HomeLogoFile.FileName);
                            var imagePath = Path.Combine(viewFolderPath, "images") + "\\" + imageName;
                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                await homeViewModel.HomeLogoFile.CopyToAsync(stream);
                                home.HomeLogo = imageName;


                            }

                        }


                        var mappingConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<HomeViewModel, Home>();
                           
                        });

                        var mapper = mappingConfig.CreateMapper();

                        mapper.Map<HomeViewModel, Home>(homeViewModel, home);
                      
                        _context.Update(home);
                        await _context.SaveChangesAsync();

                    }



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeExists(home.HomeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectHelper.RedirectByRoleId("HomeInfoPage", 1);
            }
            return RedirectHelper.RedirectByRoleId("HomeInfoPage", 1);
        }

        // GET: HomeInformations/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Homes == null)
            {
                return NotFound();
            }

            var home = await _context.Homes
                .FirstOrDefaultAsync(m => m.HomeId == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);
        }

        // POST: HomeInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Homes == null)
            {
                return Problem("Entity set 'AppDbContext.Homes'  is null.");
            }
            var home = await _context.Homes.FindAsync(id);
            if (home != null)
            {
                _context.Homes.Remove(home);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeExists(decimal id)
        {
          return (_context.Homes?.Any(e => e.HomeId == id)).GetValueOrDefault();
        }




        public async Task<IActionResult> GetHomeInfoById(decimal id)
        {

            return Json(await _context.Homes.FirstOrDefaultAsync(e => e.HomeId == id));
        }
        public ActionResult LoadData()
        {


            return Json(new
            {
                data = _context.Homes.Select(e => new
                {
                    e.HomeId,
                    e.HomeWebsiteName,
                    e.HomeDesc,
                   
                   e.HomeTitle,
               


                })

                .ToList()
            }); ;
        }





    }
}
