namespace EShopper.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Authentication
        {
            public const string BaseAuthentication = Base + "/auth";
            public const string Register = BaseAuthentication + "/register";
            public const string Login = BaseAuthentication + "/login";
            public const string SendEmailConfirmationLink = BaseAuthentication + "/sendEmailConfirmationLink";
            public const string ConfirmEmail = BaseAuthentication + "/confirmEmail";
            public const string ForgotPassword = BaseAuthentication + "/forgotPassword";
            public const string ResetPassword = BaseAuthentication + "/resetPassword";
            public const string Log = BaseAuthentication + "/log";
        }

    }
}