using System.Security.Claims;
using TripService.Domain.Enum;
using TripService.Domain.Services;

namespace TripService.Domain.Helpers
{
    public class SharePermissionHelper
    {
            public static async Task<bool> HasViewPermission(
                HttpRequest request,
                ClaimsPrincipal user,
                IShareService shareService)
            {
                if (user.Identity?.IsAuthenticated == true)
                    return true;

                var token = request.Headers["x-share-token"].FirstOrDefault();

                if (string.IsNullOrEmpty(token))
                    return false;

                var permission = await shareService.GetPermissionFromToken(token);

                return permission == SharePermission.VIEW
                    || permission == SharePermission.EDIT;
            }

            public static async Task<bool> HasEditPermission(
                HttpRequest request,
                ClaimsPrincipal user,
                IShareService shareService)
            {
                if (user.Identity?.IsAuthenticated == true)
                    return true;

                var token = request.Headers["x-share-token"].FirstOrDefault();

                if (string.IsNullOrEmpty(token))
                    return false;

                var permission = await shareService.GetPermissionFromToken(token);

                return permission == SharePermission.EDIT;
            }
        }
    }
