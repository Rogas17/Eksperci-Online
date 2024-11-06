using EksperciOnline.Data;
using Microsoft.AspNetCore.Mvc;


namespace EksperciOnline.Controllers
{
    public class ServicesController : Controller
    {
        

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Retrieve the result from the repository


            // Pass data to view

            return View();
        }
    }
}
