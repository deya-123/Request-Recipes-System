using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OurRecipes.Data;
using OurRecipes.Models;
using OurRecipes.ViewModels;

namespace OurRecipes.Controllers
{
    public class RecipeCategoriesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
   

        public RecipeCategoriesController(AppDbContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        // GET: RecipeCategories
        public async Task<IActionResult> Index()
        {
              return _context.RecipeCategories != null ? 
                          View(await _context.RecipeCategories.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.RecipeCategories'  is null.");
        }

        // GET: RecipeCategories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.RecipeCategories == null)
            {
                return NotFound();
            }

            var recipeCategory = await _context.RecipeCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (recipeCategory == null)
            {
                return NotFound();
            }

            return View(recipeCategory);
        }

        // GET: RecipeCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecipeCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryTypeId")] RecipeCategory recipeCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("CategoriesPage", "AdminDash");
            }
            return RedirectToAction("CategoriesPage", "AdminDash");
        }

        // GET: RecipeCategories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.RecipeCategories == null)
            {
                return NotFound();
            }

            var recipeCategory = await _context.RecipeCategories.FindAsync(id);
            if (recipeCategory == null)
            {
                return NotFound();
            }
            return View(recipeCategory);
        }

        // POST: RecipeCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id,  RecipeCategoryViewModel recipeCategoryViewModel)
        {
            if (id != recipeCategoryViewModel.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var recipeCategory = _context.RecipeCategories.FirstOrDefault(e => e.CategoryId == recipeCategoryViewModel.CategoryId);
                    if (recipeCategory != null)
                    {

                        _mapper.Map(recipeCategoryViewModel, recipeCategory);
                        _context.Update(recipeCategory);
                        await _context.SaveChangesAsync();


                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeCategoryExists(recipeCategoryViewModel.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CategoriesPage", "AdminDash");
            }
            return RedirectToAction("CategoriesPage", "AdminDash");
        }

        // GET: RecipeCategories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.RecipeCategories == null)
            {
                return NotFound();
            }

            var recipeCategory = await _context.RecipeCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (recipeCategory == null)
            {
                return NotFound();
            }

            return View(recipeCategory);
        }
        
       public async Task<IActionResult> GetCateogryById(decimal id)
        {

            return Json(await _context.RecipeCategories.FirstOrDefaultAsync(e => e.CategoryId == id));
        }

        public  IActionResult GetCateogryByTypeId(decimal id)
        {

            return Json( _context.RecipeCategories.Where(e => e.CategoryTypeId== id));
        }


        // POST: RecipeCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.RecipeCategories == null)
            {
                return Problem("Entity set 'AppDbContext.RecipeCategories'  is null.");
            }
            var recipeCategory = await _context.RecipeCategories.FindAsync(id);
            if (recipeCategory != null)
            {
                _context.RecipeCategories.Remove(recipeCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("CategoriesPage", "AdminDash");
        }

        private bool RecipeCategoryExists(decimal id)
        {
          return (_context.RecipeCategories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }


        public ActionResult LoadData()
        {


            return Json(new
            {
                data = _context.RecipeCategories.Include(e=>e.CategoryType).Select(e => new
                {
                    e.CategoryId,
                    e.CategoryName,
                    CategoryType=e.CategoryType!=null ? e.CategoryType.RecipeCategoryTypeName:null
            


                })

                .ToList()
            }); ;
        }

    }
}
