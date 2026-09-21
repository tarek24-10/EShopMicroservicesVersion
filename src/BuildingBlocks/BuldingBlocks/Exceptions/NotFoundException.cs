namespace BuldingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string entityName, object key) : 
            base($"entity {entityName} with key {key} not found")
        {
        }
    }
}
