namespace BuldingBlocks.Exceptions
{
    public class BadRequestException : Exception
    {
        public string? Details { get; private set; }
        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}
