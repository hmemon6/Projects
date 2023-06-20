<Query Kind="Program">
  <Connection>
    <ID>d52b7341-79cb-4e27-9005-151059e6ae19</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>LAPTOP-53H7GL8U</Server>
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
// Name: Jacqueline Zapanta
// A01
// Rentals
	try
	{
		
		//List<RentDetails> rentedList = new();        //holds final list of rented equiptment
		//List<RentalEquipment> rentedequipt = new();  //holds final list of available equiptment
		//MessageList errmsg = new();
		//RentDetails newrent;
		//List<RentalEquipment> equipt;
		//List<RentDetails> rented;		
		
		List<ReturnEquipmentTRXInput> equipt;
		decimal rentaldays;
		string paytype;
		decimal subtotal;
		decimal damages;
		decimal gst;
		decimal discx;
		decimal total;
		
		int equipmentidselected;
		int rentalidselected;
		char actionDone;
		string couponVal;
		MessageList errorMessages;
		RentalInfo rentalData = new();
		int EmployeeID = 19;          //employee sign in
			
		
		#region Simulation1 --- Renting
			//"Simulation1: user enters home phone; customer details, customer rental list, and available equipments are retrieved".Dump();
			//rentalData = Perform_SearchAction("780.773.9520",EmployeeID);
			////OR
			////rentalData = Perform_SearchAction("780.432.2222",EmployeeID);
			//
			////Display --------------------------------
			//"Customer Details".Dump();
			//rentalData.custInfo.Dump();
			//"Customer Rental List".Dump();
			//rentalData.custrentals.Dump();
			//"Equipment".Dump();
			//rentalData.origEquipt.Dump();
			////"Feedback".Dump();
			////errorMessages.Dump();			
		#endregion Simulation1

		#region Simulation2
			//"Simulation2: user selects a rental from the list; rent equipment details are displayed".Dump();
			//rentalData = Perform_SearchAction("780.773.9520",EmployeeID);
			//rentalData = Perform_SelectRental(rentalData, 101);   
			////OR
			//rentalData = Perform_SearchAction("780.432.2222",EmployeeID);
			//rentalData = Perform_SelectRental(rentalData, 257); 
			//
			////Display --------------------------------
			//"Customer Details".Dump();
			//rentalData.custInfo.Dump();
			//"Customer Rental List".Dump();
			//if (rentalData.custrentals.Count() == 0)
			//{ "Customer has no rentals yet.".Dump(); }
			//else { rentalData.custrentals.Dump();   }
			//"Equipment".Dump();
			//rentalData.newEquipt.Dump();
			//"Rent Equipment".Dump();
			//rentalData.origRentDetails.Dump();
		#endregion Simulation2
		
		#region Simulation3
//			"Simulation3: new rental, no rental selected from the list, user adds an equiptment".Dump();
//			equipmentidselected = 11; // selected equipment (Add)
//			rentalidselected = 0; // when no rental selected
//			actionDone = 'A'; // A = Add equipment; D - remove equipment
//			
//			rentalData = Perform_SearchAction("780.773.9520",EmployeeID);
//			rentalData = Perform_SelectRental(rentalData, 101);   
//			////OR
//			//rentalData = Perform_SearchAction("780.432.2222",EmployeeID);
//			//rentalData = Perform_SelectRental(rentalData, 257); 				
//			(rentalData, errorMessages) = Perform_AddEquipment(rentalData, equipmentidselected, rentalidselected, actionDone);
//			////Display --------------------------------
//			"Customer Details".Dump();
//			rentalData.custInfo.Dump();
//			"Customer Rental List".Dump();
//			rentalData.custrentals.Dump();
//			"Equipment".Dump();
//			rentalData.newEquipt.Dump();
//			"Rent Equipment".Dump();
//			rentalData.newRentDetails.Dump();
//	        "Feedback".Dump();
//			errorMessages.Dump();
		#endregion Simulation3
		
		
		#region Simulation4
//			"Simulation4: With no rental selected from the list, user adds 2 equiptments and a coupon with Save".Dump();
//			rentalidselected = 0; // when no rental selected
//			actionDone = 'A'; // A = Add equipment; D - remove equipment
//			couponVal = "NewStarts";
//			
//			//rentalData = Perform_SearchAction("780.773.9520",EmployeeID);
//			//rentalData = Perform_SelectRental(rentalData, 101);   
//			////OR
//			rentalData = Perform_SearchAction("780.432.2222",EmployeeID);
//			rentalData = Perform_SelectRental(rentalData, 257); 	
//			
//			equipmentidselected = 11; // first equipment selected (Add)
//			(rentalData, errorMessages) = Perform_AddEquipment(rentalData, equipmentidselected, rentalidselected, actionDone);		
//			
//			equipmentidselected = 2; // second equipment selected (Add)
//			(rentalData, errorMessages) = Perform_AddEquipment(rentalData, equipmentidselected, rentalidselected, actionDone);
//			
//			(rentalData, errorMessages) = Perform_AddCoupon(rentalData, couponVal);
//
//	    	//Save entry
//    		errorMessages = Perform_Save(rentalData, rentalidselected);	
//			
//			//Display --------------------------------
//			"Customer Details".Dump();
//			rentalData.custInfo.Dump();
//			"Customer Rental List".Dump();
//			rentalData.custrentals.Dump();
//			"Equipment".Dump();
//			rentalData.newEquipt.Dump();
//			"Rent Equipment".Dump();
//			rentalData.newRentDetails.Dump();
//			"Feedback".Dump();
//	        errorMessages.Dump();
		#endregion Simulation4
		
		#region Simulation5
//			"Simulation5: With no rental selected from the list, user adds 2 equiptments and a coupon, and deletes a row (with Save)".Dump();
//			rentalidselected = 0; // when no rental selected
//			actionDone = 'A';   // A = Add equipment; D - remove equipment
//			couponVal = "NewStarts";
//
//			rentalData = Perform_SearchAction("780.773.9520",EmployeeID);
//			rentalData = Perform_SelectRental(rentalData, 101);   
//			////OR
//			//rentalData = Perform_SearchAction("780.432.2222",EmployeeID);
//			//rentalData = Perform_SelectRental(rentalData, 257); 	
//			
//			equipmentidselected = 1;   // first equipment selected
//			(rentalData, errorMessages) = Perform_AddEquipment(rentalData, equipmentidselected, rentalidselected, actionDone);		
//			
//			equipmentidselected = 6; 	// second equipment selected
//			(rentalData, errorMessages) = Perform_AddEquipment(rentalData, equipmentidselected, rentalidselected, actionDone);
//			
//			(rentalData, errorMessages) = Perform_AddCoupon(rentalData, couponVal);
//			
////          Display --------------------------------
//			"Customer Details".Dump();
//			rentalData.custInfo.Dump();
//			"Customer Rental List".Dump();
//			rentalData.custrentals.Dump();
//			"Equipment".Dump();
//			rentalData.newEquipt.Dump();			
//			"Rent Equipment Before Deletion".Dump();
//			rentalData.newRentDetails.Dump();
//
//			actionDone = 'D';   // A = Add equipment; D - remove equipment
//			equipmentidselected = 1;   // first equipment selected
//			(rentalData, errorMessages) = Perform_AddEquipment(rentalData, equipmentidselected, rentalidselected, actionDone);
//			
//	    	//Save entry
////    		errorMessages = Perform_Save(rentalData, rentalidselected);			
//			"Equipment".Dump();
//			rentalData.newEquipt.Dump();
//			"Rent Equipment After Deletion".Dump();
//			rentalData.newRentDetails.Dump();
//			"Feedback".Dump();
//	        errorMessages.Dump();		
		#endregion Simulation5
		
		#region Simulation6
			"Simulation6: a rental transaction selected from the list, user adds 2 equiptment. And Saves".Dump();
			equipmentidselected = 12; // selected equipment (Add)
			rentalidselected = 257; // with rental selected
			actionDone = 'A'; // A = Add equipment; D - remove equipment
			
			//rentalData = Perform_SearchAction("780.773.9520",EmployeeID);
			//rentalData = Perform_SelectRental(rentalData, 101);   
			////OR
			rentalData = Perform_SearchAction("780.432.2222",EmployeeID);
			rentalData = Perform_SelectRental(rentalData, rentalidselected); 
			(rentalData, errorMessages) = Perform_AddEquipment(rentalData, equipmentidselected, rentalidselected, actionDone);
			//rentalData.Dump();
			equipmentidselected = 13; // selected equipment (Add)
			(rentalData, errorMessages) = Perform_AddEquipment(rentalData, equipmentidselected, rentalidselected, actionDone);
			
	    	//Save entry
			if (errorMessages.hasError == false)
			{
				errorMessages = Perform_Save(rentalData, rentalidselected);				
			}
			////Display --------------------------------
			"Customer Details".Dump();
			rentalData.custInfo.Dump();
			"Customer Rental List".Dump();
			rentalData.custrentals.Dump();
			"Equipment".Dump();
			rentalData.newEquipt.Dump();
			"Rent Equipment".Dump();
			rentalData.newRentDetails.Dump();
	        "Feedback".Dump();
			errorMessages.Dump();		
		#endregion Simulation6
		
		#region Simulation7 --- Returns
//			"Simulation7: user enters home phone and/or rental id; customer details, customer rental list, and available equipments are retrieved".Dump();
//			"User then updates rental days, payment method, and returns and pays".Dump();
//			rentalidselected = 257;
//			rentaldays = 1.5m;
//		    paytype = "C";
//			
//			//rentalData = Perform_SearchAction("780.773.9520",EmployeeID, 101);
//			//OR
//			rentalData = Perform_SearchAction("780.432.2222",EmployeeID, rentalidselected);
//			
//			
//			//**** UPDATE ME ****
//			equipt = PopulateForm(rentalidselected);
//			(errorMessages, rentalData, subtotal, damages, gst, discx, total) = Return_Equipment(rentalData, equipt, rentalidselected, rentaldays, paytype);
//
//			//Display --------------------------------
//			"Customer Details".Dump();
//			rentalData.custInfo.Dump();
//			"Customer Rental List".Dump();
//			rentalData.custrentals.Dump();
//			var displaydate = rentalData.custrentals
//							 .Where(x => x.RentalID == rentalidselected)
//							 .Select(x=>x.RentalDateOut.ToString("MM/dd/yyy"))
//							 .FirstOrDefault();
//			 displaydate = "Rent Equipment - " + displaydate;
//
//
//			"SubTotal".Dump();
//			subtotal.Dump();
//			"Total Cost of Repair".Dump();
//			damages.Dump();
//			"GST".Dump();
//			gst.Dump();
//			"Discount".Dump();
//			discx.Dump();
//			"Total".Dump();
//			total.Dump();
//			 
//			 "Feedback".Dump();
//			 errorMessages.Dump();		
//			 if (errorMessages.hasError == false)
//			 {
//			 	rentalData.Dump();
//			 	Perform_SaveReturn(rentalData, rentalidselected, subtotal, damages, gst);	 
//			 }
		#endregion Simulation7	
		
		
		
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
		//having collected a number of errors
		//	each error should be dumped to a separate line
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

// You can define other methods, fields, classes and namespaces here
#region ObjectDefinition
public class CustomerDetails
{
    public int CustomerId {get; set;}
    public string Lastname {get; set;}
    public string Firstname {get; set;}
    public string Address {get; set;}
    public string City {get; set;}
    public string PostalCode {get; set;}
    public string EmailAddress {get; set;}
    public string ContactPhone {get; set;}
}

public class RentalsEmployee
{
    public int RentalID {get; set;}
    public int CustomerID {get; set;}
    public int EmployeeID {get; set;}
    public int? CouponID {get; set;}
    public DateTime RentalDateOut {get; set;}
    public DateTime RentalDateIn {get; set;}
	public string PaymentType {get; set;}
    public string EmployeeName  {get; set;}
}

public class RentalEquipment
{
    public int RentalEquipmentID {get; set;}
    public string ModelNumber {get; set;}
    public string SerialNumber {get; set;}
    public string Description {get; set;}
    public decimal DailyRate {get; set;}
    public bool Available {get; set;}
}

public class Coupon
{
    public int CouponID {get; set;}
    public string CouponIDValue {get; set;}
	public int CouponDiscount {get; set;}
}


public class RentDetails
{
    public int RentalDetailID {get; set;}
    public int RentalID {get; set;}
    public int RentalEquipmentID {get; set;}
    public decimal RentalDays {get; set;}    
    public decimal RentalRate {get; set;}
    public string OutCondition {get; set;}
    public string InCondition {get; set;}
    public string Comments {get; set;}
    public decimal DamageRepairCost {get; set;}
}

public class RentalEquipmentTRXInput
{
    public int CustomerID {get; set;}
    public int RentalEquipmentID {get; set;}
}

public class RentalInfo
{
    public CustomerDetails custInfo {get; set;}
	public List<RentalsEmployee> custrentals {get; set;}
    public List<RentDetails> origRentDetails {get; set;}
	public List<RentDetails> newRentDetails {get; set;}
    public List<RentalEquipment> origEquipt {get; set;}
	public List<RentalEquipment> newEquipt {get; set;}
	public Coupon appliedCoupon {get; set;}
	public int EmployeeID {get; set;}
}

public class MessageList
{
	public bool hasError {get; set;}
	public List<Exception> errorMsgs {get; set;}
}

public class ReturnEquipmentTRXInput
{
    public int RentalDetailID {get; set;}
    public int RentalID {get; set;}
    public int RentalEquipmentID {get; set;}
    public string InCondition {get; set;}
    public decimal DamageRepairCost {get; set;}
    public string Comments {get; set;}
	public bool Available {get; set;}
}


#endregion ObjectDefinition

private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}


