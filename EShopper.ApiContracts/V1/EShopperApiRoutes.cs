namespace EShopper.ApiContracts.V1
{
    public static class EShopperApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Authentication
        {
            public const string AuthBase = Base + "/auth";
        }
    }
}