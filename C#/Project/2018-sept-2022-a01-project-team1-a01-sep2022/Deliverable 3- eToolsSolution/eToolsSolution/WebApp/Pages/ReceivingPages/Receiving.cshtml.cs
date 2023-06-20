using EToolsSecurity.BLL;
using EToolsSecurity.ViewModel;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Receiving.BLL;
using Receiving.ViewModels;
using static Humanizer.In;

namespace WebApp.Pages.ReceivingPages
{
    public class ReceivingModel : PageModel
    {
        private readonly OutStandingPurchaseOrderServices _opoServices;
        private readonly ReceiveOrderServices _receivingServices;
        private readonly SecurityService _securityService;

        public ReceivingModel(OutStandingPurchaseOrderServices opoServices, ReceiveOrderServices receivingServices, SecurityService securityService)
        {
            _opoServices = opoServices;
            _receivingServices = receivingServices;
            _securityService = securityService;
        }

        [TempData]
        public string ErrorMessage { get; set; }
        [TempData]
        public string FeedBackMessage { get; set; }
        public List<string> ErrorDetails { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string ForceCloseReason { get; set; }
        [BindProperty(SupportsGet = true)]
        public int POID { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectedPurchaseOrderDetails> SelectedPODetails { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<SelectedPurchaseOrderDetails> TransactionList { get; set; }


        [BindProperty]
        public string UnorderedDescription { get; set; }
        [BindProperty(SupportsGet = true)]
        public string UnorderedItemVSN { get; set; }
        [BindProperty(SupportsGet = true)]
        public int UnorderedQty { get; set; }


        [BindProperty(SupportsGet = true)]
        public List<UnOrderedItemsList> UnOrderedItems { get; set; }

        [BindProperty(SupportsGet = true)] 
        public int RemoveStockID { get; set; }

        [BindProperty]
        public EmployeeInfo Employee { get; set; }

        // Please use below to change the value to true or false for if manager = true otherwise set it to false to Disable force close

        [BindProperty(SupportsGet =true)]
        public bool isManager { get; set; }

        public IActionResult OnGet(int CurrentPurchaseOrderID)
        {
            POID = CurrentPurchaseOrderID;
            UnOrderedItems = _receivingServices.ReturnItemsList();
            Employee = _securityService.GetEmployeeInfo(isManager);
            SelectedPODetails = _opoServices.DisplayOutstandingPODetails(POID);
            return Page();
        }


        public IActionResult OnPostReceive()
        {
            try
            {
                Employee = _securityService.GetEmployeeInfo(isManager);
                foreach (var stockItem in SelectedPODetails)
                {
                    TransactionList.Add(new SelectedPurchaseOrderDetails()
                    {
                        StockItemID = stockItem.StockItemID,
                        Description = stockItem.Description,
                        QuanitiyOrdered = stockItem.QuanitiyOrdered,
                        QuanitiyOutStanding = stockItem.QuanitiyOutStanding,
                        Receive = stockItem.Receive,
                        Reason = stockItem.Reason,
                        Return = stockItem.Return
                    });
                }

                _receivingServices.RecievePOService(TransactionList, POID, Employee.EmployeeID);

                if (ErrorDetails.Count() == 0 && string.IsNullOrEmpty(ErrorMessage))
                {
                    int sum = 0;
                    SelectedPODetails = _opoServices.DisplayOutstandingPODetails(POID);

                    //SelectedPODetails.Count();
                    
                    foreach (var stockItem in SelectedPODetails)
                    {
                        if(stockItem.QuanitiyOutStanding == 0)
                        {
                            sum++;
                        }
                    }
                    if(sum == SelectedPODetails.Count())
                    {
                        return RedirectToPage("/ReceivingPages/OutStandingPurchaseOrders");
                    }
                    FeedBackMessage = $"Delivery Received";
                    //This clears out input fields
                    return RedirectToPage(new
                    {
                        FeedBackMessage = "",
                        CurrentPurchaseOrderID = POID
                    }) ;
                }

                //return Page();
                //SelectedPODetails = _opoServices.DisplayOutstandingPODetails(POID);
                return RedirectToPage(new
                {
                    ErrorMessage = "",
                    CurrentPurchaseOrderID = POID
                }); 
            }


            catch (AggregateException ex)
            {
                foreach (var error in ex.InnerExceptions)
                {
                    ErrorDetails.Add(error.Message);
                }
                return Page();

            }
            catch (ArgumentNullException ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                return Page();
            }

            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                //return Page();
                return RedirectToPage(new
                {
                    ErrorMessage = "",
                    CurrentPurchaseOrderID = POID
                });
            }

        }
        public IActionResult OnPostForceClose()
        {
            try
            {

                _receivingServices.ForceCloseService(POID, ForceCloseReason);

                if (string.IsNullOrEmpty(ErrorMessage))
                {

                    //FeedBackMessage = $"Purchase Order closed!";
                    return RedirectToPage("/ReceivingPages/OutStandingPurchaseOrders");
                    //return Page();
                }
                return Page();
            }

            catch (ArgumentNullException ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                return RedirectToPage(new
                {
                    FeedBackMessage = "",
                    ErrorMessage = "",
                    CurrentPurchaseOrderID = POID
                });
            }

            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                //return Page();
                return RedirectToPage(new
                {
                    FeedBackMessage = "",
                    ErrorMessage = "",
                    CurrentPurchaseOrderID = POID
                });
            }


        }


        public IActionResult OnPostInsert()
        {
            try
            {
                List<UnOrderedItemsList> unOrderedItemList = new List<UnOrderedItemsList>();
                unOrderedItemList.Add(new UnOrderedItemsList()
                {
                    Description = UnorderedDescription,
                    Quantity = UnorderedQty,
                    VSN = UnorderedItemVSN
                });

                _receivingServices.Insert_UnorderedItem(unOrderedItemList);
                //return RedirectToPage("/ReceivingPages/Receiving");

                //int maxid = UnOrderedItems.Select(x => x.CID).Max();
                //var selectedItem = UnOrderedItems.SingleOrDefault(x => x.CID == maxid);
                //if(selectedItem != null)
                //{
                //    UnOrderedItems.Add(selectedItem);
                //}
                UnOrderedItems = _receivingServices.ReturnItemsList();

                return Page();
            }
            

            catch (AggregateException ex)
            {
                foreach (var error in ex.InnerExceptions)
                {
                    ErrorDetails.Add(error.Message);
                }
                return Page();

            }
            catch (ArgumentNullException ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                return Page();
            }

            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                return Page();
            }


        }

        public IActionResult OnPostRemove()
        {
            try
            {

                var selectedItem = UnOrderedItems.SingleOrDefault(x => x.CID == RemoveStockID);

                if (selectedItem != null)
                {
                    _receivingServices.Remove_UnorderedItem(RemoveStockID);
                    UnOrderedItems.Remove(selectedItem);
                }

                //return RedirectToPage("/ReceivingPages/Receiving");
                return Page();
            }


            catch (AggregateException ex)
            {
                foreach (var error in ex.InnerExceptions)
                {
                    ErrorDetails.Add(error.Message);
                }
                return Page();

            }
            catch (ArgumentNullException ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                return Page();
            }

            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                return Page();
            }
        }

        public IActionResult OnPostRefresh()
        {
            if(isManager == true)
            {
                return RedirectToPage(new
                {
                    isManager = true
                });
            }
            else
            {
                return RedirectToPage(new
                {
                    isManager = false
                });
            }
            
        }

        public IActionResult OnPostBack()
        {

            return RedirectToPage("/ReceivingPages/OutStandingPurchaseOrders");

        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }

    }
}
