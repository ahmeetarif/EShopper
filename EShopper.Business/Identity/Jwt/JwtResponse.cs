﻿namespace EShopper.Business.Identity.Jwt
{
    public class JwtResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}