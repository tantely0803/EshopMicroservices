
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts
{
    // qithout paging
    //public record GetProductsRequest();

    public record GetProductsRequest(int? PageNumber = 1 , int? PageSize = 10);


    public record getProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<getProductsResponse>();

                return Results.Ok(response);
            })
               .WithName("GetProducts")
               .Produces<CreateProductResponse>(StatusCodes.Status201Created)
               .WithSummary("Get Products")
               .WithDescription("Get Products");
        }
    }
}
