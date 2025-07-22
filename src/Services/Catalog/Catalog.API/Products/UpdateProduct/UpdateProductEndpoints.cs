
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.GetProductBy8Id;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record UpdateProductRespone(bool IsSuccess);


    public class UpdateProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductRespone>();

                return Results.Ok(response);
            })
               .WithName("UpdateProduct")
               .Produces<UpdateProductRespone>(StatusCodes.Status201Created)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .ProducesProblem(StatusCodes.Status404NotFound)
               .WithSummary("Update Product")
               .WithDescription("Update Product");
        }
    }
}
