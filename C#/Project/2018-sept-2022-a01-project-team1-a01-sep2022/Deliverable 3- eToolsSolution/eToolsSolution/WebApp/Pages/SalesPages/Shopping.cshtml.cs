#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.BLL;
using Sales.ViewModels;

namespace WebApp.Pages.SalesPages
{
    public class ShoppingModel : PageModel
    {
        private CategoryServices _categoryServices;
        private StockItemServices _stockItemServices;
        public ShoppingModel(CategoryServices categoryServices,
                             StockItemServices stockitemServices)
        { 
            _categoryServices = categoryServices;
            _stockItemServices = stockitemServices;
        }


        [BindProperty(SupportsGet = true)]
        public List<CategoryInfo> CategoryList { get; set; } = new List<CategoryInfo>();
        [BindProperty(SupportsGet = true)]
        public List<StockItemInfo> ItemCountList { get; set;} = new List<StockItemInfo>();
        
        public List<StockItemInfo> ItemList { get; set; } = new List<StockItemInfo>();

        [BindProperty(SupportsGet = true)]
        public CategoryInfo Category { get; set; }
        [BindProperty]
        public int ItemQuantity { get; set; }

        [BindProperty]
        public int StockItem { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CategoryDescription { get; set; }

        [TempData]
        public string FeedBack { get; set; }
        public void OnGet()
        {
            CategoryListFill();
        }

        public IActionResult OnPostSearch()
        {
            CategoryListFill();
            ItemList = _stockItemServices.StockItems_FetchByCategoryDescription(CategoryDescription);
            return Page();
        }
        public IActionResult OnPostAddItem()
        {
            try
            {
                _stockItemServices.Add_Item(StockItem, ItemQuantity);
                FeedBack = $"Added {ItemQuantity} items";
            }
            catch (Exception ex)
            { 
                FeedBack = ex.Message;
            }
            return RedirectToPage("/SalesPages/Cart");
        }

        public void CategoryListFill()
        {
            CategoryList = _categoryServices.Category_FetchCategory();

            foreach (var category in CategoryList)
            {
                ItemCountList = _stockItemServices.StockItems_FetchByCategoryDescription(category.Description);
                CategoryList.FirstOrDefault(x => x.CategoryID == category.CategoryID).Quantity = ItemCountList.Count();
            }
        }
    }
}
