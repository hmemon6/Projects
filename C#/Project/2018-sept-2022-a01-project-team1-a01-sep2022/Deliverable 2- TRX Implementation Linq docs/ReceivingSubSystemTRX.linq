<Query Kind="Program">
  <Connection>
    <ID>47f4fd96-c03b-40e4-ada7-8c00d0f36ea7</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>.</Server>
    <Database>eTools</Database>
    <DisplayName>eTools-Entity</DisplayName>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

/// <summary>
///  
/// 
/// 
///   Name: Haseeb Memon
///   Section: A01
///   SubSection: Receiving
/// 
/// 
/// 
/// </summary>



void Main()
{
	try
	{
		// This will be binded to the ID selected on the razor page for outstandingpurchaseOrder and the page will redirect to a new page which will show the details of the 
		//outstandingPurchaseID --> Return RedirectToPage( new { outsrandingPOSelected})
		int outstandingPOSelected = 358;

		// This will be bound to the authorized individual who logged in.
		int receiverEmployeeID = 4;

		// Display purchase orders that are not closed yet  uncomment below line to see the purchaseorder that is not closed
		//DisplayOutstandingPO().Dump();

		//2nd step, once the OutstandingpurchaseID is selected PostRedirect to a differnet page showing OutstandingPO details (this page will be only accessible if clicked Select)
		// Uncomment below to see the selected purcahseOrder details
		//DisplayOutstandingPODetails(outstandingPOSelected).Dump();


		#region Test Data


		#region Validation tests

		//1) cannot receive nothing, must have receive input || return input with reason

		//Uncomment below to see the validation (1)

		//List<SelectedPurchaseOrderDetails> purchaseOrderItemList = new List<SelectedPurchaseOrderDetails>();
		//purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//{
		//	StockItemID = 34,
		//	Receive = 0,
		//	Reason = null,
		//	Return = 0
		//});
		//purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//{
		//	StockItemID = 5566,
		//	Receive = 0,
		//	Reason = null,
		//	Return = 0
		//});
		//purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//{
		//	StockItemID = 5567,
		//	Receive = 0,
		//	Reason = null,
		//	Return = 0
		//});
		//purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//{
		//	StockItemID = 5572,
		//	Receive = 0,
		//	Reason = "Just Coz",
		//	Return = 0
		//});

		//RecievePOService(purchaseOrderItemList, outstandingPOSelected, receiverEmployeeID);

		//2) Cannot receive or return more than QuantityOutstanding

		//		List<SelectedPurchaseOrderDetails> purchaseOrderItemList = new List<SelectedPurchaseOrderDetails>();
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 34,
		//			Receive = 1000,
		//			Reason = "Broken",
		//			Return = 0
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5566,
		//			Receive = 0,
		//			Reason = "trash",
		//			Return = 1000
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5567,
		//			Receive = 1000,
		//			Reason = "1 receive, 1 return",
		//			Return = 1
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5572,
		//			Receive = 1000,
		//			Reason = "Just Coz",
		//			Return = 1000
		//		});
		//
		//		RecievePOService(purchaseOrderItemList, outstandingPOSelected, receiverEmployeeID);


		//3) Cannot return without reason + cannot use regatives for recive || return

		//		List<SelectedPurchaseOrderDetails> purchaseOrderItemList = new List<SelectedPurchaseOrderDetails>();
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 34,
		//			Receive = -1,
		//			Reason = "Broken",
		//			Return = 0
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5566,
		//			Receive = 0,
		//			Reason = null,
		//			Return = -1
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5567,
		//			Receive = 1,
		//			Reason = "1 receive, 1 return",
		//			Return = 1
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5572,
		//			Receive = 0,
		//			Reason = null,
		//			Return = 1
		//		});
		//
		//		RecievePOService(purchaseOrderItemList, outstandingPOSelected, receiverEmployeeID);



		#endregion


		#region Partial Receieve AND Return


		//		List<SelectedPurchaseOrderDetails> purchaseOrderItemList = new List<SelectedPurchaseOrderDetails>();
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 34,
		//			Receive = 0,
		//			Reason = "Broken",
		//			Return = 0
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5566,
		//			Receive = 0,
		//			Reason = "trash",
		//			Return = 0
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5567,
		//			Receive = 1,
		//			Reason = "1 receive, 1 return",
		//			Return = 1
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5572,
		//			Receive = 0,
		//			Reason = "Just Coz",
		//			Return = 0
		//		});
		//
		//		RecievePOService(purchaseOrderItemList, outstandingPOSelected, receiverEmployeeID);
		//
		//		var stockItem = PurchaseOrderDetails
		//			.Where(x => x.PurchaseOrderID == 358)
		//			.Select(x => x)
		//			.ToList();
		//
		//		stockItem.Dump();  // Please click on ReceiveOrderDetails and ReturnOrderDetails for the ID that was received and returned, You can test it yourself by adjusting above values
		//							// in the list
		//		DisplayOutstandingPO().Dump();  // PO 358 is not closing on partial receive --> GOOD // Sequence contain no element

		#endregion


		#region Full Receive

		// Please note: For this delete database and recreate with fresh values. PO 358 needs to have QtyOutStanding original values for full receive to work
		// OR
		// If you clicked play once in previous simulation then use the following values below with Receive 143 instead of 144 in itemID  5567

		//		List<SelectedPurchaseOrderDetails> purchaseOrderItemList = new List<SelectedPurchaseOrderDetails>();
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 34,
		//			Receive = 10,
		//			Reason = "Broken",
		//			Return = 0
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5566,
		//			Receive = 15,
		//			Reason = "trash",
		//			Return = 0
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5567,
		//			Receive = 144,
		//			Reason = "1 receive, 1 return",
		//			Return = 0
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5572,
		//			Receive = 71,
		//			Reason = "Just Coz",
		//			Return = 0
		//		});
		//
		//		RecievePOService(purchaseOrderItemList, outstandingPOSelected, receiverEmployeeID);
		//
		//		var stockItem = PurchaseOrderDetails
		//			.Where(x => x.PurchaseOrderID == 358)
		//			.Select(x => x)
		//			.ToList();
		//
		//		stockItem.Dump();  // Please click on ReceiveOrderDetails and ReturnOrderDetails for the ID that was received and returned, You can test it yourself by adjusting above values
		//						   // in the list
		//		DisplayOutstandingPO().Dump();  // PO 358 is not showing up  meaning its closed on full receive --> GOOD
		//		PurchaseOrders
		//			.Select(x => x).Dump(); // THe attribute CLosed should show True for PO 358

		#endregion

		#region Full Return

		// Please note: For this delete database and recreate with fresh values. PO 358 needs to have QtyOutStanding original values for full return to work

		//		List<SelectedPurchaseOrderDetails> purchaseOrderItemList = new List<SelectedPurchaseOrderDetails>();
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 34,
		//			Receive = 0,
		//			Reason = "Broken",
		//			Return = 10
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5566,
		//			Receive = 0,
		//			Reason = "OverAge",
		//			Return = 15
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5567,
		//			Receive = 0,
		//			Reason = "Dont Want",
		//			Return = 144
		//		});
		//		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		//		{
		//			StockItemID = 5572,
		//			Receive = 0,
		//			Reason = "Damaged",
		//			Return = 71
		//		});
		//
		//		RecievePOService(purchaseOrderItemList, outstandingPOSelected, receiverEmployeeID);
		//
		//		var stockItem = PurchaseOrderDetails
		//			.Where(x => x.PurchaseOrderID == 358)
		//			.Select(x => x)
		//			.ToList();
		//
		//		stockItem.Dump();  // Please click on ReceiveOrderDetails and ReturnOrderDetails for the ID that was received and returned, You can test it yourself by adjusting above values
		//						   // in the list
		//		DisplayOutstandingPO().Dump();  // PO 358 is showing up  meaning its not closed on full return --> GOOD
		//		PurchaseOrders
		//			.Select(x => x).Dump(); // THe attribute CLosed should show True for PO 358

		#endregion

		#region Force Close
		// Can close any PO, please use PO 358 as it is set already. If you have already closed in previous tests. You will need to recreate database to have PO 358 opened

		//Force Close Validation --> reason is required, uncomment below line to execute validation
		//ForceCloseService(outstandingPOSelected, null);

		// Force Close -- Uncomment Below 2 lines to force close PO 358
		//string forceCloseReason = "Vendor went bankrupt. Cannot order more items";
		//ForceCloseService(outstandingPOSelected, forceCloseReason);

		// To see if the purchase order was force closed
		//PurchaseOrders.Select(x => x).Dump();

		#endregion

		#region Insert UnOrderedItem

		// Make a list of items when the insert button is clicked and append it to the table UnOrderedItems
		List<UnOrderedItemsList> unOrderedItemList = new List<UnOrderedItemsList>();
		unOrderedItemList.Add(new UnOrderedItemsList()
		{
			Description = "Test Item",
			Quantity = 25,
			VSN = "Vendor test"
		});

		//Insert_UnorderedItem(unOrderedItemList);
		//UnOrderedItems.Select(x=>x).Dump();
		

		#endregion

		#region Remove UnOrderedItem
		// this will be bound to the selected CID picked from unOrderedItem list table
		
		//UnOrderedItems.Select(x=>x).Dump();
		int removeItem = 1002; // chose the ID that your database showed on the table from insert in order to remove the ID from UnOrderedTable
		//Remove_UnorderedItem(removeItem);
		//DisplayOutstandingPO Unordered table 
		//UnOrderedItems.Select(x=>x).Dump();
		#endregion
		
		#endregion

	}
	catch (AggregateException ex)
	{
		foreach (var error in ex.InnerExceptions)
		{
			error.Message.Dump();
		}

	}
	catch (ArgumentNullException ex)
	{
		GetInnerException(ex).Message.Dump();
	}

	catch (Exception ex)
	{
		GetInnerException(ex).Message.Dump();
	}

}

