#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.BLL;
using Sales.ViewModels;

namespace WebApp.Pages.SalesPages
{
    public class RefundModel : PageModel
    {
        private SaleDetailServices _saleDetailServices;
        private SaleServices _saleServices;
        private RefundServices _refundServices;
        public RefundModel(SaleDetailServices saleDetailServices, SaleServices saleServices, RefundServices refundServices)
        { 
            _saleDetailServices = saleDetailServices;
            _saleServices = saleServices;
            _refundServices = refundServices;
        }

        [TempData]
        public string FeedBack { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SaleID { get; set; }
        public SalesInfo Sale { get; set; }
        public List<SaleDetailsInfo> SaleDetails { get; set; } = new List<SaleDetailsInfo>();
        [BindProperty]
        public List<SaleDetailsInfo> Quantity { get; set; } = new List<SaleDetailsInfo>(); 
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        public int RefundID { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPostLookupSale()
        {
            Sale = _saleServices.Sale_FetchBySaleID(SaleID);
            SaleDetails = _saleDetailServices.SaleDetails_FetchBySaleID(SaleID);

            if (Sale == null)
            {
                FeedBack = "Could Not Find Sale";
            }
            else 
            {
                CalculateRefundPrice();
            }
            return Page();
        }

        public IActionResult OnPostClear()
        {
            FeedBack = "";
            ModelState.Clear();
            return Page();
        }

        public IActionResult OnPostRefund()
        {
            Sale = _saleServices.Sale_FetchBySaleID(SaleID);
            SaleDetails = _saleDetailServices.SaleDetails_FetchBySaleID(SaleID);
            List<ItemRegistration> refundItems = new List<ItemRegistration>();
            foreach (var item in SaleDetails)
            {
                int i = 0;
                ItemRegistration itemReg = new ItemRegistration
                {
                    StockItemID = item.StockItemID,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    SellingPrice = item.SellingPrice
                };
                refundItems.Add(itemReg);
                i++;
            }
            FeedBack = "Refund successfully Created";
            _refundServices.Add_Refund(SaleID, 1, refundItems);
            return Page();
        }

        public void CalculateRefundPrice()
        {
            foreach (var item in SaleDetails)
            {
                SubTotal = SubTotal + (item.Quantity * item.SellingPrice);
            }
            Tax = SubTotal * (decimal)0.05;

            if (Sale.CouponID != null)
            {

                Discount = SubTotal * (decimal)(Sale.CouponDiscount * 0.01);
            }
            else 
            {
                Discount = 0;
            }

            Total = SubTotal + Tax - Discount;
        }
    }
}
