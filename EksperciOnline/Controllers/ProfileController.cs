using EksperciOnline.Repositiories;
using Microsoft.AspNetCore.Mvc;
using EksperciOnline.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EksperciOnline.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public ProfileController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var kategorie = await categoryRepository.GetAllAsync();

            var model = new AddServicesRequest
            {
                Kategorie = kategorie.Select(x => new SelectListItem { Text = x.NazwaKategorii, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddServicesRequest addServicesRequest)
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
