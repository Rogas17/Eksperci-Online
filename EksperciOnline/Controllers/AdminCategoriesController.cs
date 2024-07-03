using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    public class AdminCategoriesController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
    }
}
