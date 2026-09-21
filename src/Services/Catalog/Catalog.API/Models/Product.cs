namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!; 
        public decimal Price { get; set; }
        public List<string> Category { get; set; } = new();

        public static Product Create(string name, string description, string imageUrl, decimal price, List<string> category)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Price = price,
                Category = category
            };
        }

        public void Update(string name, string description, string imageUrl, decimal price, List<string> category)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            Category = category;
        }
    }
}
