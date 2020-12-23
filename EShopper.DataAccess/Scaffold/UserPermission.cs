using System;
using System.Collections.Generic;

#nullable disable

namespace EShopper.DataAccess.Scaffold
{
    public partial class UserPermission
    {
        public int PermissionId { get; set; }
        public string UserId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
