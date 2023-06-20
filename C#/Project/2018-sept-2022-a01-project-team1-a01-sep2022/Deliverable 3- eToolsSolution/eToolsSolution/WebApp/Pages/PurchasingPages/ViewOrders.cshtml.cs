using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Eventing.Reader;
using Purchasing.BLL;
using Purchasing.ViewModels;
using System.Xml.Linq;
using EToolsSecurity.BLL;
using EToolsSecurity.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApp.Pages.PurchasingPages
{
    public class ViewOrdersModel : PageModel
    {
        private readonly PurchaseOrderServices _purchaseOrderServices;
        public ViewOrdersModel(PurchaseOrderServices purchaseOrderServices)
        {
            _purchaseOrderServices = purchaseOrderServices;
        }

        public List<AllPurchaseOrders> AllPurchaseOrders { get; set; } = new();

        public void OnGet()
        {
            AllPurchaseOrders = _purchaseOrderServices.Get_AllPurchaseOrders();
        }
    }
}
