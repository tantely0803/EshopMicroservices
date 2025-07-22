using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class CartModel(IBasketService basketService, ILogger<CartModel> logger)
        : PageModel
    {
        public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

        public async Task<IActionResult> OnGetAsync()
        {
            //logger.LogInformation("Cart page visited");
            Cart = await basketService.LoadUserBasket();


            return Page();
        }

        public async Task<IActionResult> OnPostRemoveItemAsync(Guid productId)
        {
            logger.LogInformation("Removing item from cart");
            
            Cart = await basketService.LoadUserBasket();

            Cart.Items.RemoveAll(item => item.ProductId == productId);

            await basketService.StoreBasket(new StoreBasketRequest(Cart));

            return RedirectToPage();
        }
    }
}
