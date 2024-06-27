using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    public class AdminKategorieController : Controller
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
