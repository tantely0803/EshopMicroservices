﻿

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext dbContext) 
        : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            //update Order entity from entity object 
            //save to database
            // return result

            var orderId = OrderId.Of(command.Order.Id);

            var order = await dbContext.Orders
                .FindAsync([orderId], cancellationToken: cancellationToken);

            if (order == null) {
                throw new OrderNotFoundException(command.Order.Id);
            }

            UpdateOrderWithNewValues(order, command.Order);

            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
        {
            var updateShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName,
                orderDto.ShippingAddress.LastName,
                orderDto.ShippingAddress.EmailAddress,
                orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.Country,
                orderDto.ShippingAddress.State,
                orderDto.ShippingAddress.ZipCode);

            var updateBillingAddress = Address.Of(orderDto.BillingAddress.FirstName,
                orderDto.BillingAddress.LastName,
                orderDto.BillingAddress.EmailAddress,
                orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.Country,
                orderDto.BillingAddress.State,
                orderDto.BillingAddress.ZipCode);

            var updatePayment = Payment.Of(orderDto.Payment.CardName,
                orderDto.Payment.CardNumber,
                orderDto.Payment.Expiration,
                orderDto.Payment.Cvv,
                orderDto.Payment.PaymentMethod
                );

            order.Update(
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: updateShippingAddress,
                billingAddress: updateBillingAddress,
                payment: updatePayment,
                status: orderDto.Status);
        }
    }
}
