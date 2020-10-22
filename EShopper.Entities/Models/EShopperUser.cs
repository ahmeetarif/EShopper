using EShopper.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EShopper.Entities.Models
{
    public class EShopperUser : IdentityUser, IEntity
    {
        public EShopperUser()
        {
            UsersAddress = new HashSet<UsersAddress>();
        }
        public virtual SellerUsers SellerUsers { get; set; }
        public virtual UsersDetail UsersDetail { get; set; }
        public virtual ICollection<UsersAddress> UsersAddress { get; set; }
    }
}