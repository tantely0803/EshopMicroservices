namespace Shopping.Web.Models.Ordering
{
    public record OrderModel
    (
        Guid Id,
        Guid CustomerId,
        string OrderName,
        AddressModel ShippingAddress,
        AddressModel BillingAddress,
        PaymentModel Payment,
        OrderStatus Status,
        List<OrderItemModel> OrderItems
    );

    public record OrderItemModel(
        Guid OrderId,
        string ProductId,
        int Quantity,
        decimal Price
    );

    public record AddressModel(
        string FirstName,
        string LastName,
        string EmailAddress,
        string AddressLine,
        string Country,
        string State,
        string ZipCode
    );

    public record PaymentModel(
        string CardName,
        string CardNumber,
        string Expiration,
        string CVV,
        int PaymentMethod
    );

    public enum OrderStatus
    {
        Pending = 1 ,
        Processing = 2,
        Shipped = 3,
        Delivered = 4,
        Cancelled = 5
    }

    public record GetOrderResponse(PaginatedResult<OrderModel> Orders);

    public record GetOrdersByNameResponse(IEnumerable<OrderModel> Orders);

    public record GetOrdersByCustomerResponse(IEnumerable<OrderModel> Orders);
}
