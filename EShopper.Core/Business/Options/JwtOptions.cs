using System;

namespace EShopper.Core.Business.Options
{
    public partial class JwtOptions
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
    }
}