#region ViewModels
public class PurchaseOrderList
{
	public int PurchaseOrderID { get; set; }
	public DateTime? OrderDate { get; set; }
	public string Vendor { get; set; }
	public string Phone { get; set; }
}

public class SelectedPurchaseOrderDetails
{
	public int StockItemID { get; set; }
	public string Description { get; set; }
	public int QuanitiyOrdered { get; set; }
	public int QuanitiyOutStanding { get; set; }
	public int Receive { get; set; }
	public int Return { get; set; }
	public string Reason { get; set; }
}

public class UnOrderedItemsList
{
	public int CID { get; set; }
	public string VSN { get; set; }
	public string Description { get; set; }
	public int Quantity { get; set; }
}

#endregion

#region Methods
private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}
#endregion

#region Query
public List<PurchaseOrderList> DisplayOutstandingPO()
{
	IEnumerable<PurchaseOrderList> purchaseOrders = PurchaseOrders
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
	IEnumerable<SelectedPurchaseOrderDetails> purchaseOrderDetails = PurchaseOrderDetails
		.Where(x => x.PurchaseOrderID == OutstandingPOID)
		.Select(x => new SelectedPurchaseOrderDetails
		{
			StockItemID = x.StockItemID,
			Description = x.StockItem.Description,
			QuanitiyOrdered = x.Quantity,
			QuanitiyOutStanding = x.StockItem.QuantityOnOrder,
		});
	return purchaseOrderDetails.ToList();
}

