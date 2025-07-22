using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Shopping.Web.Pages
{
    public class OrderListModel(IOrderingService orderingService , ILogger<OrderListModel> logger) 
        : PageModel
    {
        public IEnumerable<OrderModel> Orders { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Test orderging list model");

            var customerId = new Guid("4c38df16-969b-414b-ad40-50c83fe233bd");

            var response = await orderingService.GetOrdersByCustomer(customerId);

            return Page();
        }
    }
}
