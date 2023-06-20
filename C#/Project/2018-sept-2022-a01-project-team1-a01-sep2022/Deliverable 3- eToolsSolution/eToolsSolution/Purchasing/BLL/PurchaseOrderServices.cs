using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Purchasing.DAL;
using Purchasing.Entities;
using Purchasing.ViewModels;

namespace Purchasing.BLL
{
    public class PurchaseOrderServices
    {
        private eTools2021Context _context;

        internal PurchaseOrderServices(eTools2021Context context)
        {
            _context = context;
        }

        public List<AllPurchaseOrders> Get_AllPurchaseOrders()
        {
            IEnumerable<AllPurchaseOrders> results = _context.PurchaseOrders
                                                .OrderBy(x => x.PurchaseOrderID)
                                                .Select(x => new AllPurchaseOrders()
                                                {
													PurchaseOrderID = x.PurchaseOrderID,
													OrderDate = x.OrderDate,
													VendorID = x.VendorID,
													EmployeeID = x.EmployeeID,
													TaxAmount = x.TaxAmount,
													SubTotal = x.SubTotal,
													Closed = x.Closed,
													Notes = x.Notes
												});
            return results.ToList();
        }

        // pulled OnGet(), fills the drop-down selection list
        public List<DisplayVendorSelection> Get_DisplayVendorSelection()
		{
			IEnumerable<DisplayVendorSelection> results = _context.Vendors
												.OrderBy(x => x.VendorName)
												.Select(x => new DisplayVendorSelection()
												{
													VendorID = x.VendorID,
													VendorName = x.VendorName
												});
			return results.ToList();
		}

		public DisplayVendorInfo Get_DisplayVendorInfo(int vendorID)
		{
			DisplayVendorInfo result = _context.Vendors.Select(x => new DisplayVendorInfo()
												{
													VendorID = x.VendorID,
													VendorName = x.VendorName,
													PhoneNumber = x.Phone,
													City = x.City,
													PurchaseOrderID = _context.PurchaseOrders.Where(x => x.VendorID == vendorID && x.OrderDate == null).Select(x => x.PurchaseOrderID).FirstOrDefault(),
													EmployeeID = _context.PurchaseOrders.Where(x => x.VendorID == vendorID && x.OrderDate == null).Select(x => x.EmployeeID).FirstOrDefault()
												}).FirstOrDefault();
			return result;
		}