#region query

public List<RentDetails> Get_RentalDetails(int rentID){

	return RentalDetails
			.Where(x => x.RentalID == rentID)
			.Select(x => new RentDetails {
				RentalDetailID = x.RentalDetailID,
				RentalID = x.RentalID,
				RentalEquipmentID = x.RentalEquipmentID,
				RentalDays = x.RentalDays,
				RentalRate = x.RentalRate,
				OutCondition = x.OutCondition,
				InCondition = x.InCondition,
				Comments = x.Comments,
				DamageRepairCost = x.DamageRepairCost
			})
			.ToList();
}


public Coupon Get_Coupon(string couponVal){
	return Coupons
			.Where(x => x.CouponIDValue == couponVal)
			.Select(x => new Coupon {
				CouponID = x.CouponID,
				CouponIDValue = x.CouponIDValue,
				CouponDiscount = x.CouponDiscount
			})
			.FirstOrDefault();
}

public List<RentalEquipment> Get_RentalEquipment(char indicator = 'A', int equiptid = 0){

	return RentalEquipments
			.Where(x => indicator == 'A' 
						? x.Available == true
						: indicator == 'U' 
						? x.Available == false
						: x.Available == false 
						 || x.Available == true )
			.Select(x => new RentalEquipment {
				RentalEquipmentID = x.RentalEquipmentID,
				ModelNumber = x.ModelNumber,
				SerialNumber = x.SerialNumber,
				Description = x.Description,
				DailyRate = x.DailyRate,
				Available = x.Available
			})
			.Where(x => equiptid > 0 
					? x.RentalEquipmentID == equiptid
					: 1 == 1)
			.OrderBy(x => x.RentalEquipmentID)
			.ToList();
}

