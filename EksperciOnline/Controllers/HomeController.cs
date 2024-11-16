using EksperciOnline.Models;
using EksperciOnline.Repositiories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EksperciOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository categoryRepository;

        public HomeController(ILogger<HomeController> logger,
            ICategoryRepository categoryRepository
            )
        {
            _logger = logger;
            this.categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            // get all categories
            var kategorie = await categoryRepository.GetAllAsync();

            return View(kategorie);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
