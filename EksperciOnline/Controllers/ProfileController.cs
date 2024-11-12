using EksperciOnline.Repositiories;
using Microsoft.AspNetCore.Mvc;
using EksperciOnline.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using EksperciOnline.Models.Domain;

namespace EksperciOnline.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IServiceRepository serviceRepository;

        public ProfileController(ICategoryRepository categoryRepository, IServiceRepository serviceRepository)
        {
            this.categoryRepository = categoryRepository;
            this.serviceRepository = serviceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var usługi = await serviceRepository.GetAllAsync();

            return View(usługi);
        }

        

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
