namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public string ImageUrl { get; private set; } = default!; 
        public decimal Price { get; private set; }
        public List<string> Categories { get; private set; } = new();

        public static Product Create(string name, string description, string imageUrl, decimal price, List<string> categories)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Price = price,
                Categories = categories
            };
        }
    }
}