public List<RentalsEmployee> Get_RentalsEmployee(int custid, int rentid = 0) {
	
	return Rentals
				.Where(x => rentid == 0 
							? x.CustomerID.Equals(custid)
							: rentid == x.RentalID && x.CustomerID == custid)
				.Select(x => new RentalsEmployee {
					RentalID = x.RentalID,
					CustomerID = x.CustomerID,
					EmployeeID = x.EmployeeID,
					CouponID = x.CouponID,
					RentalDateOut = x.RentalDateOut,
					RentalDateIn = x.RentalDateIn,
					PaymentType = x.PaymentType,
					EmployeeName = x.Employee.FirstName + ' ' + x.Employee.LastName
				})
				.ToList();
}


public CustomerDetails Get_CustomerDetails(string homePhone) {
	
	return Customers
			.Where(x => x.ContactPhone == homePhone)
			.Select(x => new CustomerDetails {
				CustomerId = x.CustomerID,
				Lastname = x.LastName,
				Firstname = x.FirstName,
				Address = x.Address,
				City = x.City,
				PostalCode = x.PostalCode,
				EmailAddress = x.EmailAddress,
				ContactPhone = x.ContactPhone
			})
			.FirstOrDefault();
}
#endregion query

#region action

public RentalInfo Perform_SearchAction(string phone, int employeeid, int rentalid = 0) {
		RentalInfo rentalinfo = new();
		
		CustomerDetails customer = null;
		List<RentalsEmployee> rentals = null;
		
		customer = Get_CustomerDetails(phone);
		if (customer == null)
		{
			throw new Exception("Customer does not exist. Only customers with good standing are allowed.");
		}				

		if (customer != null)
		{  
			rentals = Get_RentalsEmployee(customer.CustomerId, rentalid);		
		}
		
		rentalinfo.origEquipt = rentalinfo.newEquipt = Get_RentalEquipment();
		
		rentalinfo.custInfo = customer;
		
		rentalinfo.custrentals = rentals;
		
		rentalinfo.EmployeeID = employeeid;
		
		if (rentalid > 0 && customer != null)  //customer details already retrieved
		{
			rentalinfo = Perform_SelectRental(rentalinfo, rentalid);
		}
		
		return rentalinfo;
}

