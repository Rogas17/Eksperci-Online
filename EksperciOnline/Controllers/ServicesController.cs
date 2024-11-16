using EksperciOnline.Data;
using EksperciOnline.Models.Domain;
using EksperciOnline.Models.ViewModels;
using EksperciOnline.Repositiories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace EksperciOnline.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IServiceRepository serviceRepository;

        public ServicesController(ICategoryRepository categoryRepository, IServiceRepository serviceRepository)
        {
            this.categoryRepository = categoryRepository;
            this.serviceRepository = serviceRepository;
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
        public async Task<IActionResult> List()
        {
            var usługa = await serviceRepository.GetAllAsync();

            return View(usługa);
        }

        [HttpGet]
        public async Task<IActionResult> Show(Guid id)
        {
            var usługa = await serviceRepository.GetAsync(id);

            return View(usługa);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Retrieve the result from the repository
            var usługa = await serviceRepository.GetAsync(id);
            var categoryDomainModel = await categoryRepository.GetAllAsync();

            if (usługa != null)
            {
                // map domain model into view model
                var model = new EditServiceRequest
                {
                    Id = usługa.Id,
                    Tytuł = usługa.Tytuł,
                    Lokalizacja = usługa.Lokalizacja,
                    NrTelefonu = usługa.NrTelefonu,
                    CenaOd = usługa.CenaOd,
                    CenaDo = usługa.CenaDo,
                    Opis = usługa.Opis,
                    KrótkiOpis = usługa.KrótkiOpis,
                    Widoczność = usługa.Widoczność,
                    UrlZdjęcia = usługa.UrlZdjęcia,
                    UrlBaneru = usługa.UrlBaneru,
                    DataPulikacji = usługa.DataPulikacji,
                    Autor = usługa.Autor,
                    Kategorie = categoryDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.NazwaKategorii,
                        Value = x.Id.ToString()
                    }),
                    WybranaKategoria = usługa.Kategoria.Id.ToString()
                };

                return View(model);
            }

            // Pass data to view

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditServiceRequest editServiceRequest)
        {
            // map view model back to domain model
            var serviceDomainModel = new Usługa
            {
                Id = editServiceRequest.Id,
                Tytuł = editServiceRequest.Tytuł,
                Lokalizacja = editServiceRequest.Lokalizacja,
                NrTelefonu = editServiceRequest.NrTelefonu,
                CenaOd = editServiceRequest.CenaOd,
                CenaDo = editServiceRequest.CenaDo,
                Opis = editServiceRequest.Opis,
                KrótkiOpis = editServiceRequest.KrótkiOpis,
                Widoczność = editServiceRequest.Widoczność,
                UrlZdjęcia = editServiceRequest.UrlZdjęcia,
                UrlBaneru = editServiceRequest.UrlBaneru,
                DataPulikacji = editServiceRequest.DataPulikacji,
                Autor = editServiceRequest.Autor,
            };

            // map category into domain model
            var selectedCategory = new Kategoria();
            var wybranaKategoria = editServiceRequest.WybranaKategoria;
            {
                if (Guid.TryParse(wybranaKategoria, out var kategoria))
                {
                    var foundCategory = await categoryRepository.GetAsync(kategoria);

                    if (foundCategory != null)
                    {
                        serviceDomainModel.Kategoria = foundCategory;
                    }
                }
            }

            // submit information to repository to update
            var updatedService = await serviceRepository.UpdateAsync(serviceDomainModel);

            if (updatedService != null)
            {
                // Show success notification
                return RedirectToAction("Edit");
            }

            // Show error notification
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditServiceRequest editServiceRequest)
        {
            // talk to repository to delte this service
            var deletedService = await serviceRepository.DeleteAsync(editServiceRequest.Id);

            if (deletedService != null)
            {
                // Show success notification
                return RedirectToAction("List");
            }
            //Show error notification
            return RedirectToAction("Edit", new { id = editServiceRequest.Id });

            // display the response
        }
    }
}
