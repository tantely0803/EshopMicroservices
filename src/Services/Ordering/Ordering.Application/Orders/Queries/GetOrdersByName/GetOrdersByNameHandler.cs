﻿
using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            //get orders by nmae using dbContext
            //return result 
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(query.Name))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            //var orderDtos = ProjectToOrderDto(orders);

            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }

        //private List<OrderDto> ProjectToOrderDto(List<Order> orders )
        //{
        //    List<OrderDto> result = new();

        //    foreach (var order in orders) {
        //        var orderDto = new OrderDto(
        //            Id: order.Id.Value,
        //            CustomerId: order.CustomerId.Value,
        //            OrderName: order.OrderName.Value,
        //            ShippingAddress: new AddressDto(
        //                order.ShippingAddress.FirstName,
        //                order.ShippingAddress.LastName,
        //                order.ShippingAddress.Country,
        //                order.ShippingAddress.State,
        //                order.ShippingAddress.ZipCode,
        //                order.ShippingAddress.AddressLine,
        //                order.ShippingAddress.EmailAdress
        //                ),
        //            BillingAddress: new AddressDto(
        //                order.BillingAddress.FirstName,
        //                order.BillingAddress.LastName,
        //                order.BillingAddress.Country,
        //                order.BillingAddress.State,
        //                order.BillingAddress.ZipCode,
        //                order.BillingAddress.AddressLine,
        //                order.BillingAddress.EmailAdress
        //                ),
        //            Payment: new PaymentDto(
        //                order.Payment.CardName,
        //                order.Payment.CardNumber,
        //                order.Payment.Expiration,
        //                order.Payment.CVV,
        //                order.Payment.PaymentMethod
        //                ),
        //            Status: order.Status,
        //            OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value , oi.ProductId.Value , oi.Quantity , oi.Price )
        //            ).ToList());

        //        result.Add(orderDto);
        //    }
        //    return result;
        //}
    }
}
