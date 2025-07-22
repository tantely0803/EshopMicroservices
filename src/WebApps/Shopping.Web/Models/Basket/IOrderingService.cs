namespace Shopping.Web.Models.Basket
{
    public interface IOrderingService
    {
        [Get("/ordering-service/orders/{id}")]
        Task<GetOrderResponse> GetOrders(int? pageIndex = 1, int? pagesize = 10);

        [Get("/ordering-service/orders/{orderName}")]
        Task<GetOrdersByNameResponse> GetOrdersByName(string orderName);

        [Get("/ordering-service/orders/customer/{customerId}")]
        Task<GetOrdersByCustomerResponse> GetOrdersByCustomer(Guid customerId);
    }
}
