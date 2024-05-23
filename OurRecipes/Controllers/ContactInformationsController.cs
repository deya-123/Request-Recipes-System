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
    public class ContactInformationsController : Controller
    {
        private readonly AppDbContext _context;

        public ContactInformationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ContactInformations
        public async Task<IActionResult> Index()
        {
              return _context.ContactInfos != null ? 
                          View(await _context.ContactInfos.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.ContactInfos'  is null.");
        }

        // GET: ContactInformations/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.ContactInfos == null)
            {
                return NotFound();
            }

            var contactInfo = await _context.ContactInfos
                .FirstOrDefaultAsync(m => m.ContactInfoId == id);
            if (contactInfo == null)
            {
                return NotFound();
            }

            return View(contactInfo);
        }

        // GET: ContactInformations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactInfoId,LocationOnMap,Address,Phone,Email")] ContactInfo contactInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactInfo);
        }

        // GET: ContactInformations/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.ContactInfos == null)
            {
                return NotFound();
            }

            var contactInfo = await _context.ContactInfos.FindAsync(id);
            if (contactInfo == null)
            {
                return NotFound();
            }
            return View(contactInfo);
        }

        // POST: ContactInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ContactInfoId,LocationOnMap,Address,Phone,Email")] ContactInfo contactInfo)
        {
            if (id != contactInfo.ContactInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactInfoExists(contactInfo.ContactInfoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectHelper.RedirectByRoleId("ContactInfoPage", 1);
            }
            return RedirectHelper.RedirectByRoleId("ContactInfoPage", 1);
        }

        // GET: ContactInformations/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.ContactInfos == null)
            {
                return NotFound();
            }

            var contactInfo = await _context.ContactInfos
                .FirstOrDefaultAsync(m => m.ContactInfoId == id);
            if (contactInfo == null)
            {
                return NotFound();
            }

            return View(contactInfo);
        }

        // POST: ContactInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.ContactInfos == null)
            {
                return Problem("Entity set 'AppDbContext.ContactInfos'  is null.");
            }
            var contactInfo = await _context.ContactInfos.FindAsync(id);
            if (contactInfo != null)
            {
                _context.ContactInfos.Remove(contactInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactInfoExists(decimal id)
        {
          return (_context.ContactInfos?.Any(e => e.ContactInfoId == id)).GetValueOrDefault();
        }



        public async Task<IActionResult> GetContactInformationById(decimal id)
        {

            return Json(await _context.ContactInfos.FirstOrDefaultAsync(e => e.ContactInfoId == id));
        }
        public ActionResult LoadData()
        {


            return Json(new
            {
                data = _context.ContactInfos.Select(e => new
                {
                    e.ContactInfoId,
                    e.LocationOnMap,
                    e.Address,
                    e.Email,
                    e.Phone



                })

                .ToList()
            }); ;
        }

    }
}
