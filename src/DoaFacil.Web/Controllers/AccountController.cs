using DoaFacil.Application.DTOs;
using DoaFacil.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoaFacil.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> Login(SignInRequest model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _authService.SignInAsync(model, HttpContext.RequestAborted);

            if (!WasSucceeded(result)) return View(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet, AllowAnonymous]
        public IActionResult AccessDenied() => View();

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync(HttpContext.RequestAborted);
            return RedirectToAction("Login", "Account");
        }
    }
}