#endregion

#region ONPOST RECEIVE
public void RecievePOService(List<SelectedPurchaseOrderDetails> purchaseOrderItemList, int outstandingPOSelected, int receiverEmployeeID)
{
	ReceiveOrders receiveEntry = null;
	ReceiveOrderDetails receiveOrderDetailEntry = null;
	ReturnedOrderDetails returnItems = null;
	List<Exception> errorList = new List<Exception>();


	int sumReceive = purchaseOrderItemList.Sum(x => x.Receive);
	int sumReturn = purchaseOrderItemList.Sum(x => x.Return);

	if (sumReceive == 0 && sumReturn == 0)
	{
		throw new ArgumentException("Cannot receive 0 items. Either receive items, return items or both. If return, please provide reason.");
	}

	receiveEntry = new ReceiveOrders
	{
		PurchaseOrderID = outstandingPOSelected,
		ReceiveDate = DateTime.Now,
		EmployeeID = receiverEmployeeID
	};
	ReceiveOrders.Add(receiveEntry);
	int count = 0;
	var unOrderedReturn = UnOrderedItems.ToList();
	if (unOrderedReturn.Count() > 0)
	{
		foreach (var item in unOrderedReturn)
		{
			returnItems = new ReturnedOrderDetails
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
		var checkSIDSame = PurchaseOrderDetails
						.Where(x => x.PurchaseOrderID == outstandingPOSelected && x.StockItemID == stockItem.StockItemID)
						.Any();

		if (checkSIDSame)
		{
			var matchPODetailID = PurchaseOrderDetails
								.Where(x => x.PurchaseOrderID == outstandingPOSelected && x.StockItemID == stockItem.StockItemID)
								.Single();

			if (stockItem.Receive < 0)
			{
				errorList.Add(new Exception($"For item {stockItem.StockItemID}, Quantity Receive cannot be negative value."));
			}

			if (stockItem.Receive > 0)
			{
				receiveOrderDetailEntry = new ReceiveOrderDetails
				{
					ReceiveOrderID = receiveEntry.ReceiveOrderID,
					PurchaseOrderDetailID = matchPODetailID.PurchaseOrderDetailID,
					QuantityReceived = stockItem.Receive
				};
				receiveEntry.ReceiveOrderDetails.Add(receiveOrderDetailEntry);

				var stockAdjusted = StockItems
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
					Update(stockAdjusted);
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

				var stockAdjusted = StockItems
					.Where(x => x.StockItemID == stockItem.StockItemID)
					.Single();
				if (stockItem.Return > stockAdjusted.QuantityOnOrder)
				{
					errorList.Add(new Exception($"For item {stockItem.StockItemID}, Quantity Return cannot be greater than Quantity Outstanding."));
				}
				else
				{
					returnItems = new ReturnedOrderDetails
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
		SaveChanges();
		foreach (var stockItem in purchaseOrderItemList)
		{
			var checkOutstandingQtyEqualToZero = PurchaseOrderDetails
			.Where(x => x.PurchaseOrderID == outstandingPOSelected && x.StockItemID == stockItem.StockItemID
			&& x.StockItem.QuantityOnOrder == 0)
			.Any();
			if (checkOutstandingQtyEqualToZero)
			{
				count++;
			}
		}
		var countItemIDs = PurchaseOrderDetails
				.Where(x => x.PurchaseOrderID == outstandingPOSelected)
				.Select(x => x.StockItemID).Count();
		if (count == countItemIDs)
		{
			var selectedPurchaseOrder = PurchaseOrders.Where(x => x.PurchaseOrderID == outstandingPOSelected)
								.Single();
			selectedPurchaseOrder.Closed = true;
			Update(selectedPurchaseOrder);
		}
		SaveChanges();
		if (unOrderedReturn.Count() > 0)
		{
			foreach (var item in unOrderedReturn)
			{
				UnOrderedItems.Remove(item);
			}
			SaveChanges();
		}

	}

}

#endregion

#region ONPOST INSERT
public void Insert_UnorderedItem(List<UnOrderedItemsList> unOrderedItemList)
{
	UnOrderedItems items = null;
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
			items = new UnOrderedItems
			{
				ItemName = item.Description,
				VendorProductID = item.VSN,
				Quantity = item.Quantity
			};

			UnOrderedItems.Add(items);
			SaveChanges();
		}

	}


}

#endregion

#region ONPOST Remove
public void Remove_UnorderedItem(int CID)
{
	var selectedUnorderdItem = UnOrderedItems
								.Where(x => x.ItemID == CID)
								.Single();
	UnOrderedItems.Remove(selectedUnorderdItem);
	SaveChanges();

}
#endregion

#region ONPOST FORCE CLOSE
public void ForceCloseService(int outstandingPOSelected, string forceCloseReason)
{

	if (string.IsNullOrWhiteSpace(forceCloseReason))
	{
		throw new ArgumentNullException("Please give a reason for closing the purchase order.");
	}
	else
	{
		var selectedPurchaseOrder = PurchaseOrders.Where(x => x.PurchaseOrderID == outstandingPOSelected)
									.Single();
		selectedPurchaseOrder.Closed = true;
		selectedPurchaseOrder.Notes = forceCloseReason;
		Update(selectedPurchaseOrder);

		var stockItems = PurchaseOrderDetails
						.Where(x => x.PurchaseOrderID == outstandingPOSelected)
						.Select(x => x.StockItem)
						.ToList();
		foreach (var item in stockItems)
		{
			item.QuantityOnOrder = 0;
		}

		SaveChanges();
	}


}

#endregion






















