using System.Security.Claims;

namespace ArtGallery.Extensions
{
    public static class IdentityExtension
    {
        public static long GetUserIdFromClaim(this ClaimsPrincipal claimsPrincipal)
        {
            var subClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            return subClaim != null ? long.Parse(subClaim.Value) : 0;
        }
    }
}
