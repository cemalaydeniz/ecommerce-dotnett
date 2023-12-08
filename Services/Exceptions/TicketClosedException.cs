namespace ecommerce_dotnet.Services.Exceptions
{
    public class TicketClosedException : Exception
    {
        public TicketClosedException(string message) : base(message) { }
    }
}
