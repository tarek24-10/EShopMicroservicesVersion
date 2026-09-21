namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand
    (string Name, string Description, string ImageUrl, decimal Price, List<string> Category)
    : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Image URL is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
        }
    }

    internal class CreateProductHandler(IDocumentSession session)
            : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.Name, request.Description, request.ImageUrl
                , request.Price, request.Category);

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
