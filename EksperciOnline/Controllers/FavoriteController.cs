using EksperciOnline.Models.Domain;
using EksperciOnline.Models.ViewModels;
using EksperciOnline.Repositiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteServiceRepository favoriteServiceRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IServiceCommentRepository serviceCommentRepository;

        public FavoriteController(
            IFavoriteServiceRepository favoriteServiceRepository,
            IServiceRepository serviceRepository,
            UserManager<IdentityUser> userManager,
            IServiceCommentRepository serviceCommentRepository)
        {
            this.favoriteServiceRepository = favoriteServiceRepository;
            this.serviceRepository = serviceRepository;
            this.userManager = userManager;
            this.serviceCommentRepository = serviceCommentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid serviceId)
        {
            var userId = Guid.Parse(userManager.GetUserId(User));

            var favoriteService = new FavoriteService
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ServiceId = serviceId
            };

            await favoriteServiceRepository.AddAsync(favoriteService);

            return RedirectToAction("List", "Services");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid serviceId)
        {
            var userId = Guid.Parse(userManager.GetUserId(User));

            var success = await favoriteServiceRepository.RemoveAsync(userId, serviceId);

            if (success)
            {
                return RedirectToAction("List", "Services");
            }

            return BadRequest("Nie udało się usunąć usługi z ulubionych.");
        }

        [HttpGet]
        public async Task<IActionResult> MyFavorites()
        {
            var userId = Guid.Parse(userManager.GetUserId(User));
            var favorites = await favoriteServiceRepository.GetByUserIdAsync(userId);

            var favoriteServices = new List<UsługaViewModel>();

            foreach (var favorite in favorites)
            {
                var usługa = await serviceRepository.GetAsync(favorite.ServiceId);

                if (usługa != null)
                {
                    var comments = await serviceCommentRepository.GetCommentsByServiceIdAsync(usługa.Id);
                    double averageGrade = comments.Any() ? comments.Average(c => c.Grade) : 0;
                    int totalComments = comments.Count();

                    favoriteServices.Add(new UsługaViewModel
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
                        Kategoria = usługa.Kategoria,
                        AverageGrade = averageGrade,
                        TotalComments = totalComments
                    });
                }
            }

            return View(favoriteServices);
        }
    }
}