public RentalInfo Perform_SelectRental(RentalInfo rentalinfo, int selectedRental){

		RentalInfo newrentalInfo = new();
		newrentalInfo = rentalinfo;
		newrentalInfo.origRentDetails = Get_RentalDetails(selectedRental);
		
		return newrentalInfo;
}


public (RentalInfo, MessageList) Perform_AddEquipment(
										RentalInfo rentalInfo,
										int selectedEquipment, 
										int rentalidselected,
										char action               //A = Add equipment; D - remove equipment
									  )
{
	List<RentDetails> newrentlist = new();
	RentalEquipmentTRXInput newrental = new();
	
	RentalInfo newRentalInfo = new();
	//List<RentDetails> origrent = new();
	MessageList validate = new();
	bool hasAdded = false;

	newrental.CustomerID = rentalInfo.custInfo.CustomerId;
	newrental.RentalEquipmentID = selectedEquipment;
	
	newRentalInfo = rentalInfo;

	if (action == 'A')  //Add equipment
	{
		//check if can still add equipment to a rental
		validate = Perform_ValidateRent(rentalInfo, rentalidselected);
		
		//prepare record
		newrentlist.Add(PerformAdd(rentalInfo, rentalidselected, newrental));

		if (rentalidselected != 0)    //modify rental to add equipment
		{

			newRentalInfo.newRentDetails = rentalInfo.origRentDetails;
			
			if (validate.hasError != true) 
			{
				newRentalInfo.newRentDetails.AddRange(newrentlist);
			}
		}
		else
		{
			if (rentalInfo.newRentDetails == null)
			{
				newRentalInfo.newRentDetails = newrentlist;
			}
			else 
			{
				newRentalInfo.newRentDetails = rentalInfo.newRentDetails;
				newRentalInfo.newRentDetails.AddRange(newrentlist);
			}
		}
		if (validate.hasError != true)
		{
			newRentalInfo.newEquipt.RemoveAll(x => x.RentalEquipmentID == newrental.RentalEquipmentID);
		}
	}
	else //remove equipment
	{
		hasAdded = true;
		newRentalInfo.newRentDetails.RemoveAll(x => x.RentalEquipmentID == newrental.RentalEquipmentID);
		newRentalInfo.newEquipt.AddRange(PerformRemove(rentalInfo, newrental));
	}
	
	//just sort if added equipment
	if (hasAdded == true)
	{
		newRentalInfo.newEquipt = newRentalInfo.newEquipt.OrderBy(x => x.RentalEquipmentID).ToList();
	}
	
	//regenerate; for some reason equipt is also deleted here
	if (validate.hasError != true)
	{
		newRentalInfo.origEquipt = Get_RentalEquipment();
	}
	
	return (newRentalInfo, validate);
}

