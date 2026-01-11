using DoaFacil.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DoaFacil.Infrastructure.Auth;

public class CurrentUser(IHttpContextAccessor _http) : ICurrentUser
{
    public bool IsAuthenticated
        => _http.HttpContext?.User?.Identity?.IsAuthenticated == true;

    public string? UserId
        => _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? Email
        => _http.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
}
