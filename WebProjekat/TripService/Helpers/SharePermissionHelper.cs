using System.Security.Claims;
using Contract.Enums;
using Contract.Services;
using Microsoft.AspNetCore.Http;


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

                if (permission == null) return false;

                return permission.Permission == SharePermission.VIEW.ToString()
                    || permission.Permission == SharePermission.EDIT.ToString();
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

                if (permission == null)
                    return false;

                return permission.Permission == "EDIT";
            }
        }
    }