private RentDetails PerformAdd(RentalInfo rentalInfo, int rentalidselected, RentalEquipmentTRXInput newrental) 
{
		RentDetails newrent = new();

		newrent.RentalID = rentalidselected;
		newrent.RentalEquipmentID = newrental.RentalEquipmentID;
		newrent.RentalDays = 1;
		newrent.RentalRate = rentalInfo.origEquipt
								.Where(x => x.RentalEquipmentID == newrental.RentalEquipmentID)
								.Select(x => x.DailyRate)
								.FirstOrDefault();
		newrent.OutCondition = "Good";
		newrent.InCondition = "out on rental";
		newrent.Comments = null;
		newrent.DamageRepairCost = 0.0000m;
		
		return newrent;
}


private List<RentalEquipment> PerformRemove(RentalInfo rentalInfo, RentalEquipmentTRXInput newrental) 
{
		List<RentalEquipment> equiptlist = new();
		RentalEquipment equipt = new();
		RentalEquipment selectedEquipt = rentalInfo.origEquipt
										.Where(x => x.RentalEquipmentID == newrental.RentalEquipmentID)
										.Select(x => x)
										.FirstOrDefault();
		
		equiptlist.Add(new RentalEquipment() {
			RentalEquipmentID = newrental.RentalEquipmentID,
			ModelNumber = selectedEquipt.ModelNumber,
			SerialNumber = selectedEquipt.SerialNumber,
			Description = selectedEquipt.Description,
			DailyRate = selectedEquipt.DailyRate,
			Available = selectedEquipt.Available
		});

		return equiptlist;
}

