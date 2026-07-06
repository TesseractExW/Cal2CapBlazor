using System.Security.Claims;
using Cal2CapBlazor.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Cal2CapBlazor.Infrastructure.Authentications;
public class ServerUserContext(IHttpContextAccessor httpContextAccessor)
    : IUserContext
{
    public Guid Id 
        => Guid.TryParse(httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id) ? id : Guid.Empty;
    public bool IsAuthenticated 
        => httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}