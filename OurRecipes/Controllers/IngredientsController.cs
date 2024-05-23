using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OurRecipes.Data;
using OurRecipes.Models;
using OurRecipes.Utilities;
using OurRecipes.ViewModels;

namespace OurRecipes.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly AppDbContext _context;

        public IngredientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ingredients
        public async Task<IActionResult> Index()
        {
              return _context.Ingredients != null ? 
                          View(await _context.Ingredients.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Ingredients'  is null.");
        }

        // GET: Ingredients/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Ingredients == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients
                .FirstOrDefaultAsync(m => m.IngredientId == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // GET: Ingredients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
  
        public async Task<IActionResult> Create([Bind("IngredientCustomName,IngredientQuantity,IngredientUnitId,RecipeId",Prefix = "IngredientViewModel")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredient);
                await _context.SaveChangesAsync();
                return RedirectHelper.RedirectByRoleId("RecipesPage", 1);
            }
            return RedirectHelper.RedirectByRoleId("RecipesPage", 1);
        }

        // GET: Ingredients/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Ingredients == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IngredientId,IngredientCustomName,IngredientQuantity,IngredientUnitId", Prefix = "IngredientViewModel")] Ingredient ingredient)
        {
            if (id != ingredient.IngredientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var ingredientOrginal = await _context.Ingredients.FirstOrDefaultAsync(e => e.IngredientId == ingredient.IngredientId);
                try
                {
                    if (ingredientOrginal != null) {
                        ingredientOrginal.IngredientCustomName = ingredient.IngredientCustomName;
                        ingredientOrginal.IngredientQuantity = ingredient.IngredientQuantity;
                        ingredientOrginal.IngredientUnitId = ingredient.IngredientUnitId;



                        _context.Update(ingredientOrginal);
                        await _context.SaveChangesAsync();
                    }
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientExists(ingredient.IngredientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectHelper.RedirectByRoleId("RecipesPage", 1);
            }
            return RedirectHelper.RedirectByRoleId("RecipesPage", 1);
        }

        // GET: Ingredients/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Ingredients == null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients
                .FirstOrDefaultAsync(m => m.IngredientId == id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Ingredients == null)
            {
                return Problem("Entity set 'AppDbContext.Ingredients'  is null.");
            }
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectHelper.RedirectByRoleId("RecipesPage", 1);
        }

        private bool IngredientExists(decimal id)
        {
          return (_context.Ingredients?.Any(e => e.IngredientId == id)).GetValueOrDefault();
        }

        public ActionResult LoadDataByRecipeId(decimal recipeId)
        {


            return Json(new
            {
                data = _context.Ingredients.Where(e => e.RecipeId == recipeId).Include(e => e.IngredientUnit).Select(e => new
                {
                    e.IngredientCustomName,
                    e.IngredientQuantity,
                    e.IngredientId,
                    IngredientUnitName = e.IngredientUnit != null ? e.IngredientUnit.IngredientUnitName : null, // Check if UserCountry is null
                   

                })

                .ToList()
            }); ;
        }





        public ActionResult GetIngredientById(decimal id)
        {


            return Json(

             _context.Ingredients.Include(e => e.IngredientUnit).Select(e => new
             {
                 e.IngredientCustomName,
                 e.IngredientQuantity,
                 e.IngredientId,
                 e.IngredientUnitId,
                 IngredientUnitName = e.IngredientUnit != null ? e.IngredientUnit.IngredientUnitName : null, // Check if UserCountry is null

             }).FirstOrDefault(e => e.IngredientId == id)


            ); 
        }



    }
}
