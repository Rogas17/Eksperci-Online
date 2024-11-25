using EksperciOnline.Models.ViewModels;
using EksperciOnline.Repositiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EksperciOnline.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IServiceCommentRepository serviceCommentRepository;

        public AccountController(ICategoryRepository categoryRepository,
            IServiceRepository serviceRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IServiceCommentRepository serviceCommentRepository
            )
        {
            this.categoryRepository = categoryRepository;
            this.serviceRepository = serviceRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.serviceCommentRepository = serviceCommentRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var usługi = await serviceRepository.GetAllAsync();

            var usługiViewModel = new List<UsługaViewModel>();

            foreach (var usługa in usługi)
            {
                var comments = await serviceCommentRepository.GetCommentsByServiceIdAsync(usługa.Id);
                double averageGrade = comments.Any() ? comments.Average(c => c.Grade) : 0;
                int totalComments = comments.Count();

                usługiViewModel.Add(new UsługaViewModel
                {
                    Id = usługa.Id,
                    Tytuł = usługa.Tytuł,
                    Lokalizacja = usługa.Lokalizacja,
                    NrTelefonu = usługa.NrTelefonu,
                    CenaOd = usługa.CenaOd,
                    CenaDo = usługa.CenaDo,
                    Opis = usługa.Opis,
                    KrótkiOpis = usługa.KrótkiOpis,
                    Widoczność = usługa.Widoczność,
                    UrlZdjęcia = usługa.UrlZdjęcia,
                    DataPulikacji = usługa.DataPulikacji,
                    Autor = usługa.Autor,
                    Kategoria = usługa.Kategoria,
                    AverageGrade = averageGrade,
                    TotalComments = totalComments
                });
            }

            return View(usługiViewModel);
        }

        [Authorize]
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
            if (ModelState.IsValid)
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
            }

            //Show error notification
            return View();

        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            // Show errors
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
