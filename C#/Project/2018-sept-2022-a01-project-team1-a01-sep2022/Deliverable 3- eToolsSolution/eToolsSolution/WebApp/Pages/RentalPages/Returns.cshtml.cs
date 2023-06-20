using EToolsSecurity.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rentals.BLL;
using Rentals.ViewModels;
using System.Diagnostics.Eventing.Reader;
using System.Security.Principal;

namespace WebApp.Pages.RentalPages
{
    public class ReturnsModel : PageModel
    {
        private readonly CustomerServices _customerServices;
        private readonly RentalEquipmentServices _rentalEquipmentServices;
        private readonly RentalDetailServices _rentalDetailServices;
        private readonly RentalServices _rentalServices;
        private readonly CouponServices _couponServices;
        private readonly SecurityService _securityServices;

        public ReturnsModel(CustomerServices customerServices
            , RentalEquipmentServices rentalEquipmentServices
            , RentalDetailServices rentalDetailServices
            , RentalServices rentalServices
            , EmployeeServices employeeServices
            , CouponServices couponServices
            , SecurityService securityServices)
        {
            _customerServices = customerServices;
            _rentalEquipmentServices = rentalEquipmentServices;
            _rentalDetailServices = rentalDetailServices;
            _rentalServices = rentalServices;
            _couponServices = couponServices;
            _securityServices = securityServices;
        }
        [BindProperty] public List<ReturnEquipmentTRXInput> returnEquipt { get; set; } = new List<ReturnEquipmentTRXInput>();

        [BindProperty] public List<RentalDetailsInfo> custRentalDetails { get; set; } = new List<RentalDetailsInfo>();
        [BindProperty] public List<RentalsEmployeeInfo> customerRentals { get; set; } = new List<RentalsEmployeeInfo>();
        [BindProperty] public RentalsEmployeeInfo customerRentalRec { get; set; } = new RentalsEmployeeInfo();

        [BindProperty] public List<Exception> ErrorList { get; set; } = new List<Exception>();
        [BindProperty] public CustomerInfo customerDetails { get; set; } = new CustomerInfo();
        [BindProperty] public List<RentalEquipmentInfo> equipmentList { get; set; } = new List<RentalEquipmentInfo>();
        [BindProperty] public string Feedback { get; set; } = null;
        [BindProperty] public string ErrFeedback { get; set; } = null;
        [BindProperty] public bool cantUpdate { get; set; }
        [BindProperty] public decimal rent_days { get; set; }
        [BindProperty] public string PaymentType { get; set; }
        [BindProperty] public CouponInfo couponInfo { get; set; } = new CouponInfo();
        [BindProperty] public decimal subTotal { get; set; } = 0.00m;
        [BindProperty] public decimal costOfRepair { get; set; } = 0.00m;
        [BindProperty] public decimal gst { get; set; } = 0.00m;
        [BindProperty] public decimal discount { get; set; } = 0.00m;
        [BindProperty] public decimal total { get; set; } = 0.00m;

        //[BindProperty(SupportsGet = true)] public string CouponIDValue { get; set; }
        [BindProperty(SupportsGet = true)] public string phone_number { get; set; }
        [BindProperty(SupportsGet = true)] public int CustReturnID { get; set; }
        [BindProperty(SupportsGet = true)] public int CustRentalID { get; set; }

        public static int tmpCustReturnID;

        public void OnGet()
        {
        }

        public IActionResult OnPostReturn()
        {
            bool hasError = false;

            if (subTotal == 0)
            {
                ErrorList.Add(new Exception("Initiate payment first."));
            }

            if (CustReturnID == 0 && tmpCustReturnID != CustReturnID)
            {
                CustReturnID = tmpCustReturnID;
            }

            customerRentalRec = customerRentals.Where(x => x.RentalID == CustReturnID).FirstOrDefault();
            if (customerRentalRec != null)
            {
                customerRentalRec.PaymentType = PaymentType;
                customerRentalRec.CustomerID = customerDetails.CustomerID;
            }
            ValidateReturns();

            if (ErrorList.Count == 0)
            {
                //update rentals
                hasError = _rentalServices.UpdateReturnRec(customerRentalRec
                    , returnEquipt
                    , gst
                    , subTotal
                    , rent_days);
                if (hasError)
                {
                    ErrFeedback = "Error encountered while saving to the database.";
                }
                else
                {
                    customerRentals = _rentalServices.Get_RentalsEmployee(customerDetails.CustomerID, CustReturnID);
                    Feedback = $"Rental {CustReturnID} has been updated.";
                    tmpCustReturnID = 0;
                    cantUpdate = true;
                }
                
            }
            return Page();
        }

        public IActionResult OnPostPayReturn()
        {

            if (CustReturnID == 0 && tmpCustReturnID != CustReturnID)
            {
                CustReturnID = tmpCustReturnID;
            }
            customerRentalRec = customerRentals.Where(x => x.RentalID == CustReturnID).FirstOrDefault();
            ValidateReturns();
            couponInfo = _couponServices.Get_Coupon("", customerRentalRec.CouponID);
            if (ErrorList.Count == 0)
            {
                subTotal = 0.00m;
                costOfRepair = 0.00m;
                gst = 0.00m;
                discount = 0.00m;
                total = 0.00m;

                foreach (var item in returnEquipt)
                {
                    if (CustReturnID != item.RentalID)
                    {
                        tmpCustReturnID = CustReturnID = item.RentalID;
                    }
                    subTotal = subTotal + (item.RentalRate * Math.Ceiling(rent_days));
                    costOfRepair = costOfRepair + item.DamageRepairCost;
                }

                gst = (subTotal + costOfRepair) * 0.05m;
                if (couponInfo != null)
                {
                    discount = (subTotal + costOfRepair) * (couponInfo.CouponDiscount / 100.00m);
                }

                total = (subTotal + costOfRepair + gst) - discount;
            }
            return Page();
        }

