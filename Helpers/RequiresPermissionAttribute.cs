using Microsoft.AspNetCore.Authorization;

namespace astratech_apps_backend.Helpers
{
    public class RequiresPermissionAttribute : AuthorizeAttribute
    {
        public string Permission { get; }

        public RequiresPermissionAttribute(string permission)
        {
            Permission = permission;
            Policy = "HasPermission";
        }
    }
}
