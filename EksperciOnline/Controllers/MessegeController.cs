using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    [Authorize]
    public class MessegeController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
