using DoaFacil.Application.DTOs;
using DoaFacil.Application.Services;
using Microsoft.AspNetCore.Identity;

namespace DoaFacil.Infrastructure.Auth;

public class IdentityService(UserManager<ApplicationUser> _userManager,
                       SignInManager<ApplicationUser> _signInManager) : IAuthService
{
    public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken ct)
    {
        if (request.Password != request.ConfirmPassword)
            return Result.Failure("As senhas não conferem.");

        var user = new ApplicationUser
        {
            UserName = request.Name,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Result.Failure(result.Errors.Select(e => e.Description));

        return Result.Success();
    }

    public async Task<Result> SignInAsync(SignInRequest request, CancellationToken ct)
    {
        var result = await _signInManager.PasswordSignInAsync(
           request.Email,
           request.Password,
           request.RememberMe,
           lockoutOnFailure: false);

        if (!result.Succeeded)
            return Result.Failure("Credenciais inválidas.");

        return Result.Success();
    }

    public async Task SignOutAsync(CancellationToken ct)
    {
        await _signInManager.SignOutAsync();
    }
}
