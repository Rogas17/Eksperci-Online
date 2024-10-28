using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    public class FavoriteController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
