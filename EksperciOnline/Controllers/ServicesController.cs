using EksperciOnline.Data;
using Microsoft.AspNetCore.Mvc;


namespace EksperciOnline.Controllers
{
    public class ServicesController : Controller
    {
        

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Show()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }
    }
}
