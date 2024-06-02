using MedicApp.DTO;
using MedicApp.Services;
using Microsoft.AspNetCore.Mvc;
using MedicApp.Models;
using Error = MedicApp.Models.Error;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace MedicApp.Controllers
{
    public class UserController : Controller
    {
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IApplicationService _applicationService;

        public UserController(IApplicationService applicationService) : base()
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSignupDTO userSignupDTO)
        {
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState.Values)
                {
                    foreach (var error in entry.Errors)
                    {
                        ErrorArray!.Add(new Error("", error.ErrorMessage, ""));
                    }
                }
                ViewData["Error Array"] = ErrorArray;
                return View();
            }

            try
            {
                await _applicationService.UserService!.SignUpUserAsync(userSignupDTO);
                return RedirectToAction("Login", "User");
        
            }
            catch (Exception e)
            {
                ErrorArray!.Add(new Error("", e.Message, ""));
                ViewData["ErrorArray"] = ErrorArray;
                return View();
            }
        }


        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal principal = HttpContext.User;
            if (principal.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(); 
        
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLoginDTO credentials)
        {
            var user = await _applicationService!.UserService!.VerifyAndGetUserAsync(credentials);
            if (user == null)
            {
                ViewData["ValidateMessage"] = "Error: User not found ";
                return View();
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, credentials.Username!),
                new Claim(ClaimTypes.Role, user.UserRole!.ToString()!)
            };

            ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new()
            {
                AllowRefresh = true,
                IsPersistent = credentials.KeepLoggedIn
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), properties);

            if (user.UserRole == UserRole.Doctor)
            {
                return RedirectToAction("Index", "Doctor");
            }
            else if (user.UserRole == UserRole.Patient);
            {
                return RedirectToAction("Index", "Patient");
            }
           
        }
    }
}
