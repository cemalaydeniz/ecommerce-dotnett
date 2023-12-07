namespace ecommerce_dotnet.Utility
{
    public static class JsonResponse
    {
        public static object Success(string message)
        {
            return new
            {
                success = true,
                message = message
            };
        }

        public static object Error(string message)
        {
            return new
            {
                success = false,
                message = message
            };
        }

        public static object Data(bool isSuccessfull, object data, string? message = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                return new
                {
                    success = isSuccessfull,
                    data = data
                };
            }

            return new
            {
                success = isSuccessfull,
                data = data,
                message = message
            };
        }
    }
}