		// The vendorID is passed OnGet(), aka OnClick, so this is the 1st method activated.
		// HERE is where we generate SuggestedOrder if vendor does not have one already.
		// This MUST come before DisplayStockItems
		public List<DisplayCurrentActiveOrderItems> Get_DisplayCurrentActiveOrderItems(int vendorID)
		{
			List<Exception> errorList = new List<Exception>();

			// 1. valid parameter
			if (vendorID == null || vendorID < 1)
			{
				throw new ArgumentException("Must provide a vendorID.");
			}

			// 2. vendor (table entry) exists
			// *note: using bool + .Any() combination is ONLY for checking if exists, otherwise the null -> then query method works the same.
			bool vendorExists = _context.Vendors.Where(x => x.VendorID == vendorID).Select(x => x).Any();
			if (!vendorExists)
			{
				errorList.Add(new Exception("A vendor with the provided ID does not exist."));
			}

			// BOILERPLATE STEP SEQUENCE: vendorID -> PurchaseOrder (OPEN/Suggested) -> PurchaseOrderDetails -> StockItems
			// 3. check if vendor has OPEN PurchaseOrder -> if not, generate SuggestedOrder
			// rule: OPEN PurchaseOrder is identified by a null OrderDate value
			PurchaseOrder fetchPurchaseOrder = null;
			// checks for any Open PurchaseOrders
			// assumption: a vendor should only have 1 OPEN order existing at a time
			fetchPurchaseOrder = _context.PurchaseOrders.Where(x => x.VendorID == vendorID && x.OrderDate == null).Select(x => x).FirstOrDefault();
			// they have no OPEN PurchaseOrders (existing CurrentActiveOrder): generate SuggestedOrder

			// *NOTE: can't set it to null! gotta actually use new keyword to instantiate List<> functionality first.
			List<DisplayCurrentActiveOrderItems> currentActiveOrderList = new List<DisplayCurrentActiveOrderItems>();
			if (fetchPurchaseOrder == null)
			{
				// generate SuggestedOrder
				// *IMPORTANT TO NOTE: a DisplayCurrentActiveOrder (PurchaseOrder) does NOT have PurchaseID YET, because it is technically
				// a TEMPORARY or Suggested PurchaseOrder. When the Update Button is pressed, THEN these table columns (shared with PurchaseOrder)
				// are added to the database via Vendor.PurchaseOrders, THUS auto-generating a PurchaseOrderID using the IDENTITY function.

				// SuggestedOrder: *NOTE that a SuggestedOrder is a PurchaseOrder without an ID, HOWEVER, once a SuggestedOrder becomes a 
				// CurrentActiveOrder (Official PurchaseOrder w/ an ID), aka it's implemented in the database, THEN we generate a PurchaseOrderDetails for it.
				// This is done in the UPDATE, so we need both the SuggestedOrder info AND Quantity/Price from Details. Perhaps 3 parameters?
				List<StockItem> suggestedItems = _context.StockItems.Where(x => x.QuantityOnHand < x.ReOrderLevel).Select(x => x).ToList();
				// outstanding purchase order quantities not yet received for a specific vendor stock item.

				foreach (StockItem item in suggestedItems)
				{
					DisplayCurrentActiveOrderItems suggestedOrder = new DisplayCurrentActiveOrderItems()
					{
						StockItemID = item.StockItemID,
						Description = item.Description,
						QuantityOnHand = item.QuantityOnHand,
						ReOrderLevel = item.ReOrderLevel,
						QuantityOnOrder = item.QuantityOnOrder,
						QuantityToOrder = item.ReOrderLevel - item.QuantityOnHand,
						PurchasePrice = item.PurchasePrice
					};
					currentActiveOrderList.Add(suggestedOrder);
				}
			}
			// they DO have an OPEN PurchaseOrder, aka a query result is returned
			else
			{
				List<PurchaseOrderDetail> itemsOnOrder = _context.PurchaseOrderDetails.Where(x => x.PurchaseOrderID == fetchPurchaseOrder.PurchaseOrderID).Select(x => x).ToList();
				foreach (PurchaseOrderDetail item in itemsOnOrder)
				{
					StockItem existingItems = _context.StockItems.Where(x => x.StockItemID == item.StockItemID).Select(x => x).FirstOrDefault();
					DisplayCurrentActiveOrderItems currentOrder = new DisplayCurrentActiveOrderItems()
					{
						StockItemID = existingItems.StockItemID,
						Description = existingItems.Description,
						QuantityOnHand = existingItems.QuantityOnHand,
						ReOrderLevel = existingItems.ReOrderLevel,
						QuantityOnOrder = existingItems.QuantityOnOrder,
						// these are CUSTOM, based on PurchaseOrderDetails (e.g. quantity is different, or price is a custom discounted price by vendor)
						// ALSO remember not to CLEAR this data on refresh! Unless it hasn't been updated.
						// Basically, PurchaseOrderDetails.PurchasePrice and StockItems.PurchasePrice are NOT necessarily the same.
						// StockItem price is default value (for suggested order) and PODetails is custom IF it does change.
						QuantityToOrder = item.Quantity,
						PurchasePrice = item.PurchasePrice
					};
					currentActiveOrderList.Add(currentOrder);
				}
			}

			if (errorList.Count > 0)
			{
				throw new AggregateException("Unable to get DisplayCurrentActiveOrder. Check concerns.", errorList);
			}
			else
			{
				// return the queried data
				return currentActiveOrderList;
			}

			// OLD
			//DisplayCurrentActiveOrder displayCurrentActiveOrder = new DisplayCurrentActiveOrder()
			//{
			//	StockItemID = fetchStockItems.StockItemID,
			//	Description = fetchStockItems.Description,
			//	QuantityOnHand = fetchStockItems.QuantityOnHand,
			//	ReOrderLevel = fetchStockItems.ReOrderLevel,
			//	QuantityOnOrder = fetchStockItems.QuantityOnOrder,
			//	QuantityToOrder = PurchaseOrderDetails.Where(x => x.StockItemID == fetchStockItems.StockItemID).Select(x => x.Quantity).FirstOrDefault(),
			//	PurchasePrice = PurchaseOrderDetails.Where(x => x.StockItemID == fetchStockItems.StockItemID).Select(x => x.PurchasePrice).FirstOrDefault()
			//};
		}

