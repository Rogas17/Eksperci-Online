using EksperciOnline.Models.Domain;
using EksperciOnline.Models.ViewModels;
using EksperciOnline.Repositiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace EksperciOnline.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IServiceCommentRepository serviceCommentRepository;
        private readonly IZgłoszenieRepository zgłoszenieRepository;

        public ServicesController(ICategoryRepository categoryRepository,
            IServiceRepository serviceRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IServiceCommentRepository serviceCommentRepository,
            IZgłoszenieRepository zgłoszenieRepository)
        {
            this.categoryRepository = categoryRepository;
            this.serviceRepository = serviceRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.serviceCommentRepository = serviceCommentRepository;
            this.zgłoszenieRepository = zgłoszenieRepository;
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddServicesRequest addServicesRequest)
        {
            var userId = userManager.GetUserId(User);

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
                Widoczność = true,
                UrlZdjęcia = addServicesRequest.UrlZdjęcia,
                UrlBaneru = addServicesRequest.UrlBaneru,
                DataPulikacji = DateTime.Now,
                AutorId = Guid.Parse(userId)
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
        public async Task<IActionResult> List(string? searchQuery, string? searchLocalQuery, string? sortBy, string? sortDirection, int pageSize = 15, int pageNumber = 1)
        {
            var totalRecords = await serviceRepository.CountAsync();
            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

            if (pageNumber > totalPages)
            {
                pageNumber--;
            }

            if (pageNumber < 1)
            {
                pageNumber++;
            }

            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.SearchLocalQuery = searchLocalQuery;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDirection = sortDirection;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = pageNumber;

            var usługi = await serviceRepository.GetAllAsync(searchQuery, searchLocalQuery, sortBy, sortDirection, pageNumber, pageSize);

            var usługiViewModel = new List<UsługaViewModel>();

            foreach (var usługa in usługi)
            {
                if (usługa.Widoczność)
                {
                    var comments = await serviceCommentRepository.GetCommentsByServiceIdAsync(usługa.Id);
                    double averageGrade = comments.Any() ? comments.Average(c => c.Grade) : 0;
                    int totalComments = comments.Count();

                    // Pobierz nazwę użytkownika autora na podstawie jego ID
                    var autor = await userManager.FindByIdAsync(usługa.AutorId.ToString());
                    var autorNazwaUżytkownika = autor?.UserName ?? "Anonim";

                    usługiViewModel.Add(new UsługaViewModel
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
                        DataPulikacji = usługa.DataPulikacji,
                        Autor = autorNazwaUżytkownika,
                        Kategoria = usługa.Kategoria,
                        AverageGrade = averageGrade,
                        TotalComments = totalComments
                    });
                }
            }

            return View(usługiViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Show(Guid id)
        {
            var usługa = await serviceRepository.GetAsync(id);

            var serviceDetailViewModel = new ServiceDetailViewModel();

            if (usługa != null)
            {
                // Pobierz nazwę użytkownika na podstawie ID autora
                var autor = await userManager.FindByIdAsync(usługa.AutorId.ToString());
                var autorNazwaUżytkownika = autor?.UserName ?? "Anonim";

                // Get total comments
                var totalComments = await serviceCommentRepository.GetTotalComments(usługa.Id);

                // Get comments for service
                var serviceCommentsDomainModel = await serviceCommentRepository.GetCommentsByServiceIdAsync(usługa.Id);

                // Oblicz średnią ocen
                double averageGrade = 0;
                if (serviceCommentsDomainModel.Any())
                {
                    averageGrade = serviceCommentsDomainModel.Average(c => c.Grade);
                }

                var serviceCommentForView = new List<ServiceCommentViewModel>();

                foreach (var serviceComment in serviceCommentsDomainModel)
                {
                    serviceCommentForView.Add(new ServiceCommentViewModel
                    {
                        Description = serviceComment.Description,
                        Grade = serviceComment.Grade,
                        DateAdded = serviceComment.DateAdded,
                        Username = (await userManager.FindByIdAsync(serviceComment.UserId.ToString()))?.UserName ?? "Anonim"
                    });
                }

                serviceDetailViewModel = new ServiceDetailViewModel
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
                    Autor = autorNazwaUżytkownika,
                    Kategoria = usługa.Kategoria,
                    TotalComments = totalComments,
                    Comments = serviceCommentForView,
                    AverageGrade = averageGrade
                };
            }

            return View(serviceDetailViewModel);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Show(ServiceDetailViewModel serviceDetailViewModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                var domainModel = new ServiceComment
                {
                    ServiceId = serviceDetailViewModel.Id,
                    Description = serviceDetailViewModel.CommentDescription,
                    Grade = serviceDetailViewModel.Grade,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DateAdded = DateTime.Now,
                };

                await serviceCommentRepository.AddAsync(domainModel);
                return RedirectToAction("Show", "Services",
                    new { id = serviceDetailViewModel.Id });
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Retrieve the result from the repository
            var usługa = await serviceRepository.GetAsync(id);
            var categoryDomainModel = await categoryRepository.GetAllAsync();

            var userId = userManager.GetUserId(User);

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
                    AutorId = Guid.Parse(userId),
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditServiceRequest editServiceRequest)
        {
            var userId = userManager.GetUserId(User);

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
                Widoczność = true,
                UrlZdjęcia = editServiceRequest.UrlZdjęcia,
                UrlBaneru = editServiceRequest.UrlBaneru,
                DataPulikacji = editServiceRequest.DataPulikacji,
                AutorId = Guid.Parse(userId)
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(EditServiceRequest editServiceRequest)
        {
            // talk to repository to delete this service
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

        [Authorize]
        [HttpGet]
        public IActionResult Report(Guid id)
        {
            var model = new ZgłoszenieViewModel
            {
                UsługaId = id
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Report(ZgłoszenieViewModel zgłoszenieViewModel)
        {
            if (ModelState.IsValid)
            {
                var zgłoszenie = new Zgłoszenie
                {
                    Id = Guid.NewGuid(),
                    UsługaId = zgłoszenieViewModel.UsługaId,
                    Powód = zgłoszenieViewModel.Powód,
                    Opis = zgłoszenieViewModel.Opis,
                    DataZgłoszenia = DateTime.Now,
                    CzyRozpatrzone = false,
                    CzyZablokowane = false
                };

                await zgłoszenieRepository.AddAsync(zgłoszenie);
                return RedirectToAction("List", "Services");
            }

            return View(zgłoszenieViewModel);
        }

    }
}
