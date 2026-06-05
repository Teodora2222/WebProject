using Gateway.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Gateway.Helpers
{
    public static class SharePermissionHelper
    {
        public static async Task<bool> HasViewPermission(
            HttpRequest request,
            ClaimsPrincipal user,
            ITravelGatewayService travelService)
        {
            if (user.Identity?.IsAuthenticated == true)
                return true;

            var token =
                request.Headers["x-share-token"]
                    .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(token))
                return false;

            var share =
                await travelService.GetPermissionFromTokenAsync(token);

            if (share == null)
                return false;

            return share.Permission == "VIEW"
                || share.Permission == "EDIT";
        }

        public static async Task<bool> HasEditPermission(
            HttpRequest request,
            ClaimsPrincipal user,
            ITravelGatewayService travelService)
        {
            if (user.Identity?.IsAuthenticated == true)
                return true;

            var token =
                request.Headers["x-share-token"]
                    .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(token))
                return false;

            var share =
                await travelService.GetPermissionFromTokenAsync(token);

            if (share == null)
                return false;

            return share.Permission == "EDIT";
        }
    }
}