public (RentalInfo, MessageList) Perform_AddCoupon(RentalInfo rentaldata, string couponval)
{
	RentalInfo newrentalinfo = new();	
	MessageList messages = new() ;
	newrentalinfo = rentaldata;
	List<Exception> errorList = new List<Exception>();
	
	Coupon appcoupon = Get_Coupon(couponval);
	
	if (appcoupon != null)
	{
		newrentalinfo.appliedCoupon = appcoupon;
		messages.hasError = false;
		errorList.Add(new Exception($"Discount {appcoupon.CouponDiscount} has been applied successfully."));
	}
	else
	{
		messages.hasError = true;
		errorList.Add(new Exception($"Coupon {couponval} is invalid."));
	}
	
	if (errorList.Count() > 0 )
	{
		messages.errorMsgs = errorList;
	}
	return (newrentalinfo, messages);
}

public MessageList Perform_Save(RentalInfo rentalData, int rentalid)
{

	int newrentalid = 0;
	int newrentalitemid = 0;
	MessageList messages = new() ;
	List<Exception> errorList = new List<Exception>();
	
	if (rentalData.newRentDetails != null
	   || rentalData.newRentDetails.Count() > 0) 
	{
	
	try
	{
		if (rentalid == 0)  //new rentals
		{ 
			newrentalid = Perform_SaveRental(rentalData);
			rentalid = newrentalid;
		}

		newrentalitemid = Perform_SaveRentalItems(rentalData, rentalid);
		
		if (newrentalid != 0)
		{
			errorList.Add(new Exception($"Rental ID {newrentalid} has been created."));
		}
		else
		{
			errorList.Add(new Exception($"Rental ID {rentalid} has been updated with selected equipments."));
		}	
		
		messages.hasError = false;
		RentalsEmployee rentals = rentalData.custrentals
									.Where(x => x.RentalID == rentalid)
									.FirstOrDefault();		
		
	}
	catch (Exception ex)
	{
		messages.hasError = true;
		errorList.Add(new Exception($"{ex.Message}"));
	}

	}
	else
	{
		throw new ArgumentNullException("No equipment added to list.");
	}
	
	messages.errorMsgs = errorList;
	
	
	return messages;
}

private int Perform_SaveRental(RentalInfo rentalData)
{
		Rentals rental = new Rentals(){
			CustomerID = rentalData.custInfo.CustomerId,
			EmployeeID = rentalData.EmployeeID,
			CouponID = rentalData.appliedCoupon == null ? null : rentalData.appliedCoupon.CouponID,
			SubTotal = 0.00m,
			TaxAmount = 0.00m,
			RentalDateOut = DateTime.Today,
			RentalDateIn = DateTime.Today.AddDays(1),
			PaymentType = "C"
		};
		Rentals.Add(rental);
		SaveChanges();
	
		//get the rental ID of the newly created record
	
		return Rentals
				.Where(x => x.RentalID == rental.RentalID)
				.Select(x => x.RentalID)
				.FirstOrDefault();		
				
}

private int Perform_SaveRentalItems(RentalInfo rentalData, int rentalid)
{
	foreach(RentDetails record in rentalData.newRentDetails)
		{
			//do not update existing items; exisiting items has rentaldetailid
			if (record.RentalDetailID == 0)
			{
				RentalDetails rentalrecord = new();
				rentalrecord.RentalID = rentalid;
				rentalrecord.RentalEquipmentID = record.RentalEquipmentID;
				rentalrecord.RentalDays = record.RentalDays;
				rentalrecord.RentalRate = record.RentalRate;
				rentalrecord.OutCondition = record.OutCondition;
				rentalrecord.InCondition = record.InCondition;
				rentalrecord.DamageRepairCost = record.DamageRepairCost;
				rentalrecord.Comments = record.Comments;
				RentalDetails.Add(rentalrecord);
					
				var equip = RentalEquipments
							.Where(x => x.RentalEquipmentID == record.RentalEquipmentID)
							.Select(x => x)
							.FirstOrDefault();
				equip.Available = false;
				RentalEquipments.Update(equip);
			}			
		}
		SaveChanges();
	
	//just return 1 item 
	return RentalDetails
			.Where(x => x.RentalID == rentalid)
			.Select(x => x.RentalDetailID)
			.FirstOrDefault();
	
}


