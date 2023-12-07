namespace ecommerce_dotnet.Utility
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
            }
        }
    }
}
