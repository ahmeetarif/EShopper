using EShopper.Core.Entities;

namespace EShopper.Entities.Models
{
    public class UserPermissions : IEntity
    {
        public int PermissionId { get; set; }
        public string UserId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual EShopperUser User { get; set; }
    }
}