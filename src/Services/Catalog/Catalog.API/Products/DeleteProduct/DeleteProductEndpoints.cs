
using Catalog.API.Products.UpdateProduct;
using MediatR;

namespace Catalog.API.Products.DeleteProduct
{
    //public record DeleteProductRequest(Guid Id);


    public record DeleteProductResponse(bool isSuccess);

    public class DeleteProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/product/{id}", async (Guid id, ISender sender) =>
            {

                var result = await sender.Send(new DeleteProductCommand(id));

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
                
            })
               .WithName("DeleteProduct")
               .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .ProducesProblem(StatusCodes.Status404NotFound)
               .WithSummary("Delete Product")
               .WithDescription("Delete Product");
        }
    }
}
