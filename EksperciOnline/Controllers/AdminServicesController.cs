using EksperciOnline.Repositiories;
using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    public class AdminServicesController : Controller
    {
        private readonly IServiceRepository serviceRepository;

        public AdminServicesController(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }
        public async Task<IActionResult> List()
        {
            // Call the repository
            var blogPosts = await serviceRepository.GetAllAsync();

            return View(blogPosts);
        }
    }
}