private MessageList Perform_ValidateRent(RentalInfo rentalData, int rentalid)
{
	MessageList message = new();
	List<Exception> errorMsgs = new();
	
	if (rentalid != 0)
	{  
		RentalsEmployee rentalrec = rentalData.custrentals
									.Where(x => x.RentalID == rentalid)
									.Select(x => x)
									.FirstOrDefault(); 
		
		if (!(DateTime.Today <= rentalrec.RentalDateOut && DateTime.Today < rentalrec.RentalDateIn))
		{
			message.hasError = true;
			errorMsgs.Add(new Exception($"Change is no longer acceptable. Please create a new rental transaction.")); 
		}
		
		if (errorMsgs.Count() > 0)
		{
			message.errorMsgs = errorMsgs;
		}
	}
	return message;
}

private List<ReturnEquipmentTRXInput> PopulateForm(int rentalid)
{
	List<ReturnEquipmentTRXInput> populated = new();
	
	//populated.Add(new ReturnEquipmentTRXInput() {
	//	RentalDetailID = 895,
	//	RentalID = rentalid,
	//	RentalEquipmentID = 11,
	//	InCondition = "Good",
	//	DamageRepairCost = 0,
	//	Comments = "Ok",
	//	Available = false
	//});
	//
	//populated.Add(new ReturnEquipmentTRXInput() {
	//	RentalDetailID = 896,
	//	RentalID = rentalid,
	//	RentalEquipmentID = 2,
	//	InCondition = "Good",
	//	DamageRepairCost = 0,
	//	Comments = "Ok",
	//	Available = false
	//});
	
	populated.Add(new ReturnEquipmentTRXInput() {
		RentalDetailID = 957,
		RentalID = rentalid,
		RentalEquipmentID = 12,
		InCondition = "Bad",
		DamageRepairCost = 150,
		Comments = "Repair paid cc ***2019",
		Available = false
	});	
	
	populated.Add(new ReturnEquipmentTRXInput() {
		RentalDetailID = 958,
		RentalID = rentalid,
		RentalEquipmentID = 13,
		InCondition = "Bad",
		DamageRepairCost = 200,
		Comments = "Repair paid cc ***2019",
		Available = false
	});		
	return populated;
}


public (MessageList, RentalInfo, decimal, decimal, decimal, decimal, decimal ) 
		Return_Equipment(RentalInfo rentaldata, List<ReturnEquipmentTRXInput> equipt, int rentalid, decimal rentaldays, string paytype)
{
	RentalsEmployee rentalinfo = null;
	List<Exception> errmsg;
	MessageList list = new();
	
	decimal subtotal = 0;
	decimal tsub = 0;
	decimal damages = 0;
	decimal gst = 0;
	decimal discx = 0;
	decimal total = 0;
	
	rentalinfo = rentaldata.custrentals
					.Where(x => x.RentalID == rentalid)
					.FirstOrDefault();
					
	errmsg = Perform_ValidateRent(rentalinfo, rentaldays, equipt);
	list.errorMsgs = errmsg;
	
	if (errmsg.Count() == 0)
	{
	
		foreach(ReturnEquipmentTRXInput item in equipt)
		{
			subtotal = subtotal + rentaldata.origRentDetails
						.Where(x => x.RentalEquipmentID == item.RentalEquipmentID)
						.Select(x => x.RentalRate)
						.FirstOrDefault()
						* rentaldays;
		    
			damages = damages + item.DamageRepairCost;	
			
			//update rent details
			foreach(RentDetails rec in rentaldata.origRentDetails.Where(x => x.RentalDetailID == item.RentalDetailID
																		   && x.RentalID == rentalid
																		   && x.RentalEquipmentID == item.RentalEquipmentID))
			{
				rec.RentalDays = rentaldays;
				rec.InCondition = item.InCondition;
				rec.Comments = item.Comments;
				rec.DamageRepairCost = item.DamageRepairCost;
			}
			
			//update equipment availability
			var equip = RentalEquipments
							.Where(x => x.RentalEquipmentID == item.RentalEquipmentID)
							.Select(x => x)
							.FirstOrDefault();
			equip.Available = true;
			RentalEquipments.Update(equip);
			
		}
		tsub = subtotal + damages;
		gst = tsub * 0.05m;
		
		if (rentaldata.appliedCoupon != null)
		{
			discx = tsub * (rentaldata.appliedCoupon.CouponDiscount / 100);
		}
		
		total = (tsub + gst) - discx;
		
		//update rental info		
		foreach(RentalsEmployee rental in rentaldata.custrentals.Where(x => x.RentalID == rentalid))
		{
			rental.RentalDateIn = DateTime.Today;
			rental.PaymentType = paytype;
		}
	}
	
	return (list, rentaldata, subtotal, damages, gst, discx, total);

}

