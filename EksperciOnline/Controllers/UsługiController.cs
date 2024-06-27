using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    public class UsługiController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Show()
        {
            return View();
        }
    }
}
