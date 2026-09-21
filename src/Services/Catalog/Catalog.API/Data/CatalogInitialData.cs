using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync(cancellation))
            {
                return;
            }

            session.Store(GetPreconfiguredProducts());
            await session.SaveChangesAsync(cancellation);
        }

        public static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = new Guid("4f0a5a5a-5a5a-4a5a-9a5a-5a5a5a5a5a5a"),
                    Name = "Product 1",
                    ImageUrl = "Product1.png",
                    Description = "Description for Product 1",
                    Price = 10.99m,
                    Category = new List<string> { "Category1", "Category2" },
                },
                new Product
                {
                    Id = new Guid("5f0a5a5a-5a5a-4a5a-9a5a-5a5a5a5a5a5b"),
                    Name = "Product 2",
                    ImageUrl = "Product2.png",
                    Description = "Description for Product 2",
                    Price = 19.99m,
                    Category = new List<string> { "Category2", "Category3" },
                },
                new Product
                {
                    Id = new Guid("6f0a5a5a-5a5a-4a5a-9a5a-5a5a5a5a5a5c"),
                    Name = "Product 3",
                    ImageUrl = "Product3.png",
                    Description = "Description for Product 3",
                    Price = 5.99m,
                    Category = new List<string> { "Category3", "Category4" },
                }
            };
        }
    }
}