		// Comes after CurrentActiveOrder is displayed, we only query StockItems NOT on that list. So we need that list generated first.
		public List<DisplayCurrentActiveOrderItems> Get_DisplayCurrentActiveOrderItemsFiltered(int vendorID, List<DisplayCurrentActiveOrderItems> currentActiveOrderItems)
		{
			List<Exception> errorList = new List<Exception>();

			if (vendorID == null || vendorID < 1)
			{
				throw new ArgumentNullException("Must provide a vendorID.");
			}
			// EDIT: this can be null, to work with the SuggestedOrder ID-less logic
			//if(currentActiveOrderItems == null || currentActiveOrderItems.Count() == 0)
			//{
			//	throw new ArgumentNullException("Must provide a currentActiveOrderItem.");
			//}

			bool vendorExists = _context.Vendors.Where(x => x.VendorID == vendorID).Select(x => x).Any();
			if (!vendorExists)
			{
				errorList.Add(new Exception("A vendor with the provided ID does not exist."));
			}

			// 1. gather all stockitems under vendor
			List<StockItem> allItems = _context.StockItems.Where(x => x.VendorID == vendorID).Select(x => x).ToList();
			List<StockItem> unfilteredStockItems = new List<StockItem>();

			foreach (StockItem filterItem in allItems)
			{
				// 2. for each unfilteredItem, check if it matches ANY of the excludeItems in the CurrentActiveOrder	
				if (!currentActiveOrderItems.Any(x => x.StockItemID == filterItem.StockItemID))
				{
					// if not, then add item to filteredList
					unfilteredStockItems.Add(filterItem);
				}
			}
			// unfilteredStockItems is now filtered (removed all excludeItems: items that appear on CurrentActiveOrder)

			List<DisplayCurrentActiveOrderItems> filteredDisplayStockItems = new List<DisplayCurrentActiveOrderItems>();

			// 3. convert filteredStockItems List into DisplayStockItems
			foreach (StockItem filteredItem in unfilteredStockItems)
			{
				DisplayCurrentActiveOrderItems filteredDisplayStockItem = new DisplayCurrentActiveOrderItems()
				{
					StockItemID = filteredItem.StockItemID,
					Description = filteredItem.Description,
					QuantityOnHand = filteredItem.QuantityOnHand,
					ReOrderLevel = filteredItem.ReOrderLevel,
					QuantityOnOrder = filteredItem.QuantityOnOrder,
					BufferQuantity = filteredItem.QuantityOnHand - filteredItem.ReOrderLevel,
					PurchasePrice = filteredItem.PurchasePrice
				};

				// don't forget to add to list lol
				filteredDisplayStockItems.Add(filteredDisplayStockItem);
			}

			if (errorList.Count > 0)
			{
				throw new AggregateException("Unable to get DisplayStockItems. Check concerns.", errorList);
			}
			else
			{
				return filteredDisplayStockItems;
			}
		}

		//NEW
		public DisplayCurrentActiveOrderItems Get_DisplayCurrentActiveOrderItemsByID(int stockItemID)
		{
			return _context.StockItems
				.Where(x => x.StockItemID == stockItemID)
				.OrderBy(x => x.StockItemID)
				.Select(x => new DisplayCurrentActiveOrderItems()
				{
						StockItemID = x.StockItemID,
						Description = x.Description,
						QuantityOnHand = x.QuantityOnHand,
						ReOrderLevel = x.ReOrderLevel,
						QuantityOnOrder = x.QuantityOnOrder,
						QuantityToOrder = 0,
						BufferQuantity = x.QuantityOnHand - x.ReOrderLevel,
						PurchasePrice = x.PurchasePrice
					}
				).SingleOrDefault();
		}

