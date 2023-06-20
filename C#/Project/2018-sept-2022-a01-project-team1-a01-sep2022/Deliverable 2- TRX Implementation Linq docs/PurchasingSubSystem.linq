<Query Kind="Program">
  <Connection>
    <ID>285f9550-adcb-4d26-8072-fee1089f5b28</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>.</Server>
    <Database>eTools2021</Database>
    <DisplayName>eTools2021-Entity</DisplayName>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
// Name - Tom Phong
// Section - A01
// Subsystem - Purchasing
	try
	{
		// driver code
		Get_DisplayVendorSelection().Dump();		
		Vendors.Select(x => x).Dump();
		
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		// Good Cases - Scenario 1 (ExistingOrder):
		int goodVendorID = 2;
		int goodPurchaseOrderID = 360;
		int goodEmployeeID = 8; // store manager
		List<CurrentActiveOrderDetails> goodActiveOrderDetailsList = new List<CurrentActiveOrderDetails>();
		CurrentActiveOrderDetails goodActiveOrderDetails = new CurrentActiveOrderDetails()
		{
			PurchaseOrderDetailID = 40,
  			PurchaseOrderID = 360,
    		StockItemID = 5587,
    		PurchasePrice = 160, // old was 154.5600
    		Quantity = 10 // old was 9
		};
		goodActiveOrderDetailsList.Add(goodActiveOrderDetails);
		
		// Queries:
		Get_DisplayVendorInfo(goodVendorID).Dump();
		List<DisplayCurrentActiveOrder> goodOrder = Get_DisplayCurrentActiveOrder(goodVendorID);
		goodOrder.Dump();
		Get_DisplayStockItems(goodVendorID, goodOrder).Dump();
		Get_DisplayCurrentActiveOrderInfo(goodPurchaseOrderID).Dump();
			
		// Transactions:
		Update_CurrentActiveOrder(goodEmployeeID, goodVendorID, goodPurchaseOrderID, goodActiveOrderDetailsList);
		//Place_CurrentActiveOrder(goodPurchaseOrderID);
		//Delete_CurrentActiveOrder(359);
		
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
//		// Good Cases - Scenario 2 (SuggestedOrder):
//		int goodVendorID2 = 1;
//		int goodPurchaseOrderID2 = 0;
//		int goodEmployeeID2 = 8; // store manager
//		List<CurrentActiveOrderDetails> goodActiveOrderDetailsList2 = new List<CurrentActiveOrderDetails>();
//		CurrentActiveOrderDetails goodActiveOrderDetails2 = new CurrentActiveOrderDetails()
//		{
//			PurchaseOrderDetailID = 0,
//  			PurchaseOrderID = 0,
//    		StockItemID = 5587, 
//    		PurchasePrice = 160, // old was 154.5600
//    		Quantity = 10 // old was 9
//		};
//		goodActiveOrderDetailsList2.Add(goodActiveOrderDetails2);
		
		//// Queries:
		//Get_DisplayVendorInfo(goodVendorID2).Dump();
		//List<DisplayCurrentActiveOrder> goodOrder2 = Get_DisplayCurrentActiveOrder(goodVendorID2);
		//goodOrder2.Dump();
		//Get_DisplayStockItems(goodVendorID2, goodOrder2).Dump();
		//Get_DisplayCurrentActiveOrderInfo(goodPurchaseOrderID2).Dump();
		
		//// Transactions:
		//Update_CurrentActiveOrder(goodEmployeeID2, goodVendorID2, goodPurchaseOrderID2, goodActiveOrderDetailsList2);
		//Place_CurrentActiveOrder(PurchaseOrders.Select(x => x.PurchaseOrderID).Last());
		
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
//		// Bad Cases - Parameters:
//		int badVendorID = 1;
//		int badPurchaseOrderID = 0;
//		int badEmployeeID = 0;
//		List<CurrentActiveOrderDetails> badActiveOrderDetailsList = new List<CurrentActiveOrderDetails>();
//		CurrentActiveOrderDetails badActiveOrderDetails = new CurrentActiveOrderDetails()
//		{
//			PurchaseOrderDetailID = 0,
//  			PurchaseOrderID = 0,
//    		StockItemID = 0, 
//    		PurchasePrice = 0,
//    		Quantity = 0
//		};
//		badActiveOrderDetailsList.Add(badActiveOrderDetails);
//		
//		// Queries:
//		Get_DisplayVendorInfo(badVendorID).Dump();
//		List<DisplayCurrentActiveOrder> badOrder = Get_DisplayCurrentActiveOrder(badVendorID);
//		badOrder.Dump();
//		Get_DisplayStockItems(badVendorID, badOrder).Dump();
//		Get_DisplayCurrentActiveOrderInfo(badPurchaseOrderID).Dump();
//		
		// Transactions:
		//Update_CurrentActiveOrder(badEmployeeID, badVendorID, badPurchaseOrderID, badActiveOrderDetailsList);
		//Place_CurrentActiveOrder(badPurchaseOrderID);
		//Delete_CurrentActiveOrder(400); // doesn't exist
	}
	catch (ArgumentNullException ex)
	{ 
		GetInnerException(ex).Message.Dump();
	}
	catch (ArgumentException ex)
	{
		GetInnerException(ex).Message.Dump();
	}
	catch (AggregateException ex)
	{
		foreach (var error in ex.InnerExceptions)
		{
			error.Message.Dump();
		}
	}
	catch (Exception ex)
	{
		GetInnerException(ex).Message.Dump();
	}
}

