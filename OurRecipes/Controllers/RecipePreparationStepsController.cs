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

namespace OurRecipes.Controllers
{
    public class RecipePreparationStepsController : Controller
    {
        private readonly AppDbContext _context;

        public RecipePreparationStepsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RecipePreparationSteps
        public async Task<IActionResult> Index()
        {
              return _context.RecipePreparationSteps != null ? 
                          View(await _context.RecipePreparationSteps.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.RecipePreparationSteps'  is null.");
        }

        // GET: RecipePreparationSteps/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.RecipePreparationSteps == null)
            {
                return NotFound();
            }

            var recipePreparationStep = await _context.RecipePreparationSteps
                .FirstOrDefaultAsync(m => m.RecipePreparationStepId == id);
            if (recipePreparationStep == null)
            {
                return NotFound();
            }

            return View(recipePreparationStep);
        }

        // GET: RecipePreparationSteps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecipePreparationSteps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipePreparationStepDescription,RecipeId",Prefix = "RecipePreparationStepViewModel")] RecipePreparationStep recipePreparationStep)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipePreparationStep);
                await _context.SaveChangesAsync();
                return RedirectHelper.RedirectByRoleId("RecipesPage", 1);
            }
            return RedirectHelper.RedirectByRoleId("RecipesPage", 1);
        }

        // GET: RecipePreparationSteps/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.RecipePreparationSteps == null)
            {
                return NotFound();
            }

            var recipePreparationStep = await _context.RecipePreparationSteps.FindAsync(id);
            if (recipePreparationStep == null)
            {
                return NotFound();
            }
            return View(recipePreparationStep);
        }

        // POST: RecipePreparationSteps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("RecipePreparationStepId,RecipePreparationStepDescription", Prefix = "RecipePreparationStepViewModel")] RecipePreparationStep recipePreparationStep)
        {
            if (id != recipePreparationStep.RecipePreparationStepId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var recipePreparation = await _context.RecipePreparationSteps.FirstOrDefaultAsync(e => e.RecipePreparationStepId == recipePreparationStep.RecipePreparationStepId);
                try
                {
                    if (recipePreparation != null)
                    {

                        recipePreparation.RecipePreparationStepDescription = recipePreparationStep.RecipePreparationStepDescription;
                        

                        _context.Update(recipePreparation);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipePreparationStepExists(recipePreparationStep.RecipePreparationStepId))
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

        // GET: RecipePreparationSteps/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.RecipePreparationSteps == null)
            {
                return NotFound();
            }

            var recipePreparationStep = await _context.RecipePreparationSteps
                .FirstOrDefaultAsync(m => m.RecipePreparationStepId == id);
            if (recipePreparationStep == null)
            {
                return NotFound();
            }

            return View(recipePreparationStep);
        }

        // POST: RecipePreparationSteps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.RecipePreparationSteps == null)
            {
                return Problem("Entity set 'AppDbContext.RecipePreparationSteps'  is null.");
            }
            var recipePreparationStep = await _context.RecipePreparationSteps.FindAsync(id);
            if (recipePreparationStep != null)
            {
                _context.RecipePreparationSteps.Remove(recipePreparationStep);
            }
            
            await _context.SaveChangesAsync();
            return RedirectHelper.RedirectByRoleId("RecipesPage", 1);
        }

        private bool RecipePreparationStepExists(decimal id)
        {
          return (_context.RecipePreparationSteps?.Any(e => e.RecipePreparationStepId == id)).GetValueOrDefault();
        }
        public ActionResult LoadDataByRecipeId(decimal recipeId)
        {


            return Json(new
            {
                data = _context.RecipePreparationSteps.Where(e=>e.RecipeId==recipeId).Select(e => new
                {
                    e.RecipePreparationStepId,
                    e.RecipePreparationStepDescription,
            

                })

                .ToList()
            }); 
        }


        public ActionResult GetStepById(decimal id)
        {


            return Json(

             _context.RecipePreparationSteps.Select(e => new
             {
                 e.RecipePreparationStepId,
                 e.RecipePreparationStepDescription
             }).FirstOrDefault(e => e.RecipePreparationStepId == id)


            ); ;
        }
        

    }
}