		//public int Get_PurchaseOrderID(int vendorID)
  //      {
		//	if (vendorID == null || vendorID < 1)
  //          {
  //              throw new ArgumentException("Must select a vendorID.");
  //          }
  //          PurchaseOrder fetchPurchaseOrder = null;
		//	fetchPurchaseOrder = _context.PurchaseOrders.Where(x => x.VendorID == vendorID && x.OrderDate == null).Select(x => x).FirstOrDefault();
		//	return fetchPurchaseOrder.PurchaseOrderID;
		//}

		// instead of checking for original query, as this is populated AFTER the Get_DisplayCurrentActiveOrder() method, we'll use the data from that
		// to calculate these values. This info is a subset of VIEW only data, the Update() function will worry about updating the Quantity x Price = SubTotal
		// when it accesses the PurchaseOrder.
		// EDIT: assumption: we only display the CurrentActiveOrderInfo IF a SuggestedOrder has been updated first, otherwise display nothing.
		public DisplayCurrentActiveOrderInfo Get_DisplayCurrentActiveOrderInfo(int purchaseOrderID)
		{
			List<Exception> errorList = new List<Exception>();

			// assumption changed: cannot display info for empty currentActiveOrder
			// EDIT: if purchaseOrderID is null or 0, we simply display 0 for all values (until it gets updated)
			//if(purchaseOrderID == null || purchaseOrderID < 1)
			//{
			//	throw new ArgumentNullException("Must provide a purchaseOrderID.");
			//}

			DisplayCurrentActiveOrderInfo displayCurrentActiveOrderInfo = new DisplayCurrentActiveOrderInfo();

			bool purchaseOrderExists = _context.PurchaseOrders.Where(x => x.PurchaseOrderID == purchaseOrderID).Select(x => x).Any();
			// if PO doesn't exist, it's a SuggestedOrder so we make an
			// assumption: display all values as 0 as a filler to signify it has not been updated yet.
			// since DisplayCurrentActiveOrderInfo is ONLY used for display, this should not cause any errors.
			if (!purchaseOrderExists)
			{
				displayCurrentActiveOrderInfo.PurchaseOrderID = 0;
				displayCurrentActiveOrderInfo.SubTotal = 0;
				displayCurrentActiveOrderInfo.TaxAmount = 0;
				displayCurrentActiveOrderInfo.Total = 0;
			}
			// otherwise if it does exist, grab that PO
			else
			{
				PurchaseOrder existingPurchaseOrder = _context.PurchaseOrders.Where(x => x.PurchaseOrderID == purchaseOrderID).Select(x => x).FirstOrDefault();
				displayCurrentActiveOrderInfo.PurchaseOrderID = existingPurchaseOrder.PurchaseOrderID;
				displayCurrentActiveOrderInfo.SubTotal = existingPurchaseOrder.SubTotal;
				displayCurrentActiveOrderInfo.TaxAmount = existingPurchaseOrder.TaxAmount;
				displayCurrentActiveOrderInfo.Total = existingPurchaseOrder.SubTotal + existingPurchaseOrder.TaxAmount;
			}

			if (errorList.Count > 0)
			{
				throw new AggregateException("Unable to get DisplayCurrentActiveOrderInfo. Check concerns.", errorList);
			}
			else
			{
				return displayCurrentActiveOrderInfo;
			}
		}

