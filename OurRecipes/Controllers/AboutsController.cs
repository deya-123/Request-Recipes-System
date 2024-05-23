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
    public class AboutsController : Controller
    {
        private readonly AppDbContext _context;


        private readonly IWebHostEnvironment _environment;

        private readonly IMapper _mapper;
        public AboutsController(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;

        }

        // GET: Abouts
        public async Task<IActionResult> Index()
        {
              return _context.Abouts != null ? 
                          View(await _context.Abouts.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Abouts'  is null.");
        }

        // GET: Abouts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Abouts == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts
                .FirstOrDefaultAsync(m => m.AboutId == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // GET: Abouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AboutId,AboutTitle,AboutBody,AboutImage")] About about)
        {
            if (ModelState.IsValid)
            {
                _context.Add(about);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: Abouts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Abouts == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        // POST: Abouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, AboutViewModel aboutViewModel)
        {
            if (id != aboutViewModel.AboutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var about = await _context.Abouts.FirstOrDefaultAsync(e => e.AboutId == aboutViewModel.AboutId);
           
              

                try
                {
                    if (about is not null)
                    {


                        if (aboutViewModel.AboutImageFile != null)
                        {

                            var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot");

                            if (about.AboutImage != null)
                            {

                                System.IO.File.Delete(Path.Combine(viewFolderPath, "images") + "\\" + about.AboutImage);
                            }

                            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(aboutViewModel.AboutImageFile.FileName);
                            var imagePath = Path.Combine(viewFolderPath, "images") + "\\" + imageName;
                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                await aboutViewModel.AboutImageFile.CopyToAsync(stream);
                                about.AboutImage = imageName;


                            }

                        }
                  


                        var mappingConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<AboutViewModel, About>();

                        });

                        var mapper = mappingConfig.CreateMapper();

                        mapper.Map<AboutViewModel, About>(aboutViewModel, about);

                        _context.Update(about);
                        await _context.SaveChangesAsync();

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.AboutId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectHelper.RedirectByRoleId("AboutPage", 1);
            }
            return RedirectHelper.RedirectByRoleId("AboutPage", 1);
        }

        // GET: Abouts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Abouts == null)
            {
                return NotFound();
            }

            var about = await _context.Abouts
                .FirstOrDefaultAsync(m => m.AboutId == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // POST: Abouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Abouts == null)
            {
                return Problem("Entity set 'AppDbContext.Abouts'  is null.");
            }
            var about = await _context.Abouts.FindAsync(id);
            if (about != null)
            {
                _context.Abouts.Remove(about);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutExists(decimal id)
        {
          return (_context.Abouts?.Any(e => e.AboutId == id)).GetValueOrDefault();
        }



        public async Task<IActionResult> GetAboutById(decimal id)
        {

            return Json(await _context.Abouts.FirstOrDefaultAsync(e => e.AboutId == id));
        }
        public ActionResult LoadData()
        {


            return Json(new
            {
                data = _context.Abouts.Select(e => new
                {
                    e.AboutId,
                    e.AboutBody,
                    e.AboutTitle,
                    e.AboutImage,
                    



                })

                .ToList()
            }); ;
        }

    }
}
