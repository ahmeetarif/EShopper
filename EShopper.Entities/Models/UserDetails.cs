using EShopper.Core.Entities;
using System;

namespace EShopper.Entities.Models
{
    public partial class UserDetails : IEntity
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual EShopperUser User { get; set; }
    }
}