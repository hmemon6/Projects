#nullable disable
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
    public class PurchasingModel : PageModel
    {
        #region Private variables and DI constructor
        private readonly PurchaseOrderServices _purchaseOrderServices;
        public PurchasingModel(PurchaseOrderServices purchaseOrderServices)
        {
            _purchaseOrderServices = purchaseOrderServices;
        }
        #endregion

        #region Messaging and Error Handling
        [TempData]
        public string FeedBackMessage { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> ErrorDetails { get; set; } = new();
        public List<Exception> ErrorList { get; set; } = new();

        #endregion

        //Important to note: if you don't get a default value, the property will permanently stay null and when accessing it later,
        //    you'll get an Object set to reference of null error (aka trying to access object that doesn't exist)
        // Also, set to BindProperty else the data wont save on return Page()

        [BindProperty(SupportsGet=true)]
        public List<DisplayVendorSelection> VendorSelection { get; set; } = new List<DisplayVendorSelection>();

        [BindProperty(SupportsGet = true)]
        public List<DisplayCurrentActiveOrderItems> CurrentItems { get; set; } = new List<DisplayCurrentActiveOrderItems>();

        [BindProperty(SupportsGet = true)]
        public List<DisplayCurrentActiveOrderItems> AvailableItems { get; set; } = new List<DisplayCurrentActiveOrderItems>();

        // vendorSelection drop-down list value
        [BindProperty(SupportsGet = true)]
        public int SelectedVendorID { get; set; }

        // for list-list functionality
        //  product id that is used to select StockItem from AvailableItems and transfer to CurrentItems
        [BindProperty]public int SelectedStockItemID { get; set; }

        //  product id that is used to select the command model on my CurrentItems and update the totals (qty * price)
        [BindProperty] public int RefreshProductID { get; set; }

        //  product id that is used to remove the command model from my CurrentItem and then used to update my AvailableItems query model
        [BindProperty] public int RemoveStockItemID { get; set; }

        [BindProperty(SupportsGet = true)]
        public DisplayVendorInfo VendorInfo { get; set; } = new DisplayVendorInfo();

        public void OnGet()
        {
            VendorSelection = _purchaseOrderServices.Get_DisplayVendorSelection();

            //Due to error checks in the context method (due to business logic), we cant pass a valid vendorID until it's been selected,
            //    so no need to populate those lists yet (as they rely on vendorID)
            //CurrentItems = _purchaseOrderServices.Get_DisplayCurrentActiveOrder(SelectedVendorID);
            //AvailableItems = _purchaseOrderServices.Get_DisplayStockItems(SelectedVendorID, CurrentItems);
        }

        public IActionResult OnPostSelectVendor()
        {
            //no vendor selected (default value is 0), dont populate lists (as we dont have vendorID to pass), avoids error.
            if (SelectedVendorID == 0)
            {
                VendorSelection = _purchaseOrderServices.Get_DisplayVendorSelection();
            }
            else
            {
                //Basically doing same logic as OnGet()
                VendorSelection = _purchaseOrderServices.Get_DisplayVendorSelection();
                CurrentItems = _purchaseOrderServices.Get_DisplayCurrentActiveOrderItems(SelectedVendorID);
                AvailableItems = _purchaseOrderServices.Get_DisplayCurrentActiveOrderItemsFiltered(SelectedVendorID, CurrentItems);

                VendorInfo = _purchaseOrderServices.Get_DisplayVendorInfo(SelectedVendorID);
                //Before refreshing Page() by returning, we must basically manually call the code inside the OnGet(),
                //    because it won't  get called. Aka repopulate lists and etc as it would be on page startup.
                return Page();
            }

            //return Page() early in the if() doesn't stop the rest of code from activating, so put it in else statement.
            //    Also move return Page() outside here so don't get any errors.
            return Page();
        }

        public IActionResult OnPostAddItem()
        {

            var selectedItem = AvailableItems.SingleOrDefault(x => x.StockItemID == SelectedStockItemID);
            if (selectedItem != null)
            {
                //  remove the item from the availableItems list (query model)
                AvailableItems.Remove(selectedItem);
                //  update the selectedItem/currentItems list with initial values
                selectedItem.QuantityToOrder = 1;
                //selectedItem.Total = selectedItem.UnitPrice * selectedItem.Quantity;
                //  add item to currentItems list (command model)
                CurrentItems.Add(selectedItem);
                CurrentItems = CurrentItems.OrderBy(x => x.StockItemID).ToList();
                VendorSelection = _purchaseOrderServices.Get_DisplayVendorSelection();

            }
            ////  *NOTE: if you get a bug where everything displays as 0 or doesn't refresh, call the Get_CurrentActiveOrder / Get_StockItem methods from the Get(),
            //// because it doesn't fire here if we return to Page().
            //// Basically we manually call what's inside the OnGet() method.
            return Page();
        }

        //  remove command model item from sale item list and refresh the query modem inventory list with a clean record
        public IActionResult OnPostRemoveItem()
        {

            //  get my query model
            var selectedItem = CurrentItems.SingleOrDefault(x => x.StockItemID == RemoveStockItemID);
            if (selectedItem != null)
            {
                //  remove the item (query model) from the items list
                CurrentItems.Remove(selectedItem);
                AvailableItems.Add(_purchaseOrderServices.Get_DisplayCurrentActiveOrderItemsByID(RemoveStockItemID));
                AvailableItems = AvailableItems.OrderBy(x => x.StockItemID).ToList();
                VendorSelection = _purchaseOrderServices.Get_DisplayVendorSelection();
            }
            ////  OnGet does NOT Fire
            return Page();
        }

        //  Refresh line total
        public IActionResult OnPostRefreshItem()
        {
            //var selectedItem = NewSaleItems.SingleOrDefault(x => x.ProductID == RefreshProductID);
            //if (selectedItem != null)
            //{
            //    selectedItem.Total = selectedItem.UnitPrice * selectedItem.Quantity;
            //}
            ////  OnGet does NOT Fire
            return Page();
        }

        //Commands
        public IActionResult OnPostUpdate()
        {
            if (SelectedVendorID == 0)
            {
                VendorSelection = _purchaseOrderServices.Get_DisplayVendorSelection();
            }
            else
            {
                _purchaseOrderServices.Update_CurrentActiveOrder(VendorInfo.EmployeeID, VendorInfo.VendorID, VendorInfo.PurchaseOrderID, CurrentItems);

                VendorSelection = _purchaseOrderServices.Get_DisplayVendorSelection();
                return Page();
            }
            return Page();
        }
        public IActionResult OnPostPlace()
        {
            if (SelectedVendorID == 0)
            {
                VendorSelection = _purchaseOrderServices.Get_DisplayVendorSelection();
            }
            else
            {
                _purchaseOrderServices.Place_CurrentActiveOrder(VendorInfo.PurchaseOrderID);

                VendorSelection = _purchaseOrderServices.Get_DisplayVendorSelection();
                return Page();
            }
            return Page();
        }
        public IActionResult OnPostDelete()
        {
            if (SelectedVendorID == 0)
            {
                VendorSelection = _purchaseOrderServices.Get_DisplayVendorSelection();
                CurrentItems = new List<DisplayCurrentActiveOrderItems>();
                AvailableItems = new List<DisplayCurrentActiveOrderItems>();
            }
            else
            {
                _purchaseOrderServices.Delete_CurrentActiveOrder(VendorInfo.PurchaseOrderID);

                VendorSelection = _purchaseOrderServices.Get_DisplayVendorSelection();
                CurrentItems = new List<DisplayCurrentActiveOrderItems>();
                AvailableItems = new List<DisplayCurrentActiveOrderItems>();
                return RedirectToPage(new { SelectedVendorID = SelectedVendorID });
            }
            return Page();
        }
        public IActionResult OnPostClear()
        {
            VendorSelection = _purchaseOrderServices.Get_DisplayVendorSelection();
            CurrentItems = new List<DisplayCurrentActiveOrderItems>();
            AvailableItems = new List<DisplayCurrentActiveOrderItems>();
            return Page();
        }
    }
}