private List<Exception> Perform_ValidateRent(RentalsEmployee rentalinfo, decimal rentaldays, List<ReturnEquipmentTRXInput> equipt)
{
	List<Exception> errmsg = new();
	bool hasError;
	double noofdays;
	
	//check for partial return
	hasError = equipt
				.Where(x => x.Available == true)
				.Any();
				
	if (hasError)
	{
		errmsg.Add(new Exception("Partial return is not allowed."));
	}
	
	//check for blank InCondition
	hasError = equipt
				.Where(x => String.IsNullOrEmpty(x.InCondition))
				.Any();
				
	if (hasError)
	{
		errmsg.Add(new Exception("In condition comment is required."));
	}
	
	//check that rental days smallest is 1/2 ex. 1.2 is invalid 1.5 is valid
	if (rentaldays < 0.5m) 
	{
		errmsg.Add(new Exception("The smallest rental period is 1/2 day."));
	}
	else
	{
		if ((rentaldays % 1) < 0.5m && (rentaldays % 1) != 0)
		{
			errmsg.Add(new Exception("Day period is 1/2 day."));
		}
	}
	
	//rental days should not be less than the system calculated days
	noofdays = (DateTime.Today - rentalinfo.RentalDateOut).TotalDays;

	if (rentaldays < (int)noofdays)
	{
		errmsg.Add(new Exception("Insufficient total rent days entered."));
	}
	
	if (rentaldays >= ((int)noofdays + 1))
	{
		errmsg.Add(new Exception($"Rent days [{rentaldays}] exceed the total number of days [{noofdays} ~ {noofdays}.5 ]."));
	}
	
	
	return errmsg;
}

private void Perform_SaveReturn(RentalInfo rentaldata, int rentalid, decimal subtotal, decimal damages, decimal taxamt)
{
	RentalDetails details = new();

	RentalsEmployee custrentals = rentaldata.custrentals
									.Where(x => x.RentalID == rentalid)
									.Select(x => x)
									.FirstOrDefault();
	rentaldata.Dump();
	try
	{
		
		if (custrentals != null)
			{
				Rentals rent = new();
				rent.RentalID = custrentals.RentalID;
				rent.CustomerID = custrentals.CustomerID;
				rent.EmployeeID = custrentals.EmployeeID;
				rent.CouponID = custrentals.CouponID;
				rent.SubTotal = subtotal + damages;
				rent.TaxAmount = taxamt;
				rent.RentalDateOut = custrentals.RentalDateOut;
				rent.RentalDateIn = custrentals.RentalDateIn;
				rent.PaymentType = custrentals.PaymentType;
				Rentals.Update(rent);		
			}
		
			foreach(RentDetails item in rentaldata.origRentDetails.Where(x => x.RentalID == rentalid))
			{			
				details = new();
				details.RentalDetailID = item.RentalDetailID;
				details.RentalID = rentalid;
				details.RentalEquipmentID = item.RentalEquipmentID;
				details.RentalDays = item.RentalDays;
				details.RentalRate = item.RentalRate;
				details.OutCondition = item.OutCondition;
				details.InCondition = item.InCondition;
				details.DamageRepairCost = item.DamageRepairCost;
				details.Comments = item.Comments;
				RentalDetails.Update(details);
			}	
			
			SaveChanges();
			"Required changes are commited to database(s).".Dump();
	}
	catch{}
	
	
}
#endregion action

