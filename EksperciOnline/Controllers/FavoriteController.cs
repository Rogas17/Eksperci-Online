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

        public FavoriteController(
            IFavoriteServiceRepository favoriteServiceRepository,
            IServiceRepository serviceRepository,
            UserManager<IdentityUser> userManager)
        {
            this.favoriteServiceRepository = favoriteServiceRepository;
            this.serviceRepository = serviceRepository;
            this.userManager = userManager;
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
                var service = await serviceRepository.GetAsync(favorite.ServiceId);

                if (service != null)
                {
                    favoriteServices.Add(new UsługaViewModel
                    {
                        Id = service.Id,
                        Tytuł = service.Tytuł,
                        Lokalizacja = service.Lokalizacja,
                        NrTelefonu = service.NrTelefonu,
                        CenaOd = service.CenaOd,
                        CenaDo = service.CenaDo,
                        Opis = service.Opis,
                        KrótkiOpis = service.KrótkiOpis,
                        Widoczność = service.Widoczność,
                        UrlZdjęcia = service.UrlZdjęcia,
                        DataPulikacji = service.DataPulikacji,
                        Kategoria = service.Kategoria
                    });
                }
            }

            return View(favoriteServices);
        }
    }
}
