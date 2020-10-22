﻿using System;

namespace EShopper.Business.Options
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan TokenLifetime { get; set; }
    }
}