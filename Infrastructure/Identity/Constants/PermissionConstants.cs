using System.Collections.ObjectModel;

namespace Infrastructure.Identity.Constants
{
    public static class OBNTenantAction
    {
        public const string View = nameof(View);
        public const string Create = nameof(Create);
        public const string Update = nameof(Update);
        public const string Delete = nameof(Delete);
        public const string UpgradeSubscription = nameof(UpgradeSubscription);
    }
    public static class OBNTenantFeature
    {
        public const string Tenants = nameof(Tenants);
        public const string Users = nameof(Users);
        public const string UserRoles = nameof(UserRoles);
        public const string Roles = nameof(Roles);
        public const string RoleClaims = nameof(RoleClaims);
        public const string OBNTenants = nameof(OBNTenants);
    }

    public record OBNTenantPermission(string Description, string Action, string Feature, bool IsBasic = false, bool IsRoot = false)
    {
        public string Name => NameFor(Action, Feature);
        public static string NameFor(string action, string feature) => $"Permission.{feature}.{action}";
    }

    public static class OBNTenantPermissions
    {
        private static readonly OBNTenantPermission[] _allPermissions = 
            [
                new OBNTenantPermission("View Users", OBNTenantAction.View, OBNTenantFeature.Users),
                new OBNTenantPermission("Create Users", OBNTenantAction.Create, OBNTenantFeature.Users),
                new OBNTenantPermission("Update Users", OBNTenantAction.Update, OBNTenantFeature.Users),                
                new OBNTenantPermission("Delete Users", OBNTenantAction.Delete, OBNTenantFeature.Users),

                new OBNTenantPermission("View User Roles", OBNTenantAction.View, OBNTenantFeature.UserRoles),
                new OBNTenantPermission("Update User Roles", OBNTenantAction.Update, OBNTenantFeature.UserRoles),

                new OBNTenantPermission("View Roles", OBNTenantAction.View, OBNTenantFeature.Roles),
                new OBNTenantPermission("Create Roles", OBNTenantAction.Create, OBNTenantFeature.Roles),
                new OBNTenantPermission("Update Roles", OBNTenantAction.Update, OBNTenantFeature.Roles),
                new OBNTenantPermission("Delete Roles", OBNTenantAction.Delete, OBNTenantFeature.Roles),

                new OBNTenantPermission("View Role Claims/Permissions", OBNTenantAction.View, OBNTenantFeature.RoleClaims),
                new OBNTenantPermission("Update Role Claims/Permissions", OBNTenantAction.Update, OBNTenantFeature.RoleClaims),

                new OBNTenantPermission("View OBNTenants", OBNTenantAction.View, OBNTenantFeature.OBNTenants, IsBasic: true),
                new OBNTenantPermission("Create OBNTenants", OBNTenantAction.Create, OBNTenantFeature.OBNTenants),
                new OBNTenantPermission("Update OBNTenants", OBNTenantAction.Update, OBNTenantFeature.OBNTenants),
                new OBNTenantPermission("Delete OBNTenants", OBNTenantAction.Delete, OBNTenantFeature.OBNTenants),

                new OBNTenantPermission("View Tenants", OBNTenantAction.View, OBNTenantFeature.Tenants,IsRoot: true),
                new OBNTenantPermission("Create Tenants", OBNTenantAction.Create, OBNTenantFeature.Tenants,IsRoot: true),
                new OBNTenantPermission("Update Tenants", OBNTenantAction.Update, OBNTenantFeature.Tenants,IsRoot: true),
                new OBNTenantPermission("Upgrade Tenants Subscription", OBNTenantAction.UpgradeSubscription, OBNTenantFeature.Tenants, IsRoot: true)
            ];
        
        public static IReadOnlyList<OBNTenantPermission> All { get; } = new ReadOnlyCollection<OBNTenantPermission>(_allPermissions);
        public static IReadOnlyList<OBNTenantPermission> Root { get; } = new ReadOnlyCollection<OBNTenantPermission>(_allPermissions.Where(p => p.IsRoot).ToArray());
        public static IReadOnlyList<OBNTenantPermission> Admin { get; } = new ReadOnlyCollection<OBNTenantPermission>(_allPermissions.Where(p => !p.IsRoot).ToArray());
        public static IReadOnlyList<OBNTenantPermission> Basic { get; } = new ReadOnlyCollection<OBNTenantPermission>(_allPermissions.Where(p => p.IsBasic).ToArray());
    }

}
