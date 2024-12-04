using EksperciOnline.Repositiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminServicesController : Controller
    {
        private readonly IServiceRepository serviceRepository;
        private readonly IZgłoszenieRepository zgłoszenieRepository;

        public AdminServicesController(IServiceRepository serviceRepository, IZgłoszenieRepository zgłoszenieRepository)
        {
            this.serviceRepository = serviceRepository;
            this.zgłoszenieRepository = zgłoszenieRepository;
        }


        public async Task<IActionResult> List(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 15, int pageNumber = 1)
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
            ViewBag.SortBy = sortBy;
            ViewBag.SortDirection = sortDirection;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = pageNumber;

            // Call the repository
            var blogPosts = await serviceRepository.GetAllAsync(searchQuery, sortBy, sortDirection, pageNumber, pageSize);

            return View(blogPosts);
        }

        public async Task<IActionResult> Reports()
        {
            var zgłoszenia = await zgłoszenieRepository.GetAllAsync();
            return View(zgłoszenia);
        }

        [HttpPost]
        public async Task<IActionResult> BlockService(Guid id)
        {
            var zgłoszenie = await zgłoszenieRepository.GetAsync(id);
            if (zgłoszenie != null)
            {
                zgłoszenie.CzyRozpatrzone = true;
                zgłoszenie.CzyZablokowane = true;

                // Zablokowanie usługi
                var usługa = await serviceRepository.GetAsync(zgłoszenie.UsługaId);
                if (usługa != null)
                {
                    usługa.Widoczność = false;
                    await serviceRepository.UpdateAsync(usługa);
                }

                await zgłoszenieRepository.UpdateAsync(zgłoszenie);
            }

            return RedirectToAction("Reports");
        }

    }
}
