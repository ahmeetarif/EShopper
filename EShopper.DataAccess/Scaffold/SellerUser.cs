using System;
using System.Collections.Generic;

#nullable disable

namespace EShopper.DataAccess.Scaffold
{
    public partial class SellerUser
    {
        public SellerUser()
        {
            Products = new HashSet<Product>();
        }

        public string UserId { get; set; }
        public string StoreName { get; set; }
        public long Score { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual AspNetUser User { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
