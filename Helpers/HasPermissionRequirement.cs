using Microsoft.AspNetCore.Authorization;

namespace sistem_pengelolaan_lab.Helpers
{
    public class HasPermissionRequirement(string permission) : IAuthorizationRequirement
    {
        public string Permission { get; } = permission;
    }
}
