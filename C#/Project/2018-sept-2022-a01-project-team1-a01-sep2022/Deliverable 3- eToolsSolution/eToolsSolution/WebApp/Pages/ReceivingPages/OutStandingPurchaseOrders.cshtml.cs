using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Receiving.BLL;
using Receiving.ViewModels;

namespace WebApp.Pages.ReceivingPages
{
    public class OutStandingPurchaseOrdersModel : PageModel
    {
        private readonly OutStandingPurchaseOrderServices _OPOServices;

        public OutStandingPurchaseOrdersModel(OutStandingPurchaseOrderServices OPOServices)
        {
            _OPOServices = OPOServices;
        }

        public List<PurchaseOrderList> PurchaseOrderList { get; set; } = new();

        public void OnGet()
        {
            PurchaseOrderList = _OPOServices.DisplayOutstandingPO();
        }
    }
}
