using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
