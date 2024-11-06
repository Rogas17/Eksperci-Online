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
            //Map view model to domain model
            var usługa = new Usługa
            {
                Tytuł = addServicesRequest.Tytuł,
                Lokalizacja = addServicesRequest.Lokalizacja,
                NrTelefonu = addServicesRequest.NrTelefonu,
                CenaOd = addServicesRequest.CenaOd,
                CenaDo = addServicesRequest.CenaDo,
                Opis = addServicesRequest.Opis,
                KrótkiOpis = addServicesRequest.KrótkiOpis,
                Widoczność = addServicesRequest.Widoczność,
                UrlZdjęcia = addServicesRequest.UrlZdjęcia,
                UrlBaneru = addServicesRequest.UrlBaneru,
                DataPulikacji = addServicesRequest.DataPulikacji,
                Autor = addServicesRequest.Autor,
            };

            // Pobieranie i przypisanie wybranej kategorii
            var wybranaKategoriaId = addServicesRequest.WybranaKategoria;
            if (Guid.TryParse(wybranaKategoriaId, out Guid wybranaKategoriaIdAsGuid))
            {
                var existingCategory = await categoryRepository.GetAsync(wybranaKategoriaIdAsGuid);
                if (existingCategory != null)
                {
                    usługa.Kategoria = existingCategory; // Przypisanie kategorii do usługi
                }
            }

            await serviceRepository.AddAsync(usługa);

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
