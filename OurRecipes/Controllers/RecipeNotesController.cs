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
    public class RecipeNotesController : Controller
    {
        private readonly AppDbContext _context;

        public RecipeNotesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RecipeNotes
        public async Task<IActionResult> Index()
        {
              return _context.RecipeNotes != null ? 
                          View(await _context.RecipeNotes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.RecipeNotes'  is null.");
        }

        // GET: RecipeNotes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.RecipeNotes == null)
            {
                return NotFound();
            }

            var recipeNote = await _context.RecipeNotes
                .FirstOrDefaultAsync(m => m.RecipeNoteId == id);
            if (recipeNote == null)
            {
                return NotFound();
            }

            return View(recipeNote);
        }

        // GET: RecipeNotes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecipeNotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeNoteTitle,RecipeNoteDescription,RecipeId", Prefix = "RecipeNotesViewModel")] RecipeNote recipeNote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeNote);
                await _context.SaveChangesAsync();
                return RedirectHelper.RedirectByRoleId("RecipesPage", 1);
            }
            return RedirectHelper.RedirectByRoleId("RecipesPage", 1);
        }

        // GET: RecipeNotes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.RecipeNotes == null)
            {
                return NotFound();
            }

            var recipeNote = await _context.RecipeNotes.FindAsync(id);
            if (recipeNote == null)
            {
                return NotFound();
            }
            return View(recipeNote);
        }

        // POST: RecipeNotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("RecipeNoteId,RecipeNoteTitle,RecipeNoteDescription", Prefix = "RecipeNotesViewModel")] RecipeNote recipeNote)
        {
            if (id != recipeNote.RecipeNoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var recipeNoteOrginal = await _context.RecipeNotes.FirstOrDefaultAsync(e => e.RecipeNoteId == recipeNote.RecipeNoteId);
                try
                {
                    if (recipeNoteOrginal != null)
                    {

                        recipeNoteOrginal.RecipeNoteDescription = recipeNote.RecipeNoteDescription;
                        recipeNoteOrginal.RecipeNoteTitle = recipeNote.RecipeNoteTitle;
                        _context.Update(recipeNoteOrginal);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeNoteExists(recipeNote.RecipeNoteId))
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

        // GET: RecipeNotes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.RecipeNotes == null)
            {
                return NotFound();
            }

            var recipeNote = await _context.RecipeNotes
                .FirstOrDefaultAsync(m => m.RecipeNoteId == id);
            if (recipeNote == null)
            {
                return NotFound();
            }

            return View(recipeNote);
        }

        // POST: RecipeNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.RecipeNotes == null)
            {
                return Problem("Entity set 'AppDbContext.RecipeNotes'  is null.");
            }
            var recipeNote = await _context.RecipeNotes.FindAsync(id);
            if (recipeNote != null)
            {
                _context.RecipeNotes.Remove(recipeNote);
            }
            
            await _context.SaveChangesAsync();
            return RedirectHelper.RedirectByRoleId("RecipesPage", 1);
        }

        private bool RecipeNoteExists(decimal id)
        {
          return (_context.RecipeNotes?.Any(e => e.RecipeNoteId == id)).GetValueOrDefault();
        }



        public ActionResult GetRecipeNoteById(decimal id)
        {


            return Json(

             _context.RecipeNotes.Select(e => new
             {
                 e.RecipeId,
                 e.RecipeNoteId,
                 e.RecipeNoteDescription,
                 e.RecipeNoteTitle,
             }).FirstOrDefault(e => e.RecipeNoteId == id)


            ); 



        }


        public ActionResult LoadDataByRecipeId(decimal recipeId)
        {


            return Json(new
            {
                data = _context.RecipeNotes.Where(e => e.RecipeId == recipeId).Select(e => new
                {
                    e.RecipeId,
                    e.RecipeNoteId,
                    e.RecipeNoteDescription,
                    e.RecipeNoteTitle,
         
                })
                .ToList()
            }); ;
        }


    }
}
