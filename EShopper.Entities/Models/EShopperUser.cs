using System.Collections.Generic;
using EShopper.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace EShopper.Entities.Models
{
    public class EShopperUser : IdentityUser, IEntity
    {
        public EShopperUser()
        {
            UsersAddress = new HashSet<UsersAddress>();
            Categories = new HashSet<Category>();
        }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual SellerUsers SellerUsers { get; set; }
        public virtual UserDetails UserDetails { get; set; }
        public virtual RefreshTokens RefreshTokens { get; set; }
        public virtual ICollection<UsersAddress> UsersAddress { get; set; }
    }
}