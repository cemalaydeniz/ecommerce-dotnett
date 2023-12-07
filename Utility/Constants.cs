﻿namespace ecommerce_dotnet.Utility
{
    public static class Constants
    {
        public static class Roles
        {
            public const string User = "user";
            public const string Admin = "admin";
        }

        public static class Response
        {
            public static class General
            {
                public const string InternalServerError = "Internal Server Error";
                public const string BadRequest = "Bad Request";
            }

            public static class User
            {
                public const string UserCreated = "The user has been created";
                public const string UserNotFound = "The user not found";

                public const string WrongPassword = "The password you have entered is wrong";
                public const string LoggedIn = "You have successfully logged in";
                public const string LoggedOut = "You have successfully logged out";

                public const string ProfileNoChange = "No profile changes were made";
                public const string ProfileUpdated = "The profile has been updated";
            }
        }
    }
}
