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


        public async Task<IActionResult> List(string? searchQuery)
        {
            ViewBag.SearchQuery = searchQuery;

            // Call the repository
            var blogPosts = await serviceRepository.GetAllAsync(searchQuery);

            return View(blogPosts);
        }
    }
}