private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}

// Query Models
// pulled OnGet(), fills the drop-down selection list
public List<DisplayVendorSelection> Get_DisplayVendorSelection()
{
	IEnumerable<DisplayVendorSelection> results = Vendors
										.OrderBy(x => x.VendorName)
										.Select(x => new DisplayVendorSelection()
										{
											VendorID = x.VendorID,
											VendorName = x.VendorName
										});
	return results.ToList();
}

public class DisplayVendorSelection
{
    public int VendorID { get; set; }
    public string VendorName { get; set; }
}	

public class DisplayVendorInfo
{
    public int VendorID { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public int PurchaseOrderNumber { get; set; }
}

public class DisplayCurrentActiveOrder
{
    public int StockItemID { get; set; }
    public string Description { get; set; }
    public int QuantityOnHand { get; set; }
    public int ReOrderLevel { get; set; }
    public int QuantityOnOrder { get; set; }
    public int QuantityToOrder { get; set; }
    public decimal PurchasePrice { get; set; }
}

// see if this gets used, otherwise merge with DisplayCurrentActiveOrder
public class DisplayStockItems
{
    public int StockItemID { get; set; }
    public string Description { get; set; }
    public int QuantityOnHand { get; set; }
    public int ReOrderLevel { get; set; }
    public int QuantityOnOrder { get; set; }
    public int BufferQuantity { get; set; }
    public decimal PurchasePrice { get; set; }
}

public class DisplayCurrentActiveOrderInfo
{
    public int PurchaseOrderID { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal Total { get; set; }
}

// Command Models
public class CurrentActiveOrderDetails
{
    public int PurchaseOrderDetailID { get; set; }
    public int PurchaseOrderID { get; set; }
    public int StockItemID { get; set; }
    public decimal PurchasePrice { get; set; }
    public int Quantity { get; set; }
}

// Transactional Methods

// Queries
public DisplayVendorInfo Get_DisplayVendorInfo(int vendorID)
{
	List<Exception> errorList = new List<Exception>();
	
	// 1. valid parameter
	if(vendorID == null || vendorID < 1)
	{
		throw new ArgumentException("Must provide a vendorID.");
	}
	
	// 2. vendor (table entry) exists
	// *note: using bool + .Any() combination is ONLY for checking if exists, otherwise the null -> then query method works the same.
	bool vendorExists = Vendors.Where(x => x.VendorID == vendorID).Select(x => x).Any();
	if(!vendorExists)
	{
		errorList.Add(new Exception("A vendor with the provided ID does not exist."));
	}

	Vendors fetchVendorInfo = null;
	fetchVendorInfo = Vendors.Where(x => x.VendorID == vendorID).Select(x => x).FirstOrDefault();

	// as we are fetching from an existing (and valid!) table, and returning our custom data model, no need to set as null first -> error check.
	// if we didn't use 'new' upon creation, we'd have to assign each property 1 by 1.
	DisplayVendorInfo displayVendorInfo = new DisplayVendorInfo()
	{
		VendorID = fetchVendorInfo.VendorID,
		PhoneNumber = fetchVendorInfo.Phone,
		City = fetchVendorInfo.City,
		// business rule: a vendor can only have 1 CurrentActiveOrder at one time, this is identified with an unfulfilled OrderDate (open order)
		// no need to check the 'Closed' property as it does not signify a vendor's unique Active Order
		// the hierarchy is OrderDate > Closed, thus a closed property should not be 'TRUE' unless OrderDate is not null.
		PurchaseOrderNumber = PurchaseOrders.Where(x => x.VendorID == fetchVendorInfo.VendorID && x.OrderDate == null).Select(x => x.PurchaseOrderID).FirstOrDefault()
	};
	
	if(errorList.Count > 0)
	{
		throw new AggregateException("Unable to get DisplayVendorInfo. Check concerns.", errorList);
	}
	else
	{
		return displayVendorInfo;
	}
}

// The vendorID is passed OnGet(), aka OnClick, so this is the 1st method activated.
// HERE is where we generate SuggestedOrder if vendor does not have one already.
// This MUST come before DisplayStockItems
public List<DisplayCurrentActiveOrder> Get_DisplayCurrentActiveOrder(int vendorID)
{
	List<Exception> errorList = new List<Exception>();
	
	// 1. valid parameter
	if(vendorID == null || vendorID < 1)
	{
		throw new ArgumentException("Must provide a vendorID.");
	}
	
	// 2. vendor (table entry) exists
	// *note: using bool + .Any() combination is ONLY for checking if exists, otherwise the null -> then query method works the same.
	bool vendorExists = Vendors.Where(x => x.VendorID == vendorID).Select(x => x).Any();
	if(!vendorExists)
	{
		errorList.Add(new Exception("A vendor with the provided ID does not exist."));
	}
	
	// BOILERPLATE STEP SEQUENCE: vendorID -> PurchaseOrder (OPEN/Suggested) -> PurchaseOrderDetails -> StockItems
	// 3. check if vendor has OPEN PurchaseOrder -> if not, generate SuggestedOrder
	// rule: OPEN PurchaseOrder is identified by a null OrderDate value
	PurchaseOrders fetchPurchaseOrder = null;
	// checks for any Open PurchaseOrders
	// assumption: a vendor should only have 1 OPEN order existing at a time
	fetchPurchaseOrder = PurchaseOrders.Where(x => x.VendorID == vendorID && x.OrderDate == null).Select(x => x).FirstOrDefault();
	// they have no OPEN PurchaseOrders (existing CurrentActiveOrder): generate SuggestedOrder
	
	// *NOTE: can't set it to null! gotta actually use new keyword to instantiate List<> functionality first.
	List<DisplayCurrentActiveOrder> currentActiveOrderList = new List<DisplayCurrentActiveOrder>();
	if(fetchPurchaseOrder == null)
	{
		// generate SuggestedOrder
		// *IMPORTANT TO NOTE: a DisplayCurrentActiveOrder (PurchaseOrder) does NOT have PurchaseID YET, because it is technically
		// a TEMPORARY or Suggested PurchaseOrder. When the Update Button is pressed, THEN these table columns (shared with PurchaseOrder)
		// are added to the database via Vendor.PurchaseOrders, THUS auto-generating a PurchaseOrderID using the IDENTITY function.
		
		// SuggestedOrder: *NOTE that a SuggestedOrder is a PurchaseOrder without an ID, HOWEVER, once a SuggestedOrder becomes a 
		// CurrentActiveOrder (Official PurchaseOrder w/ an ID), aka it's implemented in the database, THEN we generate a PurchaseOrderDetails for it.
		// This is done in the UPDATE, so we need both the SuggestedOrder info AND Quantity/Price from Details. Perhaps 3 parameters?
		List<StockItems> suggestedItems = StockItems.Where(x => x.QuantityOnHand < x.ReOrderLevel).Select(x => x).ToList();
		// outstanding purchase order quantities not yet received for a specific vendor stock item.
		
		foreach(StockItems item in suggestedItems)
		{
			DisplayCurrentActiveOrder suggestedOrder = new DisplayCurrentActiveOrder()
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
		List<PurchaseOrderDetails> itemsOnOrder = PurchaseOrderDetails.Where(x => x.PurchaseOrderID == fetchPurchaseOrder.PurchaseOrderID).Select(x => x).ToList();
		foreach(PurchaseOrderDetails item in itemsOnOrder)
		{
			StockItems existingItems = StockItems.Where(x => x.StockItemID == item.StockItemID).Select(x => x).FirstOrDefault();
			DisplayCurrentActiveOrder currentOrder = new DisplayCurrentActiveOrder()
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
	
	if(errorList.Count > 0)
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
public List<DisplayStockItems> Get_DisplayStockItems(int vendorID, List<DisplayCurrentActiveOrder> currentActiveOrderItems)
{
	List<Exception> errorList = new List<Exception>();

	if(vendorID == null || vendorID < 1)
	{
		throw new ArgumentNullException("Must provide a vendorID.");
	}
	// EDIT: this can be null, to work with the SuggestedOrder ID-less logic
	//if(currentActiveOrderItems == null || currentActiveOrderItems.Count() == 0)
	//{
	//	throw new ArgumentNullException("Must provide a currentActiveOrderItem.");
	//}

	bool vendorExists = Vendors.Where(x => x.VendorID == vendorID).Select(x => x).Any();
	if(!vendorExists)
	{
		errorList.Add(new Exception("A vendor with the provided ID does not exist."));
	}
	
	// 1. gather all stockitems under vendor
	List<StockItems> allItems = StockItems.Where(x => x.VendorID == vendorID).Select(x => x).ToList();
	List<StockItems> unfilteredStockItems = new List<StockItems>();
	
	foreach(StockItems filterItem in allItems)
	{
		// 2. for each unfilteredItem, check if it matches ANY of the excludeItems in the CurrentActiveOrder	
		if(!currentActiveOrderItems.Any(x => x.StockItemID == filterItem.StockItemID))
		{
			// if not, then add item to filteredList
			unfilteredStockItems.Add(filterItem);				
		}
	}
	// unfilteredStockItems is now filtered (removed all excludeItems: items that appear on CurrentActiveOrder)

	List<DisplayStockItems> filteredDisplayStockItems = new List<DisplayStockItems>();
	
	// 3. convert filteredStockItems List into DisplayStockItems
	foreach(StockItems filteredItem in unfilteredStockItems)
	{
		DisplayStockItems filteredDisplayStockItem = new DisplayStockItems()
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
	
	if(errorList.Count > 0)
	{
		throw new AggregateException("Unable to get DisplayStockItems. Check concerns.", errorList);
	}
	else
	{
		return filteredDisplayStockItems;
	}
}

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
	
	bool purchaseOrderExists = PurchaseOrders.Where(x => x.PurchaseOrderID == purchaseOrderID).Select(x => x).Any();
	// if PO doesn't exist, it's a SuggestedOrder so we make an
	// assumption: display all values as 0 as a filler to signify it has not been updated yet.
	// since DisplayCurrentActiveOrderInfo is ONLY used for display, this should not cause any errors.
	if(!purchaseOrderExists)
	{
		displayCurrentActiveOrderInfo.PurchaseOrderID = 0;
		displayCurrentActiveOrderInfo.SubTotal = 0;
		displayCurrentActiveOrderInfo.TaxAmount = 0;
		displayCurrentActiveOrderInfo.Total = 0;
	}
	// otherwise if it does exist, grab that PO
	else
	{
		PurchaseOrders existingPurchaseOrder =  PurchaseOrders.Where(x => x.PurchaseOrderID == purchaseOrderID).Select(x => x).FirstOrDefault();
		displayCurrentActiveOrderInfo.PurchaseOrderID = existingPurchaseOrder.PurchaseOrderID;
		displayCurrentActiveOrderInfo.SubTotal = existingPurchaseOrder.SubTotal;
		displayCurrentActiveOrderInfo.TaxAmount = existingPurchaseOrder.TaxAmount;
		displayCurrentActiveOrderInfo.Total =  existingPurchaseOrder.SubTotal + existingPurchaseOrder.TaxAmount;
	}

	if(errorList.Count > 0)
	{
		throw new AggregateException("Unable to get DisplayCurrentActiveOrderInfo. Check concerns.", errorList);
	}
	else
	{
		return displayCurrentActiveOrderInfo;
	}
}

// Commands
public void Update_CurrentActiveOrder(int employeeID, int vendorID, int purchaseOrderID, List<CurrentActiveOrderDetails> currentActiveOrderDetails)
{
	// assumption changed: a currentactiveorder may not have 0 items in an order at a time, as updating with 0 items is possible but saving a suggestedOrder is not (new PurchaseOrderDetails need to be created).

	//#region Error Checking
	List<Exception> errorList = new List<Exception>();
	
	// check for missing parameters - throw exception instead of adding to errorlist, as proceeding with a missing parameter will create more errors.
	// use < 1 instead of == 0 as this will check for negative integers too.
	// ignore the warning: you still need to check for null purchaseOrderDetailID.
	if(employeeID == null || employeeID < 1)
	{
		throw new ArgumentNullException("Must provide an employeeID.");
	}
	if(vendorID == null || vendorID < 1)
	{
		throw new ArgumentNullException("Must provide a vendorID.");
	}
	// we can have purchaseOrderID == null, thus means it is a SuggestedOrder and does not yet have it's own PurchaseOrderID
	//if(purchaseOrderID == null || purchaseOrderID < 1)
	//{
	//	throw new ArgumentNullException("Must provide a purchaseOrderID.");
	//}
	if(currentActiveOrderDetails == null || currentActiveOrderDetails.Count() == 0)
	{
		throw new ArgumentNullException("Must provide a currentActiveOrderDetail.");
	}
	
	bool employeeExists = Employees.Where(x => x.EmployeeID == employeeID).Select(x => x).Any();
	if(!employeeExists)
	{
		errorList.Add(new Exception("The provided employee does not exist."));
	}
	//#endregion

	decimal calculatedSubTotal = 0;
	decimal calculatedTaxAmount = 0;
	
	foreach(CurrentActiveOrderDetails calculateItem in currentActiveOrderDetails)
	{
		calculatedSubTotal = calculatedSubTotal + (calculateItem.PurchasePrice * calculateItem.Quantity);
	}
	
	// assumption: Tax amount rate is 5% GST
	calculatedTaxAmount = calculatedSubTotal * (decimal) 0.05;
	
	Vendors matchingVendor = null;
	matchingVendor = Vendors.Where(x => x.VendorID == vendorID).Select(x => x).FirstOrDefault();
	if(matchingVendor == null)
	{
		errorList.Add(new Exception("The provided vendor does not exist."));
	}

	PurchaseOrders purchaseOrder = null;
	purchaseOrder = PurchaseOrders.Where(x => x.PurchaseOrderID == purchaseOrderID).Select(x => x).FirstOrDefault();
	if(purchaseOrder == null)
	{
		// if PurchaseOrder doesn't exist, we need to generate a new one first and THEN add the details to that.
		// *NOTE: we NEED to select the vendor first and then .Add() PurchaseOrder to that otherwise a new identity key won't be generated.
		PurchaseOrders addSuggestedOrder = new PurchaseOrders()
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
		PurchaseOrders.Add(addSuggestedOrder);
		
		// gets the PurchaseOrderID of the newly created PurchaseOrder so we can add PurchaseOrderDetails to that.
		int recentlyAddedPurchaseOrderID = matchingVendor.PurchaseOrders.Select(x => x.PurchaseOrderID).Last();
		
		// after PurchaseOrder is CREATED then we can update details/add to that purchaseorder
		foreach(CurrentActiveOrderDetails item in currentActiveOrderDetails)
		{			
			PurchaseOrderDetails addPurchaseOrderDetails = new PurchaseOrderDetails()
			{
				PurchaseOrderID = recentlyAddedPurchaseOrderID,
				StockItemID = item.StockItemID,
				PurchasePrice = item.PurchasePrice,
				Quantity = item.Quantity
			};
			
			PurchaseOrders recentlyCreated = PurchaseOrders.Where(x => x.PurchaseOrderID == recentlyAddedPurchaseOrderID).Select(x => x).FirstOrDefault();
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
		foreach(CurrentActiveOrderDetails item in currentActiveOrderDetails)
		{
			PurchaseOrderDetails updatePurchaseOrderDetails = PurchaseOrderDetails.Where(x => x.PurchaseOrderDetailID == item.PurchaseOrderDetailID).Select(x => x).FirstOrDefault();
			updatePurchaseOrderDetails.PurchasePrice = item.PurchasePrice;
			updatePurchaseOrderDetails.Quantity = item.Quantity;
		}
	}

	if(errorList.Count > 0)
	{
		throw new AggregateException("Unable to update CurrentActiveOrder. Check concerns.", errorList);
	}
	else
	{
		SaveChanges();
	}
}

// This is only AFTER the suggestedOrder has been saved.
public void Place_CurrentActiveOrder(int purchaseOrderID)
{
	List<Exception> errorList = new List<Exception>();
	
	if(purchaseOrderID == null || purchaseOrderID < 1)
	{
		throw new ArgumentException("Must provide a purchaseOrderID.");
	}
	
	PurchaseOrders existingPurchaseOrder = null;
	existingPurchaseOrder = PurchaseOrders.Where(x => x.PurchaseOrderID == purchaseOrderID).Select(x => x).FirstOrDefault();
	if(existingPurchaseOrder == null)
	{
		errorList.Add(new Exception("Suggested Order must be saved to database using Update Button before it can be placed."));
	}
	
	// what's the difference between .update() vs change directly?
	existingPurchaseOrder.OrderDate = DateTime.Now;
	
	// update all StockItems on the PurchaseOrder (with the purchaseOrderID) to reflect that it has been placed
	// *note: for every 1 StockItem there is 1 PurchaseOrderDetail, so it should be a list.
	// *RELATIONSHIPS: A PurchaseOrder can have multiple StockItems on it. And a StockItem can appear on MULTIPLE different PurchaseOrders.
	List<PurchaseOrderDetails> allStockItems = PurchaseOrderDetails.Where(x => x.PurchaseOrderID == purchaseOrderID).Select(x => x).ToList();
	foreach(PurchaseOrderDetails updateStockItem in allStockItems)
	{
		StockItems item = StockItems.Where(x => x.StockItemID == updateStockItem.StockItemID).Select(x => x).FirstOrDefault();
		item.QuantityOnOrder = item.QuantityOnOrder + updateStockItem.Quantity;
		// do we need to do .Update()?
	}
	
	if(errorList.Count > 0)
	{
		throw new AggregateException("Unable to place CurrentActiveOrder. Check concerns.", errorList);
	}
	else
	{
		SaveChanges();
	}
}

public void Delete_CurrentActiveOrder(int purchaseOrderID)
{
	List<Exception> errorList = new List<Exception>();
	
	if(purchaseOrderID == null || purchaseOrderID < 1)
	{
		throw new ArgumentException("Must provide a purchaseOrderID.");
	}
	
	PurchaseOrders existingPurchaseOrder = null;
	existingPurchaseOrder = PurchaseOrders.Where(x => x.PurchaseOrderID == purchaseOrderID).Select(x => x).FirstOrDefault();
	if(existingPurchaseOrder == null)
	{
		errorList.Add(new Exception("The purchaseOrder does not exist."));
	}
	else
	{
		//rule: We cannot alter or delete placed/closed orders.
		if(existingPurchaseOrder.OrderDate != null || existingPurchaseOrder.Closed == true)
		{
			errorList.Add(new Exception("You cannot alter placed or closed orders."));
		}
		else
		{
			// need to ONLY remove if purchase order exists, otherwise previous logic gave us Object Reference errror.
			// we were trying to remove/access a purchase order that didn't exist, but somehow it passed the OrderDate/Closed condition and got through.
			PurchaseOrders.Remove(existingPurchaseOrder);
		}
	}
	
	if(errorList.Count > 0)
	{
		throw new AggregateException("Unable to delete CurrentActiveOrder. Check concerns.", errorList);
	}
	else
	{
		SaveChanges();
	}
}