		// Commands
		public void Update_CurrentActiveOrder(int employeeID, int vendorID, int purchaseOrderID, List<DisplayCurrentActiveOrderItems> currentActiveOrderDetails)
		{
			// assumption changed: a currentactiveorder may not have 0 items in an order at a time, as updating with 0 items is possible but saving a suggestedOrder is not (new PurchaseOrderDetails need to be created).

			//#region Error Checking
			List<Exception> errorList = new List<Exception>();

			// check for missing parameters - throw exception instead of adding to errorlist, as proceeding with a missing parameter will create more errors.
			// use < 1 instead of == 0 as this will check for negative integers too.
			// ignore the warning: you still need to check for null purchaseOrderDetailID.
			if (employeeID == null || employeeID < 1)
			{
				throw new ArgumentNullException("Must provide an employeeID.");
			}
			if (vendorID == null || vendorID < 1)
			{
				throw new ArgumentNullException("Must provide a vendorID.");
			}
			// we can have purchaseOrderID == null, thus means it is a SuggestedOrder and does not yet have it's own PurchaseOrderID
			//if(purchaseOrderID == null || purchaseOrderID < 1)
			//{
			//	throw new ArgumentNullException("Must provide a purchaseOrderID.");
			//}
			if (currentActiveOrderDetails == null || currentActiveOrderDetails.Count() == 0)
			{
				throw new ArgumentNullException("Must provide a currentActiveOrderDetail.");
			}

			bool employeeExists = _context.Employees.Where(x => x.EmployeeID == employeeID).Select(x => x).Any();
			if (!employeeExists)
			{
				errorList.Add(new Exception("The provided employee does not exist."));
			}
			//#endregion

			decimal calculatedSubTotal = 0;
			decimal calculatedTaxAmount = 0;

			foreach (DisplayCurrentActiveOrderItems calculateItem in currentActiveOrderDetails)
			{
				calculatedSubTotal = calculatedSubTotal + (calculateItem.PurchasePrice * calculateItem.QuantityToOrder);
			}

			// assumption: Tax amount rate is 5% GST
			calculatedTaxAmount = calculatedSubTotal * (decimal)0.05;

			Vendor matchingVendor = null;
			matchingVendor = _context.Vendors.Where(x => x.VendorID == vendorID).Select(x => x).FirstOrDefault();
			if (matchingVendor == null)
			{
				errorList.Add(new Exception("The provided vendor does not exist."));
			}

			PurchaseOrder purchaseOrder = null;
			purchaseOrder = _context.PurchaseOrders.Where(x => x.PurchaseOrderID == purchaseOrderID).Select(x => x).FirstOrDefault();
			if (purchaseOrder == null)
			{
				// if PurchaseOrder doesn't exist, we need to generate a new one first and THEN add the details to that.
				// *NOTE: we NEED to select the vendor first and then .Add() PurchaseOrder to that otherwise a new identity key won't be generated.
				PurchaseOrder addSuggestedOrder = new PurchaseOrder()
				{
					OrderDate = null,
					VendorID = vendorID,
					EmployeeID = employeeID,
					TaxAmount = calculatedTaxAmount,
					SubTotal = calculatedSubTotal,
					Closed = false,
					Notes = null
				};

				// *IMPORTANT: for our SuggestedOrder (missing PO) logic to work, make sure to add to PurchaseOrders table DIRECTLY!!!
				// DO NOT use matchingVendor.PurchaseOrders.Add()
				_context.PurchaseOrders.Add(addSuggestedOrder);

				// gets the PurchaseOrderID of the newly created PurchaseOrder so we can add PurchaseOrderDetails to that.
				int recentlyAddedPurchaseOrderID = matchingVendor.PurchaseOrders.Select(x => x.PurchaseOrderID).Last();

				// after PurchaseOrder is CREATED then we can update details/add to that purchaseorder
				foreach (DisplayCurrentActiveOrderItems item in currentActiveOrderDetails)
				{
					PurchaseOrderDetail addPurchaseOrderDetails = new PurchaseOrderDetail()
					{
						PurchaseOrderID = recentlyAddedPurchaseOrderID,
						StockItemID = item.StockItemID,
						PurchasePrice = item.PurchasePrice,
						Quantity = item.QuantityToOrder
					};

					PurchaseOrder recentlyCreated = _context.PurchaseOrders.Where(x => x.PurchaseOrderID == recentlyAddedPurchaseOrderID).Select(x => x).FirstOrDefault();
					recentlyCreated.PurchaseOrderDetails.Add(addPurchaseOrderDetails);
				}
			}
			// purchaseOrder exists and we are just updating it
			// THIS IS FOR IF PURCHASE ORDER EXISTS (NOT SUGGESTED ORDER)
			else
			{
				purchaseOrder.TaxAmount = calculatedTaxAmount;
				purchaseOrder.SubTotal = calculatedSubTotal;

				// after PurchaseOrder is updated/exists then we can update details/add to that purchaseorder
				foreach (DisplayCurrentActiveOrderItems item in currentActiveOrderDetails)
				{
					PurchaseOrderDetail updatePurchaseOrderDetails = _context.PurchaseOrderDetails.Where(x => x.PurchaseOrderDetailID == item.PurchaseOrderDetailID).Select(x => x).FirstOrDefault();
					updatePurchaseOrderDetails.PurchasePrice = item.PurchasePrice;
					updatePurchaseOrderDetails.Quantity = item.QuantityToOrder;
				}
			}

			if (errorList.Count > 0)
			{
				throw new AggregateException("Unable to update CurrentActiveOrder. Check concerns.", errorList);
			}
			else
			{
				_context.SaveChanges();
			}
		}

