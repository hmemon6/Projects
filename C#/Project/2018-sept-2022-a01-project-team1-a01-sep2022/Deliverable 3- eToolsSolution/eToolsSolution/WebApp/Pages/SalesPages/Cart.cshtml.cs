#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.BLL;
using Sales.ViewModels;

namespace WebApp.Pages.SalesPages
{
    public class CartModel : PageModel
    {
        private StockItemServices _stockItemServices;
        public CartModel(StockItemServices stockItemServices)
        {
            _stockItemServices = stockItemServices;
        }
        [TempData]
        public string FeedBack { get; set; }
        public List<ItemRegistration> Cart { get; set; } = ShoppingCart.ShoppingCartItems;

        [BindProperty]
        public int Quantity { get; set; }

        [BindProperty]
        public int Item { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostRemoveItem()
        {
            ItemRegistration item = Cart.FirstOrDefault(x => x.StockItemID == Item);
            ShoppingCart.ShoppingCartItems.Remove(item);
            FeedBack = "Item Removed.";
            return Page();
        }

        public IActionResult OnPostChangeQuantity()
        {
            ItemRegistration item = Cart.FirstOrDefault(x => x.StockItemID == Item);
            ShoppingCart.ShoppingCartItems.Remove(item);
            item.Quantity = Quantity;
            ShoppingCart.ShoppingCartItems.Add(item);
            FeedBack = "Item Quantity Changed";
            return Page();
        }
    }
}
