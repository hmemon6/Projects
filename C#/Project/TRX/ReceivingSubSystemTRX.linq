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
		//outstandingPurchaseID
		int outstandingPOSelected = 358;
		//outstandingPOSelected = 361;
		//int stockItemID = 0;
		// This will be bound to the authorized individual who logged in.
		int receiverEmployeeID = 4;

		// this will be bound to the selected CID picked from unOrderedItem list table
		int removeItem = 4;

		//1st Step!
		// Display purchase orders that are not closed yet
		DisplayOutstandingPO().Dump();

		//2nd step, once the OutstandingpurchaseID is selected PostRedirect to a differnet page showing OutstandingPO details (this page will be only accessible if clicked Select)
		// This page will go away if the user clicks Receive or ForceClose and the page wil postredirct back to unordered purchase order list display screen)
		DisplayOutstandingPODetails(outstandingPOSelected).Dump();

		List<SelectedPurchaseOrderDetails> purchaseOrderItemList = new List<SelectedPurchaseOrderDetails>();
		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		{
			StockItemID = 34,
			Receive = 0,
			Reason = "Broken",
			Return = 0
		});
		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		{
			StockItemID = 5566,
			Receive = 0,
			Reason = "trash",
			Return = 0
		});
		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		{
			StockItemID = 5567,
			Receive = 1,
			Reason = "1 receive, 1 return",
			Return = 1
		});
		purchaseOrderItemList.Add(new SelectedPurchaseOrderDetails()
		{
			StockItemID = 5572,
			Receive = 0,
			Reason = "Just Coz",
			Return = 0
		});




		// Make a list of items when the insert button is clicked and append it to the table UnOrderedItems

		List<UnOrderedItemsList> unOrderedItemList = new List<UnOrderedItemsList>();
		unOrderedItemList.Add(new UnOrderedItemsList()
		{
			Description = "Test Item",
			Quantity = 25,
			VSN = "Vendor test"
		});
		//unOrderedItemList.Dump();
		//unOrderedItemList.Dump();
		//Insert_UnorderedItem(unOrderedItemList);
		//UnOrderedItems.Select(x=>x).Dump();
		//removeItem = 7;
		//Remove_UnorderedItem(removeItem);
		//UnOrderedItems.Select(x=>x).Dump();
		RecievePOService(purchaseOrderItemList, outstandingPOSelected, receiverEmployeeID);


		// Force Close
		string forceCloseReason = "Vendor went bankrupt. Cannot order more items";
		//ForceCloseService(outstandingPOSelected, forceCloseReason);
		
		// To see if the purchase order was force closed
		//ReceiveOrders.Where(x=>x.PurchaseOrder.Closed == true).Select(x=>x).Dump();

		DisplayOutstandingPODetails(outstandingPOSelected).Dump();
		DisplayOutstandingPO().Dump();
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

private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}

//public void DisplayOutstandingPO()
//{
//	PurchaseOrders
//			.Where(x => x.Closed == false && x.OrderDate != null)
//			.Select(x => new
//			{
//				PurchaseOrderID = x.PurchaseOrderID,
//				OrderDate = x.OrderDate,
//				Vendor = x.Vendor.VendorName,
//				VendorContactPhone = x.Vendor.Phone
//			}).Dump();
//}

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


//public void DisplayOutstandingPODetails(int OutstandingPOID)
//{
//
//	PurchaseOrderDetails
//		.Where(x => x.PurchaseOrderID == OutstandingPOID)
//		.Select(x => new
//		{
//			Vendor = x.PurchaseOrder.Vendor.VendorName,
//			VendorContactPhone = x.PurchaseOrder.Vendor.Phone,
//			StockItemID = x.StockItemID,
//			StockItemDesciption = x.StockItem.Description,
//			QuantityOnOrder = x.Quantity,
//			QuantityOutStanding = x.StockItem.QuantityOnOrder
//
//		})
//		.Dump();
//
//}


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
			Receive = 0,
			Return = 0,
			Reason = null
		});
	return purchaseOrderDetails.ToList();
}




