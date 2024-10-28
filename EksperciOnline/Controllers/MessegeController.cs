using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    public class MessegeController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
