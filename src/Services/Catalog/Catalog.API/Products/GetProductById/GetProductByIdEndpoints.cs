﻿
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductBy8Id
{
    //public record GetProductByIdRequest();

    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);
            })
               .WithName("GetProductById")
               .Produces<GetProductByIdResponse>(StatusCodes.Status201Created)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Get Product By Id")
               .WithDescription("Get Product By Id");
        }
    }
}
