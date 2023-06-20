#nullable disable
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
    public class ReceiveOrderServices
    {



        private ReceivingContext _context;

        internal ReceiveOrderServices(ReceivingContext context)
        {
            _context = context;
        }

        #region UnorderedItems Display

        public List<UnOrderedItemsList> ReturnItemsList()
        {
            IEnumerable<UnOrderedItemsList> returnItems = _context.UnOrderedItems
                                                            .Select(x => new UnOrderedItemsList
                                                            {
                                                                CID = x.ItemID,
                                                                VSN = x.VendorProductID,
                                                                Description = x.ItemName,
                                                                Quantity = x.Quantity
                                                            });

            return returnItems.ToList();
        }

        #endregion

        #region Receive, ordered return
        public void RecievePOService(List<SelectedPurchaseOrderDetails> purchaseOrderItemList, int outstandingPOSelected, int receiverEmployeeID)
        {
            ReceiveOrder receiveEntry = null;
            ReceiveOrderDetail receiveOrderDetailEntry = null;
            ReturnedOrderDetail returnItems = null;
            List<Exception> errorList = new List<Exception>();

            int count = 0;
            var unOrderedReturn = _context.UnOrderedItems.ToList();
            int sumReceive = purchaseOrderItemList.Sum(x => x.Receive);
            int sumReturn = purchaseOrderItemList.Sum(x => x.Return);

            if (sumReceive == 0 && sumReturn == 0 && unOrderedReturn.Count() == 0)
            {
                throw new ArgumentException("Cannot receive 0 items. Either receive items, return items or both. If return, please provide reason.");
            }

            receiveEntry = new ReceiveOrder
            {
                PurchaseOrderID = outstandingPOSelected,
                ReceiveDate = DateTime.Now,
                EmployeeID = receiverEmployeeID
            };
            _context.ReceiveOrders.Add(receiveEntry);

            if (unOrderedReturn.Count() > 0)
            {
                foreach (var item in unOrderedReturn)
                {
                    returnItems = new ReturnedOrderDetail
                    {
                        ReceiveOrderID = receiveEntry.ReceiveOrderID,
                        PurchaseOrderDetailID = null,  // this could be null
                        ItemDescription = item.ItemName,
                        Quantity = item.Quantity,
                        Reason = "Not Ordered",
                        VendorStockNumber = item.VendorProductID
                    };
                    receiveEntry.ReturnedOrderDetails.Add(returnItems);
                }
            }
            foreach (var stockItem in purchaseOrderItemList)
            {
                var checkSIDSame = _context.PurchaseOrderDetails
                                .Where(x => x.PurchaseOrderID == outstandingPOSelected && x.StockItemID == stockItem.StockItemID)
                                .Any();

                if (checkSIDSame)
                {
                    var matchPODetailID = _context.PurchaseOrderDetails
                                        .Where(x => x.PurchaseOrderID == outstandingPOSelected && x.StockItemID == stockItem.StockItemID)
                                        .Single();

                    if (stockItem.Receive < 0)
                    {
                        errorList.Add(new Exception($"For item {stockItem.StockItemID}, Quantity Receive cannot be negative value."));
                    }

                    if (stockItem.Receive > 0)
                    {
                        receiveOrderDetailEntry = new ReceiveOrderDetail
                        {
                            ReceiveOrderID = receiveEntry.ReceiveOrderID,
                            PurchaseOrderDetailID = matchPODetailID.PurchaseOrderDetailID,
                            QuantityReceived = stockItem.Receive
                        };
                        receiveEntry.ReceiveOrderDetails.Add(receiveOrderDetailEntry);

                        var stockAdjusted = _context.StockItems
                                            .Where(x => x.StockItemID == stockItem.StockItemID)
                                            .Single();
                        if (stockItem.Receive > stockAdjusted.QuantityOnOrder)
                        {
                            errorList.Add(new Exception($"For item {stockItem.StockItemID}, Quantity Receive cannot be greater than Quantity Outstanding."));
                        }
                        else
                        {
                            stockAdjusted.QuantityOnHand = stockAdjusted.QuantityOnHand + stockItem.Receive;
                            stockAdjusted.QuantityOnOrder = stockAdjusted.QuantityOnOrder - stockItem.Receive;
                            _context.Update(stockAdjusted);
                        }

                    }
                    if (stockItem.Return < 0)
                    {
                        errorList.Add(new Exception($"For item {stockItem.StockItemID}, Quantity Return cannot be negative value."));
                    }
                    if (stockItem.Return > 0)
                    {
                        if (stockItem.Reason == null)
                        {
                            errorList.Add(new Exception($"Please give return reason for Item ID {stockItem.StockItemID}"));
                        }

                        var stockAdjusted = _context.StockItems
                            .Where(x => x.StockItemID == stockItem.StockItemID)
                            .Single();
                        if (stockItem.Return > stockAdjusted.QuantityOnOrder)
                        {
                            errorList.Add(new Exception($"For item {stockItem.StockItemID}, Quantity Return cannot be greater than Quantity Outstanding."));
                        }
                        else
                        {
                            returnItems = new ReturnedOrderDetail
                            {
                                ReceiveOrderID = receiveEntry.ReceiveOrderID,
                                PurchaseOrderDetailID = matchPODetailID.PurchaseOrderDetailID,
                                // itemDescription in database for returned ordered item is null but it can be the description of stockItem 
                                ItemDescription = null,
                                //ItemDescription = stockItem.Description,
                                Quantity = stockItem.Return,
                                Reason = stockItem.Reason,
                                VendorStockNumber = null
                            };
                            receiveEntry.ReturnedOrderDetails.Add(returnItems);
                        }
                    }
                }

            }

            if (errorList.Count() > 0)
            {
                throw new AggregateException("Unable to receive order.  Check concerns", errorList);
            }
            else
            {
                _context.SaveChanges();
                foreach (var stockItem in purchaseOrderItemList)
                {
                    var checkOutstandingQtyEqualToZero = _context.PurchaseOrderDetails
                    .Where(x => x.PurchaseOrderID == outstandingPOSelected && x.StockItemID == stockItem.StockItemID
                    && x.StockItem.QuantityOnOrder == 0)
                    .Any();
                    if (checkOutstandingQtyEqualToZero)
                    {
                        count++;
                    }
                }
                var countItemIDs = _context.PurchaseOrderDetails
                        .Where(x => x.PurchaseOrderID == outstandingPOSelected)
                        .Select(x => x.StockItemID).Count();
                if (count == countItemIDs)
                {
                    var selectedPurchaseOrder = _context.PurchaseOrders.Where(x => x.PurchaseOrderID == outstandingPOSelected)
                                        .Single();
                    selectedPurchaseOrder.Closed = true;
                    _context.Update(selectedPurchaseOrder);
                }
                _context.SaveChanges();
                if (unOrderedReturn.Count() > 0)
                {
                    foreach (var item in unOrderedReturn)
                    {
                        _context.UnOrderedItems.Remove(item);
                    }
                    _context.SaveChanges();
                }

            }

        }
        #endregion



        #region Insert UnorderedItems
        public void Insert_UnorderedItem(List<UnOrderedItemsList> unOrderedItemList)
        {
            UnOrderedItem items = null;
            List<Exception> errorList = new List<Exception>();

            foreach (var item in unOrderedItemList)
            {
                if (item.Description == null)
                {
                    errorList.Add(new Exception("Item Description cannot be null"));
                }
                if (item.VSN == null || item.VSN.Length > 15)
                {
                    errorList.Add(new Exception("VSN cannot be null and maximum character can be 15 only."));
                }
                if (item.Quantity <= 0)
                {
                    errorList.Add(new Exception("Quantity cannot be 0 or negative. Please use positive integers"));
                }

                if (errorList.Count() > 0)
                {
                    throw new AggregateException("Unable to add return Item to return order.  Check concerns", errorList);
                }
                else
                {
                    items = new UnOrderedItem
                    {
                        ItemName = item.Description,
                        VendorProductID = item.VSN,
                        Quantity = item.Quantity
                    };

                    _context.UnOrderedItems.Add(items);
                    _context.SaveChanges();
                }

            }


        }
        #endregion



        #region Remove Unordered Items

        public void Remove_UnorderedItem(int CID)
        {
            var selectedUnorderdItem = _context.UnOrderedItems
                                        .Where(x => x.ItemID == CID)
                                        .Single();
            _context.UnOrderedItems.Remove(selectedUnorderdItem);
            _context.SaveChanges();

        }

        #endregion


        #region Force Close
        public void ForceCloseService(int outstandingPOSelected, string forceCloseReason)
        {

            if (string.IsNullOrWhiteSpace(forceCloseReason))
            {
                throw new ArgumentNullException("Please give a reason for closing the purchase order.");
            }
            else
            {
                var selectedPurchaseOrder = _context.PurchaseOrders.Where(x => x.PurchaseOrderID == outstandingPOSelected)
                                            .Single();
                selectedPurchaseOrder.Closed = true;
                selectedPurchaseOrder.Notes = forceCloseReason;
                _context.Update(selectedPurchaseOrder);

                var stockItems = _context.PurchaseOrderDetails
                                .Where(x => x.PurchaseOrderID == outstandingPOSelected)
                                .Select(x => x.StockItem)
                                .ToList();
                foreach (var item in stockItems)
                {
                    item.QuantityOnOrder = 0;
                }

                _context.SaveChanges();
            }


        }

        #endregion



    }
}
