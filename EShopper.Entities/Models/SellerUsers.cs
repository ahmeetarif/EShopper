using EShopper.Core.Entities;
using System;
using System.Collections.Generic;

namespace EShopper.Entities.Models
{
    public partial class SellerUsers : IEntity
    {
        public SellerUsers()
        {
            Product = new HashSet<Product>();
        }

        public string UserId { get; set; }
        public string StoreName { get; set; }
        public long Score { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual EShopperUser User { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}