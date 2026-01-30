using Microsoft.AspNetCore.Authorization;

namespace IdentityService.Authorization
{
    public class RequirePermissionAttribute : AuthorizeAttribute
    {
        public RequirePermissionAttribute(string permission)
        {
            Policy = $"Permission:{permission}";
        }
    }
}
