
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints
{
    public record CreateOrderRequest(OrderDto order);

    public record CreateOrderResponse(Guid Id);


    public class CreateOrder : ICarterModule
    {

        //accept a create order request object 
        // maps the request to a CreateOrderCommand object
        // Use MediatR to send the command to the appropriate handler
        // return a response object with the order id 
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateOrderCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateOrderResponse>();

                return Results.Created($"/orders/{response.Id}", response);

            })
               .WithName("CreateOrder")
               .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Create Order")
               .WithDescription("Create Order");
        }
    }
}