		// This is only AFTER the suggestedOrder has been saved.
		public void Place_CurrentActiveOrder(int purchaseOrderID)
		{
			List<Exception> errorList = new List<Exception>();

			if (purchaseOrderID == null || purchaseOrderID < 1)
			{
				throw new ArgumentException("Must provide a purchaseOrderID.");
			}

			PurchaseOrder existingPurchaseOrder = null;
			existingPurchaseOrder = _context.PurchaseOrders.Where(x => x.PurchaseOrderID == purchaseOrderID).Select(x => x).FirstOrDefault();
			if (existingPurchaseOrder == null)
			{
				errorList.Add(new Exception("Suggested Order must be saved to database using Update Button before it can be placed."));
			}

			// what's the difference between .update() vs change directly?
			existingPurchaseOrder.OrderDate = DateTime.Now;

			// update all StockItems on the PurchaseOrder (with the purchaseOrderID) to reflect that it has been placed
			// *note: for every 1 StockItem there is 1 PurchaseOrderDetail, so it should be a list.
			// *RELATIONSHIPS: A PurchaseOrder can have multiple StockItems on it. And a StockItem can appear on MULTIPLE different PurchaseOrders.
			List<PurchaseOrderDetail> allStockItems = _context.PurchaseOrderDetails.Where(x => x.PurchaseOrderID == purchaseOrderID).Select(x => x).ToList();
			foreach (PurchaseOrderDetail updateStockItem in allStockItems)
			{
				StockItem item = _context.StockItems.Where(x => x.StockItemID == updateStockItem.StockItemID).Select(x => x).FirstOrDefault();
				item.QuantityOnOrder = item.QuantityOnOrder + updateStockItem.Quantity;
				// do we need to do .Update()?
			}

			if (errorList.Count > 0)
			{
				throw new AggregateException("Unable to place CurrentActiveOrder. Check concerns.", errorList);
			}
			else
			{
				_context.SaveChanges();
			}
		}

		public void Delete_CurrentActiveOrder(int purchaseOrderID)
		{
			List<Exception> errorList = new List<Exception>();

            //if (purchaseOrderID == null || purchaseOrderID < 1)
            //{
            //	throw new ArgumentException("Must provide a purchaseOrderID.");
            //}

            //PurchaseOrder existingPurchaseOrder = null;
            PurchaseOrder existingPurchaseOrder = _context.PurchaseOrders.Where(x => x.PurchaseOrderID == purchaseOrderID).Select(x => x).FirstOrDefault();
            //if (existingPurchaseOrder == null)
            //{
            //	errorList.Add(new Exception("The purchaseOrder does not exist."));
            //}
            //else
            //{
            //rule: We cannot alter or delete placed/closed orders.
            if (existingPurchaseOrder.OrderDate != null || existingPurchaseOrder.Closed == true)
				{
					errorList.Add(new Exception("You cannot alter placed or closed orders."));
				}
				else
				{
					// need to ONLY remove if purchase order exists, otherwise previous logic gave us Object Reference errror.
					// we were trying to remove/access a purchase order that didn't exist, but somehow it passed the OrderDate/Closed condition and got through.
					_context.PurchaseOrders.Remove(existingPurchaseOrder);
				}
			//}

			if (errorList.Count > 0)
			{
				throw new AggregateException("Unable to delete CurrentActiveOrder. Check concerns.", errorList);
			}
			else
			{
				_context.SaveChanges();
			}
		}
	}
}
