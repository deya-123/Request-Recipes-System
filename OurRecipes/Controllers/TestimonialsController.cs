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
    public class TestimonialsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
       
       
      

        public TestimonialsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Testimonials
        public async Task<IActionResult> Index()
        {
              return _context.Testimonials != null ? 
                          View(await _context.Testimonials.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Testimonials'  is null.");
        }

        // GET: Testimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .FirstOrDefaultAsync(m => m.TestimonialId == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // GET: Testimonials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestimonialText")] Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {

                var userId=HttpContext.Session.GetInt32("userId");
                if (userId != null)
                {
                    testimonial.UserId = userId;

                }
                else { testimonial.UserId = 124; }


                testimonial.TestimonialStatus = "Waiting";
                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction("TestimonialsPage", "AdminDash");
            }
            return RedirectToAction("TestimonialsPage", "AdminDash");
        }

        // GET: Testimonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            return View(testimonial);
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("TestimonialId,TestimonialText,TestimonialStatus")] TestimonialViewModel testimonialViewModel)
        {
            if (id != testimonialViewModel.TestimonialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var testimonial= _context.Testimonials.FirstOrDefault(e => e.TestimonialId == testimonialViewModel.TestimonialId);
                    if (testimonial != null) {

                       
                        _mapper.Map<TestimonialViewModel,Testimonial>(testimonialViewModel,testimonial );
                        testimonial.TestimonialStatus = "Waiting";
                        if (HttpContext.Session.GetString("roleName") == "admin") {

                            testimonial.TestimonialStatus = testimonialViewModel.TestimonialStatus;
                        }


                        _context.Update(testimonial);
                        await _context.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(testimonialViewModel.TestimonialId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("TestimonialsPage", "AdminDash");
            }
            return RedirectToAction("TestimonialsPage", "AdminDash");
        }

        // GET: Testimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .FirstOrDefaultAsync(m => m.TestimonialId == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Testimonials == null)
            {
                return Problem("Entity set 'AppDbContext.Testimonials'  is null.");
            }
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("TestimonialsPage", "AdminDash");
        }
        public async Task<IActionResult> GetTestimonialById(decimal id)
        {

            return Json(await _context.Testimonials.FirstOrDefaultAsync(e => e.TestimonialId == id));
        }
        public async Task<IActionResult> GetTestimonialsByUserId(decimal id)
        {

            return   Json(new
            {
                data = _context.Testimonials.Where(e=>e.UserId==id).Select(e => new
                {
               
                    e.TestimonialId,
                    e.TestimonialStatus,
                    e.TestimonialText
                 

                })

                .ToList()
            }); 
        }
        private bool TestimonialExists(decimal id)
        {
          return (_context.Testimonials?.Any(e => e.TestimonialId == id)).GetValueOrDefault();
        }

        public ActionResult LoadData()
        {


            return Json(new
            {
                data = _context.Testimonials.Include(e => e.User).Select(e => new
                {
                    e.UserId,
                    e.TestimonialId,
                    e.TestimonialStatus,
                    e.TestimonialText,
                    UserName = e.User != null ? e.User.UserName: null, 
                   
                })

                .ToList()
            }); ;
        }
    }
}
