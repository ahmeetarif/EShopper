﻿namespace EShopper.Contracts.V1
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

        public static class Category
        {
            public const string BaseCategory = Base + "/category";

            public const string Create = BaseCategory + "/create";

        }

        public static class UserManager
        {
            private const string BaseUserManager = Base + "/userManager";

            public const string AddUserRole = BaseUserManager + "/addUserRole";
            public const string GetMyRoles = BaseUserManager + "/getRoles";
        }

        public static class Roles
        {
            private const string BaseRoles = Base + "/role";

            public const string GetRoles = BaseRoles + "/getRoles";
            public const string CreateRole = BaseRoles + "/createRole";
            public const string DeleteRole = BaseRoles + "/deleteRole";
        }


    }
}