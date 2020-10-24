using EShopper.Core.Entities;
using System;

namespace EShopper.Entities.Models
{
    public partial class RefreshTokens : IEntity
    {
        public string UserId { get; set; }
        public string JwtId { get; set; }
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }

        public virtual EShopperUser User { get; set; }
    }
}