using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using Org.BouncyCastle.Asn1.Pkcs;
using OurRecipes.Data;
using OurRecipes.Models;
using OurRecipes.Utilities;
using OurRecipes.ViewModels;

namespace OurRecipes.Controllers
{
    public class RecipesController : Controller
    {

        private readonly IWebHostEnvironment _environment;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        public RecipesController(AppDbContext context, IMapper mapper, IToastNotification notyf, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _toastNotification = notyf;
            _environment = environment;
        }


        // GET: Recipes
        public async Task<IActionResult> Index()
        {
              return _context.Recipes != null ? 
                          View(await _context.Recipes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Recipes'  is null.");
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            return View();
        }




        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(Prefix = "RecipeViewModel")]  RecipeViewModel recipeViewModel)
        {

            if (ModelState.IsValid)
            {
                var mappingConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RecipeViewModel, Recipe>()
                     
                       .ForMember(dest => dest.RecipeStatus, src => src.Ignore())
                       .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
                });

                var mapper = mappingConfig.CreateMapper();

                var recipe = mapper.Map<RecipeViewModel, Recipe>(recipeViewModel);

                if (recipeViewModel.RecipeMainImage != null)
                {

                    var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot");


                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(recipeViewModel.RecipeMainImage.FileName);
                    var imagePath = Path.Combine(viewFolderPath, "recipesImage") + "\\" + imageName;
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await recipeViewModel.RecipeMainImage.CopyToAsync(stream);
                        recipe.RecipeMainImgPath = imageName;


                    }

                }
                if (recipeViewModel.RecipeCardImage != null)
                {

                    var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot");


                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(recipeViewModel.RecipeCardImage.FileName);
                    var imagePath = Path.Combine(viewFolderPath, "recipesImage") + "\\" + imageName;
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await recipeViewModel.RecipeCardImage.CopyToAsync(stream);
                        recipe.RecipeCardImgPath = imageName;


                    }

                }
                if (recipeViewModel.RecipeVideo != null)
                {

                    var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot");


                    var videoName = Guid.NewGuid().ToString() + Path.GetExtension(recipeViewModel.RecipeVideo.FileName);
                    var videoPath = Path.Combine(viewFolderPath, "recipesVideos") + "\\" + videoName;
                    using (var stream = new FileStream(videoPath, FileMode.Create))
                    {
                        await recipeViewModel.RecipeVideo.CopyToAsync(stream);
                        recipe.RecipeVideoPath = videoName;


                    }

                }
                recipe.RecipeStatus = "Waiting";
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();

                return RedirectHelper.RedirectByRoleId("RecipesPage",3);
            }
            return RedirectHelper.RedirectByRoleId("RecipesPage", 3);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind(Prefix = "RecipeViewModel")] RecipeViewModel recipeViewModel)
        {
            if (id != recipeViewModel.RecipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var recipe = await _context.Recipes.FirstOrDefaultAsync(e => e.RecipeId == recipeViewModel.RecipeId);
                try
                {
                    
                    if (recipe is not null) {

                 
                        if (recipeViewModel.RecipeMainImage != null)
                        {

                            var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
                            
                            if ( recipe.RecipeMainImgPath != null)
                            {

                                System.IO.File.Delete(Path.Combine(viewFolderPath, "recipesImage") + "\\" + recipe.RecipeMainImgPath);
                            }

                            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(recipeViewModel.RecipeMainImage.FileName);
                            var imagePath = Path.Combine(viewFolderPath, "recipesImage") + "\\" + imageName;
                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                await recipeViewModel.RecipeMainImage.CopyToAsync(stream);
                                recipe.RecipeMainImgPath = imageName;


                            }

                        }
                        if (recipeViewModel.RecipeCardImage != null)
                        {

                            var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot");

                            if (recipe.RecipeCardImgPath != null)
                            {

                                System.IO.File.Delete(Path.Combine(viewFolderPath, "recipesImage") + "\\" + recipe.RecipeCardImgPath);
                            }

                            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(recipeViewModel.RecipeCardImage.FileName);
                            var imagePath = Path.Combine(viewFolderPath, "recipesImage") + "\\" + imageName;
                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                await recipeViewModel.RecipeCardImage.CopyToAsync(stream);
                                recipe.RecipeCardImgPath = imageName;


                            }

                        }
                        if (recipeViewModel.RecipeVideo != null)
                        {

                            var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot");
                            if (recipe.RecipeVideoPath != null)
                            {

                                System.IO.File.Delete(Path.Combine(viewFolderPath, "recipesVideos") + "\\" + recipe.RecipeVideoPath);
                            }

                            var videoName = Guid.NewGuid().ToString() + Path.GetExtension(recipeViewModel.RecipeVideo.FileName);
                            var videoPath = Path.Combine(viewFolderPath, "recipesVideos") + "\\" + videoName;
                            using (var stream = new FileStream(videoPath, FileMode.Create))
                            {
                                await recipeViewModel.RecipeVideo.CopyToAsync(stream);
                                recipe.RecipeVideoPath = videoName;


                            }

                        }

                        var mappingConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<RecipeViewModel, Recipe>()
                               .ForMember(dest => dest.ChiefId, src => src.Ignore())
                               .ForMember(dest => dest.RecipeStatus, src => src.Ignore())
                               .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
                        });

                        var mapper = mappingConfig.CreateMapper();

                        mapper.Map<RecipeViewModel, Recipe>(recipeViewModel, recipe);
                        recipe.RecipeStatus = "Waiting";
                        _context.Update(recipe);
                        await _context.SaveChangesAsync();

                    }

            
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecipeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectHelper.RedirectByRoleId("RecipesPage", 3);
            }
            return RedirectHelper.RedirectByRoleId("RecipesPage", 3);
        }

        public async Task<IActionResult> UpdateStatus([Bind(Prefix = "UpdateStatusViewModel")] UpdateStatusViewModel updateStatusViewModel)
        {
         

            if (ModelState.IsValid)
            {
                var recipe = await _context.Recipes.FirstOrDefaultAsync(e => e.RecipeId == updateStatusViewModel.RecipeId);
                try
                {

                    if (recipe is not null)
                    {

                        recipe.RecipeStatus = updateStatusViewModel.RecipeStatus;
                         
                        _context.Update(recipe);
                        await _context.SaveChangesAsync();

                    }


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecipeId))
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





        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'AppDbContext.Recipes'  is null.");
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectHelper.RedirectByRoleId("RecipesPage", 1);
        }

        private bool RecipeExists(decimal id)
        {
          return (_context.Recipes?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        }


        public ActionResult LoadData()
        {


            return Json(new
            {
                data = _context.Recipes.Include(e => e.RecipeCategory).Select(e => new
                {
                    e.RecipeId,
                    e.RecipeName,
                    e.RecipePrice,
                    CategoryName = e.RecipeCategory != null ? e.RecipeCategory.CategoryName : null, // Check if UserCountry is null
                    e.RecipeCookingTimeMinutes,
                    e.RecipePreparingTimeMinutes,
                    e.RecipeServings,
                    e.RecipeDescription,
                    e.RecipeExplanation,
                    e.RecipeStatus,
                     CreatedAt = e.CreatedAt != null ? e.CreatedAt.Value.Date.ToShortDateString() : null,
 
                })

                .ToList()
            }); ;
        }

        public ActionResult LoadDataByChiefId( decimal id)
        {


            return Json(new
            {
                data = _context.Recipes.Include(e => e.RecipeCategory).Where(e=>e.ChiefId==id).Select(e => new
                {
                    e.RecipeId,
                    e.RecipeName,
                    e.RecipePrice,
                    CategoryName = e.RecipeCategory != null ? e.RecipeCategory.CategoryName : null, // Check if UserCountry is null
                    e.RecipeCookingTimeMinutes,
                    e.RecipePreparingTimeMinutes,
                    e.RecipeServings,
                    e.RecipeDescription,
                    e.RecipeExplanation,
                    e.RecipeStatus,
                    CreatedAt = e.CreatedAt != null ? e.CreatedAt.Value.Date.ToShortDateString() : null,

                })

                .ToList()
            }); ;
        }


  

               public ActionResult SearchByDateAndChiefId(DateTime? minDate, DateTime? maxDate, decimal chiefId)
        {
            IQueryable<Recipe> query = _context.Recipes.Where(e=>e.ChiefId==chiefId);


            if (minDate != null)
            {
                query = query.Where(r => r.CreatedAt >= minDate);
            }
            if (maxDate != null)
            {
                query = query.Where(r => r.CreatedAt <= maxDate);
            }

            return Json(new
            {
                data = query.Include(e => e.RecipeCategory).Select(e => new
                {
                    e.RecipeId,
                    e.RecipeName,
                    e.RecipePrice,
                    CategoryName = e.RecipeCategory != null ? e.RecipeCategory.CategoryName : null, // Check if UserCountry is null
                    e.RecipeCookingTimeMinutes,
                    e.RecipePreparingTimeMinutes,
                    e.RecipeServings,
                    e.RecipeDescription,
                    e.RecipeExplanation,
                    e.RecipeStatus,
                    CreatedAt = e.CreatedAt != null ? e.CreatedAt.Value.Date.ToShortDateString() : null,

                })

                .ToList()
            });
        }

        public ActionResult SearchByDate(DateTime? minDate, DateTime? maxDate)
         {
        IQueryable<Recipe> query = _context.Recipes;


            if (minDate != null)
        {
            query = query.Where(r => r.CreatedAt >= minDate);
        }
        if (maxDate != null)
        {
            query = query.Where(r => r.CreatedAt <= maxDate);
        }

            return Json(new
            {
                data = query.Include(e => e.RecipeCategory).Select(e => new
                {
                    e.RecipeId,
                    e.RecipeName,
                    e.RecipePrice,
                    CategoryName = e.RecipeCategory != null ? e.RecipeCategory.CategoryName : null, // Check if UserCountry is null
                    e.RecipeCookingTimeMinutes,
                    e.RecipePreparingTimeMinutes,
                    e.RecipeServings,
                    e.RecipeDescription,
                    e.RecipeExplanation,
                    e.RecipeStatus,
                    CreatedAt = e.CreatedAt != null ? e.CreatedAt.Value.Date.ToShortDateString() : null,

                })

                .ToList()
            }); 
        }
        
        public ActionResult GetRecipeById(decimal id)
        {


            return Json(
            
             _context.Recipes.Include(e => e.RecipeCategory).Select(e => new
                {
                    e.RecipeId,
                    e.RecipeName,
                    e.RecipePrice,
                    CategoryId = e.RecipeCategory != null ? e.RecipeCategory.CategoryId :(decimal?) null, // Check if UserCountry is null
                    e.RecipeCookingTimeMinutes,
                    e.RecipePreparingTimeMinutes,
                    e.RecipeServings,
                    e.RecipeDescription,
                    e.RecipeExplanation,
                    e.RecipeStatus,
                    e.RecipeCardImgPath,
                    e.RecipeVideoPath,
                    e.RecipeMainImgPath
                }).FirstOrDefault(e => e.RecipeId == id)

              
            ); ;
        }



        public IActionResult SearchRecipe(string recipeName, int categoryTypeId,int categoryNameId, int chiefId)
        {


            IQueryable<Recipe> query = _context.Recipes.Where(e => e.RecipeStatus == "Accepted");


            if (recipeName != null)
            {
                query = query.Where(r => r.RecipeName!=null && r.RecipeName.ToLower().Contains(recipeName.ToLower()));
            }
            if (categoryNameId > 0)
            {
                query = query.Where(r => r.RecipeCategoryId== categoryNameId);
            }
         
            if (chiefId >0)
            {
                query = query.Where(r => r.ChiefId == chiefId);
            }

          var recipes= query.Include(e => e.RecipeCategory).Include(e => e.Chief).ThenInclude(e => e.User)
               .Select(e => new { 
               
               e.RecipeName,
               e.RecipeDescription,
               UserName= e.Chief.User.UserName,
               e.RecipePrice,
                   e.RecipeId,


               CreatedAt = e.CreatedAt != null ? e.CreatedAt.Value.Date.ToShortDateString() : null,
               e.RecipeCardImgPath
               
               
               });

            return Json(recipes.ToList());
        }

    }
}
