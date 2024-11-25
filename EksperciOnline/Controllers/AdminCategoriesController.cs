using EksperciOnline.Data;
using EksperciOnline.Models.Domain;
using EksperciOnline.Models.ViewModels;
using EksperciOnline.Repositiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EksperciOnline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public AdminCategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddCategoryRequest addCategoryRequest)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            //Mapping AddCategoryRequest to Category domain model
            var kategoria = new Kategoria
            {
                NazwaKategorii = addCategoryRequest.NazwaKategorii,
                UrlZdjęcia = addCategoryRequest.UrlZdjęcia
            };

            await categoryRepository.AddAsync(kategoria);

            return RedirectToAction("List");
        }


        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            // use dbContext to read the categories
            var kategorie = await categoryRepository.GetAllAsync();


            return View(kategorie);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var kategoria = await categoryRepository.GetAsync(id);

            if (kategoria != null)
            {
                var editCategoryRequest = new EditCategoryRequest
                {
                    Id = kategoria.Id,
                    NazwaKategorii = kategoria.NazwaKategorii,
                    UrlZdjęcia = kategoria.UrlZdjęcia
                };
                return View(editCategoryRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryRequest editCategoryRequest)
        {
            var kategoria = new Kategoria
            {
                Id = editCategoryRequest.Id,
                NazwaKategorii = editCategoryRequest.NazwaKategorii,
                UrlZdjęcia = editCategoryRequest.UrlZdjęcia
            };

            var updatedCategory = await categoryRepository.UpdateAsync(kategoria);

            if (updatedCategory != null)
            {
                // Show success notification
            }
            else
            {
                // Show error notification
            }

            return RedirectToAction("Edit", new { id = editCategoryRequest.Id });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(EditCategoryRequest editCategoryRequest) 
        {
            var deletedCategory = await categoryRepository.DeleteAsync(editCategoryRequest.Id);

            if (deletedCategory != null)
            {
                //Show success notification
                return RedirectToAction("List");
            }

            // Show an error notification
            return RedirectToAction("Edit", new { id = editCategoryRequest.Id });
        }

       
    }
}
