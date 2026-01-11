using DoaFacil.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DoaFacil.Infrastructure.Auth;

public class CurrentUser(IHttpContextAccessor http) : ICurrentUser
{
    public bool IsAuthenticated
        => http.HttpContext?.User?.Identity?.IsAuthenticated == true;

    public string? UserId
        => http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? Email
        => http.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
}
