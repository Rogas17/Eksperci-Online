using EksperciOnline.Models.ViewModels;
using EksperciOnline.Repositiories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly UserManager<IdentityUser> userManager;

        public ProfileController(ICategoryRepository categoryRepository,
            IServiceRepository serviceRepository,
            UserManager<IdentityUser> userManager
            )
        {
            this.categoryRepository = categoryRepository;
            this.serviceRepository = serviceRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var usługi = await serviceRepository.GetAllAsync();

            return View(usługi);
        }

        
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
                PhoneNumber = registerViewModel.PhoneNumber
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (identityResult.Succeeded)
            {
                // assign this user the "User" role
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                if (roleIdentityResult.Succeeded)
                { 
                    //Show success notification
                    return RedirectToAction("Register");
                }
            }
            //Show error notification
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
    }
}
