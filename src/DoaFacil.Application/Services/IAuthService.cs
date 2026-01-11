using DoaFacil.Application.DTOs;

namespace DoaFacil.Application.Services;

public interface IAuthService
{
    Task<Result> RegisterAsync(RegisterRequest request, CancellationToken ct);
    Task<Result> SignInAsync(SignInRequest request, CancellationToken ct);
    Task SignOutAsync(CancellationToken ct);
}
