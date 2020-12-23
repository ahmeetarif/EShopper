using System;
using System.Collections.Generic;

#nullable disable

namespace EShopper.DataAccess.Scaffold
{
    public partial class RefreshToken
    {
        public string UserId { get; set; }
        public string JwtId { get; set; }
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
