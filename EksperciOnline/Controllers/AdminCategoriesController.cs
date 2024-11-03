using EksperciOnline.Data;
using EksperciOnline.Models.Domain;
using EksperciOnline.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    public class AdminCategoriesController : Controller
    {
        private readonly EksperciOnlineDbContext eksperciOnlineDbContext;

        public AdminCategoriesController(EksperciOnlineDbContext eksperciOnlineDbContext)
        {
            this.eksperciOnlineDbContext = eksperciOnlineDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddCategoryRequest addCategoryRequest)
        {
            //Mapping AddCategoryRequest to Category domain model
            var kategoria = new Kategoria
            {
                NazwaKategorii = addCategoryRequest.NazwaKategorii,
                UrlZdjęcia = addCategoryRequest.UrlZdjęcia
            };

            eksperciOnlineDbContext.Kategorie.Add(kategoria);
            eksperciOnlineDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            // use dbContext to read the categories
            var kategorie = eksperciOnlineDbContext.Kategorie.ToList();


            return View(kategorie);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var kategoria = eksperciOnlineDbContext.Kategorie.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Edit(EditCategoryRequest editCategoryRequest)
        {
            var kategoria = new Kategoria
            {
                Id = editCategoryRequest.Id,
                NazwaKategorii = editCategoryRequest.NazwaKategorii,
                UrlZdjęcia = editCategoryRequest.UrlZdjęcia
            };

            var existingCategory = eksperciOnlineDbContext.Kategorie.Find(kategoria.Id);

            if (existingCategory != null)
            {
                existingCategory.NazwaKategorii = kategoria.NazwaKategorii;
                existingCategory.UrlZdjęcia = kategoria.UrlZdjęcia;

                eksperciOnlineDbContext.SaveChanges();

                // Show success notification
                return RedirectToAction("List");
            }
            // Show error notification
            return RedirectToAction("Edit", new { id = editCategoryRequest.Id });
        }

        [HttpPost]
        public IActionResult Delete(EditCategoryRequest editCategoryRequest) 
        {
            var kategoria = eksperciOnlineDbContext.Kategorie.Find(editCategoryRequest.Id);

            if (kategoria != null) 
            {
                eksperciOnlineDbContext.Kategorie.Remove(kategoria);
                eksperciOnlineDbContext.SaveChanges();

                // Show success notification
                return RedirectToAction("List");
            }

            // Show an error notification
            return RedirectToAction("Edit", new { id = editCategoryRequest.Id });
        }
    }
}
