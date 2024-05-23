using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using OurRecipes.Data;
using OurRecipes.Models;
using OurRecipes.ViewModels;

namespace OurRecipes.Controllers
{
    public class ContactsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        public ContactsController(AppDbContext context, IMapper mapper, IToastNotification notyf)
        {
            _context = context;
            _mapper = mapper;
            _toastNotification = notyf;
        }
       

  


        // GET: Contacts
        public async Task<IActionResult> Index()
        {
              return _context.Contacts != null ? 
                          View(await _context.Contacts.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Contacts'  is null.");
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }
        public async Task<IActionResult> GetContactById(decimal id)
        {

            return Json(await _context.Contacts.FirstOrDefaultAsync(e => e.ContactId == id));
        }
        
        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactMessage,ContactSenderEmail,ContactSenderName")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();

                _toastNotification.AddSuccessToastMessage("thank you, message has been sent", new NotyOptions()
                {
                    ProgressBar = true,
                    Timeout = 2000,
                    Theme = "metroui",
                    Layout = "bottomCenter",

                });

                return RedirectToAction("Contact", "Home");
            }
            return RedirectToAction("Contact", "Home");
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, ContactViewModel contactViewModel)
        {
            if (id != contactViewModel.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var contact= await _context.Contacts.FirstOrDefaultAsync(e => e.ContactId == contactViewModel.ContactId);
                    if (contact != null) {
                        _mapper.Map(contactViewModel,contact);
                        _context.Update(contact);
                        await _context.SaveChangesAsync();

                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contactViewModel.ContactId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ContactsPage", "AdminDash");
            }
            return RedirectToAction("ContactsPage", "AdminDash");
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set 'AppDbContext.Contacts'  is null.");
            }
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("ContactsPage", "AdminDash"); 
        }

        private bool ContactExists(decimal id)
        {
          return (_context.Contacts?.Any(e => e.ContactId == id)).GetValueOrDefault();
        }

        public ActionResult LoadData()
        {


            return Json(new
            {
                data = _context.Contacts.Select(e => new
                {
                    e.ContactId,
                    e.ContactMessage,
                    e.ContactSenderName,
                    e.ContactSenderEmail,
                    CreatedAt = e.CreatedAt != null ? e.CreatedAt.Value.Date.ToShortDateString() : null,



                })

                .ToList()
            }); ;
        }
    }
}
