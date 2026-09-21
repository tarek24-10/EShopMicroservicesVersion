using BuldingBlocks.Exceptions;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid ProductId) : base("Product", ProductId)
        {
            
        }
    }
}
