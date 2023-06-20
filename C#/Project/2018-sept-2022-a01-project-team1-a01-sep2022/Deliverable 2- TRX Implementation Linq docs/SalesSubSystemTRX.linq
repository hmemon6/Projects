<Query Kind="Program">
  <Connection>
    <ID>226fd16f-2b59-4e42-a811-ac79adce1ea3</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>.\sqlexpress</Server>
    <Database>eTools2021</Database>
    <DisplayName>eTools2021-Entity</DisplayName>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

// Aaron Fong
// A01
// Sales
void Main()
{
	try {
		List<ItemRegistration> itemList = new List<ItemRegistration>();
		List<ItemRegistration> refundItems = new List<ItemRegistration>();
		ItemRegistration addItem;
		// Test Cases
		#region Working Test Sale Case
		//addItem = new ItemRegistration {
		//	StockItemID = 23,
		//	Description = "Dewalt Multi Speed Drill",
		//	Quantity = 1,
		//	SellingPrice = (decimal)45.6500
		//};
		//itemList.Add(addItem);
		//
		//addItem = new ItemRegistration {
		//	StockItemID = 5588,
		//	Description = "Milwaukee Tradesman Drill 7.5amp 3/8 in",
		//	Quantity = 3,
		//	SellingPrice = (decimal)79.9900
		//};
		//itemList.Add(addItem);		
		//
		//AddSale("D", 1, null, itemList);
		#endregion
		
		#region Working Test Refund Case
		//addItem = new ItemRegistration {
		//	StockItemID = 23,
		//	Description = "Dewalt Multi Speed Drill",
		//	Quantity = 1,
		//	SellingPrice = (decimal)45.6500
		//};
		//refundItems.Add(addItem);
		//
		//addItem = new ItemRegistration {
		//	StockItemID = 5588,
		//	Description = "Milwaukee Tradesman Drill 7.5amp 3/8 in",
		//	Quantity = 3,
		//	SellingPrice = (decimal)79.9900
		//};
		//refundItems.Add(addItem);
		//Add_Refund(1, 1, refundItems);
		//
		//SaleRefunds.Select(x => x).Dump();
		//SaleRefundDetails.Select(x => x).Dump();
		#endregion
		
		#region Working Test Add Item Case
		//List<ItemRegistration> shoppingCart = new List<ItemRegistration>();
		//shoppingCart = Add_Item(23, 5, shoppingCart);
		//shoppingCart.Dump();
		#endregion
		
		#region Add Sale Validation
		addItem = new ItemRegistration {
			StockItemID = 23,
			Description = "Dewalt Multi Speed Drill",
			Quantity = 1000,
			SellingPrice = (decimal)45.6500
		};
		itemList.Add(addItem);
		
		addItem = new ItemRegistration {
			StockItemID = 5588,
			Description = "Milwaukee Tradesman Drill 7.5amp 3/8 in",
			Quantity = 3000,
			SellingPrice = (decimal)79.9900
		};
		itemList.Add(addItem);		
		
		// Invalid Payment Type
		//AddSale("L", 1, 67, itemList);
		
		// Item Quantity and Coupon Doesnt Exist
		//AddSale("M", 1, 67, itemList);
		
		// Expired Coupon
		//AddSale("M", 1, 4, itemList);
		#endregion
		
		#region Add Item Validation
		List<ItemRegistration> shoppingCart = new List<ItemRegistration>();
		shoppingCart = Add_Item(23, 5, shoppingCart);
		shoppingCart.Dump();
		
		// Duplicate Item
		//shoppingCart = Add_Item(23, 5, shoppingCart);
		
		// Quantity Less than 0
		//shoppingCart = Add_Item(5588, 0, shoppingCart);
		
		// Discontinued
		//shoppingCart = Add_Item(4567, 1, shoppingCart);
		#endregion
		
		#region Add Refund Validation
		addItem = new ItemRegistration {
			StockItemID = 23,
			Description = "Dewalt Multi Speed Drill",
			Quantity = 500,
			SellingPrice = (decimal)500
		};
		refundItems.Add(addItem);
		
		addItem = new ItemRegistration {
			StockItemID = 5588,
			Description = "Milwaukee Tradesman Drill 7.5amp 3/8 in",
			Quantity = 500,
			SellingPrice = (decimal)200
		};
		refundItems.Add(addItem);
		
		// Sale Doesnt Exist
		Add_Refund(0, 1, refundItems);
		
		// Refund Quantity and Price above sale
		Add_Refund(1, 1, refundItems);
		#endregion

		#region Query Tests
		Sale_FetchBySaleID(1).Dump();
		SaleDetails_FetchBySaleID(1).Dump();
		//Category_FetchCategory().Dump();
		StockItems_FetchByCategoryDescription("Power Drill").Dump();
		//Coupon_FetchByCouponIDValue("Joy23").Dump();
		#endregion
	}
	catch(AggregateException ex)
	{
		foreach(var error in ex.InnerExceptions)
		{
			error.Message.Dump();
		}
	}
	catch(ArgumentNullException ex)
	{
		GetInnerException(ex).Message.Dump();
	}
	catch(Exception ex)
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

#region Query Models
//Sales Fetch
public class SalesInfo
{
    public int SaleID { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal SubTotal { get; set; }
    public int? CouponID { get; set; }
    public int? CouponDiscount { get; set; }
}

//SalesDetails Fetch
public class SaleDetailsInfo
{
    public int SaleID { get; set; }
    public int StockItemID { get; set; }
    public decimal SellingPrice { get; set; }
    public int Quantity { get; set; }
}

//Coupons Fetch
public class CouponInfo
{
    public string CouponIDValue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CouponDiscount { get; set; }
}

//Items Fetch
public class StockItemInfo
{
    public int StockItemID { get; set; }
    public int CategoryID { get; set; }
    public string Description { get; set; }
    public decimal SellingPrice { get; set; }
    public int QuantityOnHand { get; set; }
    public bool Discontinued { get; set; }
}

//Category Fetch
public class CategoryInfo
{
    public int CategoryID { get; set; }
    public string Description { get; set; }
}

//Add Sale
public class SaleRegistration
{
    public DateTime SaleDate { get; set; }
    public string PaymentType { get; set; }
    public int EmployeeID { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal SubTotal { get; set; }
    public int CouponID { get; set; }
}

//Add Item
public class ItemRegistration
{
    public int StockItemID { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal SellingPrice { get; set;}
}

//Add Refund
public class SalesRefundRegistration
{
    public DateTime SaleRefundDate { get; set; }
    public int SaleID { get; set; }
    public int EmployeeID { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal SubTotal { get; set; } 
}

public class SalesRefundDetailsRegistration
{
    public int StockItemID { get; set; }
    public decimal SellingPrice { get; set; }
    public int Quantity { get; set; } 
}
#endregion

#region Queries test
public SalesInfo Sale_FetchBySaleID(int saleID)
{
	SalesInfo sale = Sales
						.Where(x => x.SaleID == saleID)
						.Select(x => new SalesInfo {
							SaleID = x.SaleID,
							TaxAmount = x.TaxAmount,
							SubTotal = x.SubTotal,
							CouponID = x.CouponID,
							CouponDiscount = x.Coupon.CouponDiscount
						}).FirstOrDefault();
	return sale;
}

public List<SaleDetailsInfo> SaleDetails_FetchBySaleID(int saleID)
{
	IEnumerable<SaleDetailsInfo> saleDetails = SaleDetails
													.Where(x => x.SaleID == saleID)
													.Select(x => new SaleDetailsInfo {
														SaleID = x.SaleID,
														StockItemID = x.StockItemID,
														SellingPrice = x.SellingPrice,
														Quantity = x.Quantity
													});
	return saleDetails.ToList();
}

public CouponInfo Coupon_FetchByCouponIDValue(string couponIDValue)
{
	CouponInfo coupon = Coupons
							.Where(x => x.CouponIDValue == couponIDValue)
							.Select(x => new CouponInfo {
								CouponIDValue = x.CouponIDValue,
								StartDate = x.StartDate,
								EndDate = x.EndDate,
								CouponDiscount = x.CouponDiscount
							}).FirstOrDefault();
	return coupon;
}

public List<StockItemInfo> StockItems_FetchByCategoryDescription(string description)
{
	IEnumerable<StockItemInfo> stockItems = StockItems
									.Where(x => x.Category.Description == description)
									.Select(x => new StockItemInfo{
										StockItemID = x.StockItemID,
										CategoryID = x.CategoryID,
										Description = x.Description,
										SellingPrice = x.SellingPrice,
										QuantityOnHand = x.QuantityOnHand,
										Discontinued = x.Discontinued
									});
	return stockItems.ToList();
}

public List<CategoryInfo> Category_FetchCategory()
{
	IEnumerable<CategoryInfo> categories = Categories
												.Select(x => new CategoryInfo {
													CategoryID = x.CategoryID,
													Description = x.Description
												});
	return categories.ToList();
}
#endregion

#region Add Sale
public void AddSale(string paymentType, int employeeID,
						int? couponID, List<ItemRegistration> itemList)
{
	// Local Variables
	Sales sale = null;
	DateTime today = DateTime.Now;
	decimal subTotal = 0;
	decimal taxAmount = 0;
	Coupons couponExists = null;
	StockItems stockItem = null;
	SaleDetails saleItem = null;
	List<Exception> errorList = new List<Exception>();
	
	// Validation
	if(paymentType == "M" || paymentType =="C" || paymentType == "D")
	{}
	else 
	{
		throw new ArgumentException("Payment Type does not match any of the used payment types.");
	}

	foreach(var item in itemList)
	{	
		stockItem = StockItems
						.Where(x => x.StockItemID == item.StockItemID)
						.FirstOrDefault();
		if(item.Quantity > stockItem.QuantityOnHand)
		{
			errorList.Add(new ArgumentException("Item Quantity exceeds amount in quantity."));
		}
	}
	if(couponID != null)
	{
		couponExists = Coupons.Where(x => x.CouponID == couponID).FirstOrDefault();
		if(couponExists == null)
		{
			errorList.Add(new ArgumentException("Coupon does not exist."));
		}
		else if(today < couponExists.StartDate || today > couponExists.EndDate)
		{
			errorList.Add(new ArgumentException("Coupon is expired."));
		}
	}
	
	// Calculate Subtotal and TaxAmount
	foreach(var item in itemList)
	{
		subTotal = subTotal + (item.Quantity * item.SellingPrice);
		if(couponExists != null)
		{
			subTotal = subTotal * couponExists.CouponDiscount;
		}
		
		stockItem = StockItems
						.Where(x => x.StockItemID == item.StockItemID)
						.FirstOrDefault();
		stockItem.QuantityOnHand = stockItem.QuantityOnHand - item.Quantity;
		StockItems.Update(stockItem);
	}
	taxAmount = subTotal * (decimal)0.05;
	
	sale = new Sales {
		SaleDate = today,
		PaymentType = paymentType,
		EmployeeID = employeeID,
		TaxAmount = taxAmount,
		SubTotal = subTotal,
		CouponID = couponExists != null ? couponID : null,
	};
	
	foreach(var item in itemList)
	{
		saleItem = new SaleDetails {
			StockItemID = item.StockItemID,
			SellingPrice = item.SellingPrice,
			Quantity = item.Quantity
		};
		
		sale.SaleDetails.Add(saleItem);
	}
	
	Sales.Add(sale);
	
	if(errorList.Count() > 0)
	{
		throw new AggregateException("Unable to add new skills. Check concerns", errorList);
	}
	else
	{
		SaveChanges();
	}
}
#endregion

#region Add Item
// return list with the new item added to the backend of the webpage.
public List<ItemRegistration> Add_Item(int stockItemID, int quantity, List<ItemRegistration> shoppingCart)
{
	// Local Variables
	List<ItemRegistration> newShoppingCart = new List<ItemRegistration>();
	StockItems ItemValidation = null;
	ItemRegistration newItem = null;
	
	List<Exception> errorList = new List<Exception>();
	// Validation
	ItemValidation = StockItems.Where(x => x.StockItemID == stockItemID).FirstOrDefault();
	foreach(var item in shoppingCart)
	{
		if(item.StockItemID == ItemValidation.StockItemID)
		{
			throw new ArgumentException("Shopping Item already exists in the shopping cart.");
		}
	}
	
	if(ItemValidation.Discontinued == true)
	{
		throw new ArgumentException("Item has been discontinued and cannot be added to cart.");
	}
	
	if(quantity <= 0)
	{
		throw new ArgumentException("Item quantity cannot be 0 or less than 0.");
	}
	
	newItem = StockItems
				.Where(x => x.StockItemID == stockItemID)
				.Select(x => new ItemRegistration {
						StockItemID = x.StockItemID,
						Description = x.Description,
						Quantity = quantity,
						SellingPrice = x.SellingPrice
				}).FirstOrDefault();
				
	newShoppingCart = shoppingCart;
	newShoppingCart.Add(newItem);
	return newShoppingCart;
}
#endregion

#region Add Refund
public void Add_Refund(int saleID, int employeeID, List<ItemRegistration> refundItems)
{
	// Local Variables
 	Sales saleExist = null;
	SaleRefunds refund = null;
	SaleRefundDetails refundDetail = null;
	SaleDetails saleItem = null;
	Coupons couponExists = null;
	Employees employee = null;
	int refundDiscount = 1;
	decimal subTotal = 0;
	decimal taxAmount = 0;
	
	List<Exception> errorList = new List<Exception>();
	// Validation
	saleExist = Sales
					.Where(x => x.SaleID == saleID)
					.FirstOrDefault();
	if(saleExist == null)
	{
		throw new ArgumentNullException("Sale does not exist.");
	}
	foreach(var item in refundItems)
	{
		saleItem = SaleDetails
						.Where(x => x.SaleID == saleID && x.StockItemID == item.StockItemID)
						.FirstOrDefault();
		if(item.Quantity > saleItem.Quantity)
		{
			errorList.Add(new ArgumentException("Refund item quantity cannot be higher than the sale quantity."));
		}
		if(item.SellingPrice > saleItem.SellingPrice)
		{
			errorList.Add(new ArgumentException("Refudn item price cannot be higher than sale price."));
		}
	}
	
	// Calculate Subtotal and TaxAmount
	foreach(var item in refundItems)
	{
		subTotal = subTotal + (item.Quantity * item.SellingPrice);
		StockItems stockItem = StockItems
									.Where(x => x.StockItemID == item.StockItemID)
									.FirstOrDefault();
		stockItem.QuantityOnHand = stockItem.QuantityOnHand + item.Quantity;
		StockItems.Update(stockItem);
	}
	
	couponExists = Coupons
						.Where(x => x.CouponID == saleExist.CouponID)
						.FirstOrDefault();
	if(couponExists != null && couponExists.CouponID != 0)
	{
		refundDiscount = couponExists.CouponDiscount;
		subTotal = subTotal * refundDiscount;
	}
	
	taxAmount = subTotal * (decimal)0.05;
	
	// Make Refund and RefundDetails
	refund = new SaleRefunds {
		SaleRefundDate = DateTime.Now,
		SaleID = saleID,
		EmployeeID = employeeID,
		TaxAmount = taxAmount,
		SubTotal = subTotal
	};
	
	foreach(var item in refundItems)
	{
		refundDetail = new SaleRefundDetails {
			StockItemID = item.StockItemID,
			SellingPrice = item.SellingPrice,
			Quantity = item.Quantity
		};
		refund.SaleRefundDetails.Add(refundDetail);
	}
	SaleRefunds.Add(refund);
	
	if(errorList.Count() > 0)
	{
		throw new AggregateException("Unable to add new skills. Check concerns", errorList);
	}
	else
	{
		SaveChanges();
	}
}
#endregion
