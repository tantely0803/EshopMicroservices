

using System.Net;

namespace Shopping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> GetBasket(string userName);

        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{userName}")]
        Task<DeleteBasketResponse> DeleteBasket(string userName);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(GetBasketCheckoutRequest request);

        public async Task<ShoppingCartModel> LoadUserBasket()
        {
            var userName = "swn";
            ShoppingCartModel basket;

            try
            {
                var response = await GetBasket(userName);
                basket = response.Cart;
            }
            catch (ApiException apiException) when (apiException.StatusCode == HttpStatusCode.NotFound)
            {
                // If the basket is not found, create a new one
                basket = new ShoppingCartModel
                {
                    UserName = userName,
                    Items = [],
                };
            }

            return basket;
        }
    }
}
