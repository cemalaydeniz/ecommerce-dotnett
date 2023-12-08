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
                public const string Unauthorized = "Unauthorized";
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

            public static class Product
            {
                public const string NewProduct = "The product has been added";
                public const string ProductNotFound = "The product not found";
                public const string ProductNoChange = "No changes were made";
                public const string ProductUpdated = "The product has been updated";
                public const string ProductDeleted = "The product has been deleted";

                public const string BulkAdd = "The bulk addition has been completed successfully";
                public const string BulkEdit = "The bulk edit has been completed successfully";
                public const string BulkDelete = "The bulk deletion has been completed successfully";
            }

            public static class CustomerSupport
            {
                public const string TicketCreated = "The ticket has been created";
                public const string TicketNotFound = "The ticket not found";
                public const string TicketClosed = "The ticket has been closed";
                public const string MessageSent = "The message has been sent";
            }
        }

        public static class Exception
        {
            public const string PageOutofRange = "Wrong page or page size";

            public const string ProductNotFound = "The product not found";
            public const string OrderNotFound = "The order not found";
            public const string TicketNotFound = "The ticket not found";
            public const string TicketClosed = "The ticket is already closed";
        }
    }
}
