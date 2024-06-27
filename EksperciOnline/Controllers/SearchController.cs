using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
