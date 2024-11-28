using EksperciOnline.Repositiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminServicesController : Controller
    {
        private readonly IServiceRepository serviceRepository;

        public AdminServicesController(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }


        public async Task<IActionResult> List(string? searchQuery, string? sortBy, string? sortDirection, int pageSize = 2, int pageNumber = 1)
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
    }
}
