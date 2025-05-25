
using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketChekoutEventHandler(ISender sender , ILogger<BasketChekoutEventHandler> logger)
        : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            // TODO : Create order and start order process
            logger.LogInformation("Integration Event Handled: {IntegrationVent}" , context.Message.GetType().Name);

            var commande = MapToCreateCommand(context.Message);
            await sender.Send(commande);

            throw new NotImplementedException();
        }

        private  CreateOrderCommand MapToCreateCommand(BasketCheckoutEvent message)
        {
            var addressDto = new AddressDto
            (
                message.FirstName,
                message.LastName,
                message.AddressLine,
                message.EmailAddress,
                message.Country,
                message.State,
                message.ZipCode
            );

            var paymentDto = new PaymentDto
            (
                message.CardName,
                message.CardNumber,
                message.Expiration,
                message.CVV,
                message.PaymentMethod
            );

            var orderId = Guid.NewGuid();

            var orderDto = new OrderDto
            (
                Id: orderId,
                CustomerId: message.CustomerId,
                OrderName: message.UserName,
                ShippingAddress: addressDto,
                BillingAddress: addressDto,
                Payment: paymentDto,
                Status: Ordering.Domain.Enums.OrderStatus.Pending,
                OrderItems: [
                    new OrderItemDto(orderId , new Guid("6fe31470-5808-4cab-bbe2-4e42c9f74c9a") , 2 , 500),
                    new OrderItemDto(orderId , new Guid("522f301e-3e6e-4ef8-b7c2-27b7b3d5758a") , 1 , 1000),
                ]
            );

            return new CreateOrderCommand(orderDto);
        }
    }
}
