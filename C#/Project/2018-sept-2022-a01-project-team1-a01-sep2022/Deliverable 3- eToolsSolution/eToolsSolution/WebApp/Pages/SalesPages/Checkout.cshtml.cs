#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.BLL;
using Sales.ViewModels;

namespace WebApp.Pages.SalesPages
{
    public class CheckoutModel : PageModel
    {
        private CouponServices _couponServices;
        private SaleServices _saleServices;
        private SaleDetailServices _saleDetailServices;
        public CheckoutModel(CouponServices couponServices, SaleServices saleServices, SaleDetailServices saleDetailServices)
        {
            _couponServices = couponServices;
            _saleServices = saleServices;
            _saleDetailServices = saleDetailServices;
        }

        [TempData]
        public string FeedBack { get; set; }    
        public List<ItemRegistration> Cart { get; set; } = ShoppingCart.ShoppingCartItems;
        [BindProperty]
        public string CouponID { get; set; }
        public CouponInfo Coupon { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public void OnGet()
        {
            CalculateCosts();
        }

        public IActionResult OnPostCouponCheck()
        {
            CalculateCosts();
            if (CouponID != null)
            {
                Coupon = _couponServices.Coupon_FetchByCouponIDValue(CouponID);
                if (Coupon != null)
                {
                    Discount = Subtotal * (decimal)(Coupon.CouponDiscount * 0.01);
                    FeedBack = $"Discount Added {Discount} {Coupon.CouponDiscount * 0.01} ";
                }
                else
                {
                    FeedBack = "Coupon Invalid";
                }
            }

            CalculateDiscount();
            return Page();
        }

        public IActionResult OnPostPlaceOrder()
        {
            int? couponid = null;
            if(Coupon != null)
            {
                couponid = Coupon.CouponID;
            }

            if (ShoppingCart.ShoppingCartItems.Count > 0)
            {
                _saleServices.AddSale("C", 1, couponid, ShoppingCart.ShoppingCartItems);
                FeedBack = "Sale has been created.";
                ShoppingCart.ShoppingCartItems.Clear();
            }
            else
            {
                FeedBack = "No items to purchase";
            }
            return Page();
        }

        public void CalculateCosts()
        {
            foreach (var item in Cart)
            {
                Subtotal = Subtotal + (item.Quantity * item.SellingPrice);
            }
            Tax = Subtotal * (decimal)0.05;
            Total = Subtotal + Tax;
        }

        public void CalculateDiscount()
        {
            Total = Subtotal - Discount;
        }
    }
}
