

namespace Shopping.Web.Pages
{
    public class IndexModel(ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger)
        : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;

        public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Index page visited");
            var result = await catalogService.GetProducts();
            ProductList = result.Products;
            return Page();
        }

        public async Task<IActionResult> OnPostAddToBasketAsync(Guid productId)
        {
            logger.LogInformation("Adding to cart button clicked");

            var productResponse = await catalogService.GetProduct(productId);

            var basket = await basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Quantity = 1,
                Color = "Black", // Default color, can be changed later
            });

            await basketService.StoreBasket(new StoreBasketRequest(basket));

            return RedirectToPage("Cart");

        }
    }
}
