namespace ecommerce_dotnet.Services.Exceptions
{
    public class PageOutofRange : Exception
    {
        public PageOutofRange(string message) : base(message) { }
    }
}
