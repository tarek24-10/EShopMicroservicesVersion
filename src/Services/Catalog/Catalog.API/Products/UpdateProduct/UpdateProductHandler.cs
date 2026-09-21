namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, string ImageUrl
        , decimal Price, List<string> Category) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").Length(1, 100)
                .WithMessage("Name must be between 1 and 100 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Image URL is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
        }
    }

    internal class UpdateProductHandler(IDocumentSession session) 
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var productfromDb = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (productfromDb == null)
            {
                throw new ProductNotFoundException(productfromDb.Id);
            }

            productfromDb.Update(command.Name, command.Description, command.ImageUrl
                , command.Price, command.Category);

            session.Update(productfromDb);

            await session.SaveChangesAsync(cancellationToken);
            
            return new UpdateProductResult(true);
        }
    }
}
