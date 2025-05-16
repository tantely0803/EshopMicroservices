using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Extentions
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders) 
        {
            return orders.Select(order => new OrderDto(
                 Id: order.Id.Value,
                    CustomerId: order.CustomerId.Value,
                    OrderName: order.OrderName.Value,
                    ShippingAddress: new AddressDto(
                        order.ShippingAddress.FirstName,
                        order.ShippingAddress.LastName,
                        order.ShippingAddress.Country,
                        order.ShippingAddress.State,
                        order.ShippingAddress.ZipCode,
                        order.ShippingAddress.AddressLine,
                        order.ShippingAddress.EmailAdress
                        ),
                    BillingAddress: new AddressDto(
                        order.BillingAddress.FirstName,
                        order.BillingAddress.LastName,
                        order.BillingAddress.Country,
                        order.BillingAddress.State,
                        order.BillingAddress.ZipCode,
                        order.BillingAddress.AddressLine,
                        order.BillingAddress.EmailAdress
                        ),
                    Payment: new PaymentDto(
                        order.Payment.CardName,
                        order.Payment.CardNumber,
                        order.Payment.Expiration,
                        order.Payment.CVV,
                        order.Payment.PaymentMethod
                        ),
                    Status: order.Status,
                    OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                ));
        }
    }
}
