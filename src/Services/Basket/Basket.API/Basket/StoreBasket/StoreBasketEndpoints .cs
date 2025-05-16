using Mapster;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);

    public record StoreBasketRespponse(string userName);

    public class StoreBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<StoreBasketRespponse>();

                return Results.Created($"/basket/{response.userName}", response);

            })
               .WithName("StoreBasket")
               .Produces<StoreBasketRespponse>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Store Basket")
               .WithDescription("Store basket");
        }
    }
}
