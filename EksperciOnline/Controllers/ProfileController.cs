using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
