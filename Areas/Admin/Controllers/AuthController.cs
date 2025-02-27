using LSDCS.Entity.DTOs.User;
using LSDCS.Entity.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LSDCS.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [AllowAnonymous]
        [HttpGet]  // Login sayfasını göstermek için GET metodunu kullanıyoruz.
        public IActionResult Login()
        {
            // Kullanıcının zaten oturum açıp açmadığını kontrol et
            if (User.Identity.IsAuthenticated)
            {
                // Kullanıcı oturum açmışsa, rolüne bağlı olarak uygun sayfaya yönlendir
                var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                ViewBag.UserRole = userRole;
                if (userRole == "Admin")
                {
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
                else if (userRole == "User")
                {
                    return RedirectToAction("Index", "Home", new { Area = "User" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            // Kullanıcı oturum açmamışsa, login sayfasını göster
            return View();
        }


        [AllowAnonymous] // Giriş işlemi gerçekleştirebilmek için sayfa erişimini sınırsız hale getiriyoruz.
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        var userRole = await _userManager.GetRolesAsync(user);

                        if (userRole.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Home", new { Area = "Admin" });


                        }
                        else if (userRole.Contains("User"))
                        {
                            return RedirectToAction("Index", "Home", new { Area = "User" });

                        }
                        else
                        {
                            // Varsayılan olarak ana sayfaya yönlendir
                            return RedirectToAction("Index", "Home");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "E-mail adresiniz veyta şifreniz yanlıştır.");
                        return View();
                    }
                }
                else
                {

                    ModelState.AddModelError("", "E-mail adresiniz veyta şifreniz yanlıştır.");
                    return View();
                }

            }
            else
            {
                return View();
            }


        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            // Kullanıcıyı başka bir sayfaya yönlendir
            return RedirectToAction("Index", "Home", new { Area = "" });


        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
          

            // Kullanıcıyı başka bir sayfaya yönlendir
            return View();


        }
    }
}
