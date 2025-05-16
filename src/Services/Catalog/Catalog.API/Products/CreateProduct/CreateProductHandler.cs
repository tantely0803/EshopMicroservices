namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
        : ICommand<CreateProductResult>
    {

    }

    public record CreateProductResult(Guid Id)
    {

    }

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name iz required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("CAtegory is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price must be ggreater than 0");
        }
    }

    internal class CreateProductCommandHandler
        (IDocumentSession session)  
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // create Produsct entity from command object 
            // save to database
            // return CreateProductResult result 

            //var result = await validator.ValidateAsync(command, cancellationToken);
            //var errors = result.Errors.Select(x => x.ErrorMessage).ToList();

            //if (errors.Any()) {
            //    throw new ValidationException(errors.FirstOrDefault());
            //}

            //logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}" , command);
       
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
