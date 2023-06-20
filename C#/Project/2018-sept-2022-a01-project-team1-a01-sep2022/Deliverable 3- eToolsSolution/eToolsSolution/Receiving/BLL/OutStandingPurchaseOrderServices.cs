using Receiving.DAL;
using Receiving.Entities;
using Receiving.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiving.BLL
{
    public class OutStandingPurchaseOrderServices
    {

        private ReceivingContext _context;

        internal OutStandingPurchaseOrderServices(ReceivingContext context)
        {
            _context = context;
        }

        public List<PurchaseOrderList> DisplayOutstandingPO()
        {
            IEnumerable<PurchaseOrderList> purchaseOrders = _context.PurchaseOrders
                    .Where(x => x.Closed == false && x.OrderDate != null)
                    .Select(x => new PurchaseOrderList
                    {
                        PurchaseOrderID = x.PurchaseOrderID,
                        OrderDate = x.OrderDate,
                        Vendor = x.Vendor.VendorName,
                        Phone = x.Vendor.Phone
                    });

            return purchaseOrders.ToList();
        }


        public List<SelectedPurchaseOrderDetails> DisplayOutstandingPODetails(int OutstandingPOID)
        {
            //IEnumerable<SelectedPurchaseOrderDetails> purchaseOrderDetails = _context.PurchaseOrderDetails
            var purchaseOrderDetails = _context.PurchaseOrderDetails
                .Where(x => x.PurchaseOrderID == OutstandingPOID)
                .Select(x => new SelectedPurchaseOrderDetails
                {
                    StockItemID = x.StockItemID,
                    Description = x.StockItem.Description,
                    QuanitiyOrdered = x.Quantity,
                    QuanitiyOutStanding = x.StockItem.QuantityOnOrder,
                });
            //return purchaseOrderDetails.ToList();
            return purchaseOrderDetails.ToList();
        }




    }
}
