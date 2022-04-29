using System.Security.Claims;
using HeyTaxi.VehicleService.WebApi.Exceptions;

namespace HeyTaxi.VehicleService.WebApi.Extensions;

public static class GetUserIdExtension 
{
    public static string GetUserId(this HttpContext context)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            throw new UserIdentifierNotFoundException();
        }

        return userId!;
    }
}