public void RecievePOService(List<SelectedPurchaseOrderDetails> purchaseOrderItemList, int outstandingPOSelected, int receiverEmployeeID)
{
	ReceiveOrders receiveEntry = null;
	ReceiveOrderDetails receiveOrderDetailEntry = null;
	ReturnedOrderDetails returnItems = null;
	List<Exception> errorList = new List<Exception>();

	receiveEntry = new ReceiveOrders
	{
		PurchaseOrderID = outstandingPOSelected,
		ReceiveDate = DateTime.Now,
		EmployeeID = receiverEmployeeID
	};
	ReceiveOrders.Add(receiveEntry);
	//receiveEntry.Dump();
	int count = 0;
	//int purchaseOrderDetailID = 1;
	//int sum = 0;
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
				Reason = "Unordered",
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

			if (stockItem.Return > 0)
			{
				var stockAdjusted = StockItems
					.Where(x => x.StockItemID == stockItem.StockItemID)
					.Single();
				if (stockItem.Return > stockAdjusted.QuantityOnOrder)
				{
					errorList.Add(new Exception($"For item {stockItem.StockItemID}, Quantity Return cannot be greater than Quantity Outstanding on the same p."));
				}
				else if (stockItem.Reason == null)
				{
					errorList.Add(new Exception($"Please give return reason for Item ID {stockItem.StockItemID}"));
				}
				else
				{
					returnItems = new ReturnedOrderDetails
					{
						ReceiveOrderID = receiveEntry.ReceiveOrderID,
						PurchaseOrderDetailID = matchPODetailID.PurchaseOrderDetailID,
						ItemDescription = null,
						Quantity = stockItem.Return,
						Reason = stockItem.Reason,
						VendorStockNumber = null
					};
					receiveEntry.ReturnedOrderDetails.Add(returnItems);
				}
			}

		}

		//		else
		//		{
		//			if (unOrderedReturn.Count() > 0)
		//			{
		//				foreach (var item in unOrderedReturn)
		//				{
		//					returnItems = new ReturnedOrderDetails
		//					{
		//						ReceiveOrderID = receiveEntry.ReceiveOrderID,
		//						PurchaseOrderDetailID = purchaseOrderDetailID,
		//						ItemDescription = item.ItemName,
		//						Quantity = item.Quantity,
		//						Reason = "Unordered",
		//						VendorStockNumber = item.VendorProductID
		//					};
		//					ReturnedOrderDetails.Add(returnItems);
		//					purchaseOrderDetailID++;
		//				}
		//			}
		//			if (stockItem.Receive > 0)
		//			{
		//				receiveOrderDetailEntry = new ReceiveOrderDetails
		//				{
		//					ReceiveOrderID = receiveEntry.ReceiveOrderID,
		//					PurchaseOrderDetailID = purchaseOrderDetailID,
		//					QuantityReceived = stockItem.Receive
		//				};
		//				receiveEntry.ReceiveOrderDetails.Add(receiveOrderDetailEntry);
		//
		//				var stockAdjusted = StockItems
		//									.Where(x => x.StockItemID == stockItem.StockItemID)
		//									.Single();
		//
		//				stockAdjusted.QuantityOnHand = stockAdjusted.QuantityOnHand + stockItem.Receive;
		//				stockAdjusted.QuantityOnOrder = stockAdjusted.QuantityOnOrder - stockItem.Receive;
		//				Update(stockAdjusted);
		//				purchaseOrderDetailID++;
		//
		//			}
		//
		//			if (stockItem.Return > 0)
		//			{
		//
		//				if (stockItem.Reason != null)
		//				{
		//					returnItems = new ReturnedOrderDetails
		//					{
		//						ReceiveOrderID = receiveEntry.ReceiveOrderID,
		//						PurchaseOrderDetailID = purchaseOrderDetailID,
		//						ItemDescription = null,
		//						Quantity = stockItem.Return,
		//						Reason = stockItem.Reason,
		//						VendorStockNumber = null
		//					};
		//					ReturnedOrderDetails.Add(returnItems);
		//				}
		//				else
		//				{
		//					errorList.Add(new Exception($"Please give return reason for Item ID {stockItem.StockItemID}"));
		//				}
		//			}
		//
		//		}

	}

	if (errorList.Count() > 0)
	{
		throw new AggregateException("Unable to receive order.  Check concerns", errorList);
	}
	else
	{
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

public void Remove_UnorderedItem(int CID)
{
	var selectedUnorderdItem = UnOrderedItems
								.Where(x => x.ItemID == CID)
								.Single().Dump();
	UnOrderedItems.Remove(selectedUnorderdItem);
	SaveChanges();

}


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

		var stockItem = PurchaseOrderDetails
						.Where(x => x.PurchaseOrderID == outstandingPOSelected)
						.Select(x => x)
						.ToList();
		foreach (var item in StockItems)
		{
			item.QuantityOnOrder = 0;
		}

		SaveChanges();
	}


}
