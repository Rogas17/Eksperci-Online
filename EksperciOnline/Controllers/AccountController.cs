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
        public async Task<IActionResult> List(string? searchQuery, string? searchLocalQuery, string? sortBy, string? sortDirection, int pageSize = 15, int pageNumber = 1)
        {
            var totalRecords = await serviceRepository.CountAsync();
            var totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

            if (pageNumber > totalPages)
            {
                pageNumber--;
            }

            if (pageNumber < 1)
            {
                pageNumber++;
            }

            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = pageNumber;

            var userId = userManager.GetUserId(User); // Pobranie ID zalogowanego użytkownika
            if (string.IsNullOrEmpty(userId))
            {
                // Jeżeli użytkownik nie jest zalogowany, możesz przekierować go na stronę logowania lub wyświetlić komunikat
                return RedirectToAction("Login", "Account");
            }

            var usługi = await serviceRepository.GetAllAsync(searchQuery, searchLocalQuery, sortBy, sortDirection, pageNumber, pageSize);

            var usługiViewModel = new List<UsługaViewModel>();

            foreach (var usługa in usługi)
            {
                // Sprawdzenie, czy ID autora zgadza się z ID zalogowanego użytkownika
                if (usługa.AutorId.ToString() == userId)
                {
                    var comments = await serviceCommentRepository.GetCommentsByServiceIdAsync(usługa.Id);
                    double averageGrade = comments.Any() ? comments.Average(c => c.Grade) : 0;
                    int totalComments = comments.Count();

                    // Pobranie nazwy użytkownika autora na podstawie jego ID
                    var autor = await userManager.FindByIdAsync(usługa.AutorId.ToString());
                    var autorNazwaUżytkownika = autor?.UserName ?? "Anonim";

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
                        AutorId = Guid.Parse(userId),
                        Autor = autorNazwaUżytkownika,
                        Kategoria = usługa.Kategoria,
                        AverageGrade = averageGrade,
                        TotalComments = totalComments
                    });
                }
            }

            return View(usługiViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new EditAccountViewModel
            {
                Username = user.UserName,
                Email = user.Email
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditAccountViewModel editAccountViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editAccountViewModel);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // Aktualizacja danych użytkownika
            user.UserName = editAccountViewModel.Username;
            user.Email = editAccountViewModel.Email;

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profil został zaktualizowany.";
                return RedirectToAction("Index", "Home");
            }

            // Obsługa błędów
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(editAccountViewModel);
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
                // Sprawdź, czy nazwa użytkownika już istnieje
                var existingUser = await userManager.FindByNameAsync(registerViewModel.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Nazwa użytkownika jest już zajęta.");
                    return View(registerViewModel);
                }

                // Sprawdź, czy e-mail już istnieje
                var existingEmail = await userManager.FindByEmailAsync(registerViewModel.Email);
                if (existingEmail != null)
                {
                    ModelState.AddModelError("Email", "Ten adres e-mail jest już używany.");
                    return View(registerViewModel);
                }

                // Tworzenie nowego użytkownika
                var identityUser = new IdentityUser
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email,
                };

                var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

                if (identityResult.Succeeded)
                {
                    var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");
                    if (roleIdentityResult.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                }

                // Dodaj błędy z IdentityResult do ModelState
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerViewModel);

        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = "/")
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
                return View(loginViewModel);
            }

            // Sprawdź, czy użytkownik istnieje
            var user = await userManager.FindByNameAsync(loginViewModel.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Niepoprawna nazwa użytkownika lub hasło.");
                return View(loginViewModel);
            }

            // Próba zalogowania
            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

            if (signInResult.Succeeded)
            {
                // Jeśli ReturnUrl jest ustawione, przekieruj użytkownika
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            // Jeżeli hasło jest niepoprawne
            ModelState.AddModelError(string.Empty, "Niepoprawna nazwa użytkownika lub hasło.");
            return View(loginViewModel);
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