        public IActionResult OnPostGetRentals()
        {
            tmpCustReturnID = 0;
            couponInfo = new CouponInfo();
            subTotal = 0;
            costOfRepair = 0;
            gst = 0;
            discount = 0;
            total = 0;
            returnEquipt = new List<ReturnEquipmentTRXInput>();


            tmpCustReturnID = CustReturnID;
            custRentalDetails = _rentalDetailServices.Get_RentalDetails(CustReturnID);
            customerRentalRec = _rentalServices.Get_RentalsEmployee(customerDetails.CustomerID, CustReturnID)
                .FirstOrDefault();

            PaymentType = customerRentalRec.PaymentType;
            BuildRentalDetailsInfo(false);
            couponInfo = _couponServices.Get_Coupon("", customerRentalRec.CouponID);

            if (customerRentalRec.SubTotal > 0)
            {
                cantUpdate = true;
            }
            return Page();
        }


        public IActionResult OnPostSearchReturn()
        {
            OnPostSearchCustomerInfo();
            return Page();
        }


        private void OnPostSearchCustomerInfo()
        {
            if (phone_number == null || string.IsNullOrWhiteSpace(phone_number))
            {
                ErrorList.Add(new Exception("Phone number is null. Kindly enter customer phone number."));
            }

            if (phone_number == null && CustRentalID == 0)
            {
                ErrorList.Add(new Exception("Kindly enter customer phone number and/or rental ID number."));
            }

            if (ErrorList.Count() == 0)
            {
                tmpCustReturnID = CustReturnID = CustRentalID;
                //if (tmpCustReturnID != 0 && tmpCustReturnID != CustReturnID)
                //{
                //    CustReturnID = tmpCustReturnID;
                //}

                customerDetails = _customerServices.Get_CustomerDetails(phone_number);
                customerRentals = _rentalServices.Get_RentalsEmployee(customerDetails.CustomerID, CustReturnID);

                if (customerDetails == null && customerRentals == null)
                {
                    ErrorList.Add(new Exception("Customer does not exist or does not have a good standing."));
                }
                else
                {
                    if (customerRentals.Count() > 0)
                    {
                        //get the first rental from the list
                        customerRentalRec = customerRentals.FirstOrDefault();
                        custRentalDetails = _rentalDetailServices.Get_RentalDetails(customerRentalRec.RentalID);
                        couponInfo = _couponServices.Get_Coupon("", customerRentalRec.CouponID);

                        PaymentType = customerRentalRec.PaymentType;
                        if (customerRentalRec.SubTotal > 0)
                        {
                            cantUpdate = true;
                        }

                        tmpCustReturnID = CustReturnID = customerRentalRec.RentalID;

                        //get the first record
                        BuildRentalDetailsInfo();

                        equipmentList = _rentalEquipmentServices.Get_RentalEquipment();
                    }
                    else
                    {
                        ErrorList.Add(new Exception("No rental exists for the customer."));
                    }
                }
            }
        }//eof OnPostSearchCustomerInfo

        private void BuildRentalDetailsInfo(bool onerecord = true)
        {
            returnEquipt = new List<ReturnEquipmentTRXInput>();

            for (int idx = 0; idx < custRentalDetails.Count(); idx++)
            {
                rent_days = custRentalDetails[idx].RentalDays;
                returnEquipt.Add(new ReturnEquipmentTRXInput()
                {
                    RentalID = custRentalDetails[idx].RentalID,
                    RentalDetailID = custRentalDetails[idx].RentalDetailID,
                    RentalEquipmentID = custRentalDetails[idx].RentalEquipmentID,
                    InCondition = custRentalDetails[idx].InCondition,
                    DamageRepairCost = custRentalDetails[idx].DamageRepairCost,
                    Comments = custRentalDetails[idx].Comments,
                    Description = custRentalDetails[idx].CompleteDescription,
                    SerialNumber = custRentalDetails[idx].SerialNumber,
                    RentalRate = custRentalDetails[idx].RentalRate,
                    OutCondition = custRentalDetails[idx].OutCondition,
                    Available = equipmentList.Where(x => x.RentalEquipmentID == custRentalDetails[idx].RentalEquipmentID).Any()
                });
            }
        }//eof BuildRentalDetailsInfo

        private void ValidateReturns()
        {
            double noofdays;

            if (returnEquipt.Where(x => x.Available == false).Any())
            {
                ErrorList.Add(new Exception("Cannot process payment, partial return is not allowed."));
            }

            if (returnEquipt.Where(x => string.IsNullOrWhiteSpace(x.InCondition)).Any())
            {
                ErrorList.Add(new Exception("In condition comment is required."));
            }

            if (rent_days < 0.5m)
            {
                ErrorList.Add(new Exception("The smallest rental period is 1/2 day."));
            }
            else
            {
                if ((rent_days % 1) < 0.5m && (rent_days % 1) != 0)
                {
                    ErrorList.Add(new Exception("Day period is 1/2 day."));
                }
            }

            noofdays = (DateTime.Today - customerRentalRec.RentalDateOut).TotalDays;

            if (rent_days < (int)noofdays)
            {
                ErrorList.Add(new Exception("Insufficient total rent days entered."));
            }

            //if (rent_days > ((int)noofdays + 0.5m))
            //{
            //    ErrorList.Add(new Exception($"Rent days [{rent_days}] exceed the total number of days [{noofdays} ~ {noofdays}.5 ]."));
            //}
        }//eof ValidateReturns
    }
}
