#nullable disable
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rentals.BLL;
using Rentals.ViewModels;
using System.Xml.Linq;
using EToolsSecurity.BLL;
using EToolsSecurity.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApp.Pages.RentalPages
{
    public class RentingModel : PageModel
    {
        private readonly CustomerServices _customerServices;
        private readonly RentalEquipmentServices _rentalEquipmentServices;
        private readonly RentalDetailServices _rentalDetailServices;
        private readonly RentalServices _rentalServices;
        //private readonly EmployeeServices _employeeServices;
        private readonly CouponServices _couponServices;
        private readonly SecurityService _securityServices;
        public RentingModel(CustomerServices customerServices
                            , RentalEquipmentServices rentalEquipmentServices
                            , RentalDetailServices rentalDetailServices
                            , RentalServices rentalServices
                            //, EmployeeServices employeeServices
                            , CouponServices couponServices
                            , SecurityService securityServices)
        {
            _customerServices = customerServices;
            _rentalEquipmentServices = rentalEquipmentServices;
            _rentalDetailServices = rentalDetailServices;
            _rentalServices = rentalServices;
            //_employeeServices = employeeServices;
            _couponServices = couponServices;
            _securityServices = securityServices;
        }

        [BindProperty] public string? employeeName { get; set; } = tmpEmpname;
        [BindProperty] public CustomerInfo customerDetails { get; set; } = new CustomerInfo();

        [BindProperty]
        public List<RentalsEmployeeInfo> customerRentals { get; set; } = new List<RentalsEmployeeInfo>();

        [BindProperty] public List<RentalEquipmentInfo> equipmentList { get; set; } = new List<RentalEquipmentInfo>();

        [BindProperty] public List<RentalDetailsInfo> custRentalDetails { get; set; } = new List<RentalDetailsInfo>();
        [BindProperty] public List<Exception> ErrorList { get; set; } = new List<Exception>();
        [BindProperty] public int employeeID { get; set; } = tmpEmpID;
        [BindProperty] public string Feedback { get; set; } = null;
        [BindProperty] public string ErrFeedback { get; set; } = null;
        [BindProperty(SupportsGet = true)] public string phone_number { get; set; }
        [BindProperty(SupportsGet = true)] public int CustRentalID { get; set; }
        [BindProperty(SupportsGet = true)] public int CustEquipmentID { get; set; }
        [BindProperty(SupportsGet = true)] public int CustRentalDetailID { get; set; }
        [BindProperty(SupportsGet = true)] public int CustRentalDetailEquipmentID { get; set; }
        [BindProperty(SupportsGet = true)] public string CouponIDValue { get; set; }
        [BindProperty] public bool cantUpdate { get; set; }
        public bool hasClickedApplyBtn { get; set; }

        public CouponInfo CouponDetails { get; set; } = new CouponInfo();
        [BindProperty] public string coupMsg { get; set; } = null;

        public static string tmpEmpname = null;
        public static int tmpEmpID;
        public static int tmpCustRentalID;
        public static string tmpCouponIDValue = null;
        public static bool hasRemovedCoupon = false;


        public void OnGet()
        {
            GetEmployee(true);
        }

        public IActionResult OnPostRentSave()
        {

            try
            {
                if (tmpCustRentalID != 0 && tmpCustRentalID != CustRentalID)
                {
                    CustRentalID = tmpCustRentalID;
                }

                if (hasClickedApplyBtn)
                {
                    if (tmpCouponIDValue != null && CouponIDValue != null && tmpCouponIDValue != CouponIDValue)
                    {
                        if (!hasRemovedCoupon)
                        {
                            CouponIDValue = tmpCouponIDValue;
                        }
                    }
                }
                else
                {
                    CouponIDValue = tmpCouponIDValue;
                }

                if (CustRentalID > 0)
                {
                    //UpdateRental
                    List<RentalDetailsInfo> original = _rentalDetailServices.Get_RentalDetails(CustRentalID);

                    if (!_rentalServices.UpdateRentaRec(CustRentalID
                            , customerDetails.CustomerID
                            , employeeID
                            , _couponServices.Get_Coupon(CouponIDValue)
                            , original
                            , custRentalDetails))
                    {
                        ErrFeedback = "No changes made to rental.";
                    }
                    else
                    {
                        Feedback = $"Rental {CustRentalID} has been updated.";
                        hasClickedApplyBtn = false;
                    }
                }
                else
                {
                    var rental = _rentalServices.SaveRentaRec(customerDetails.CustomerID
                        , employeeID
                        , _couponServices.Get_Coupon(CouponIDValue)
                        , custRentalDetails);
                    if (rental > 0)
                    {
                        Feedback = $"Rental {rental} has been created to customer.";
                        tmpCouponIDValue = null;
                        CouponIDValue = null;
                        coupMsg = null;
                        custRentalDetails = new List<RentalDetailsInfo>();
                        ErrorList = new List<Exception>();
                        equipmentList = _rentalEquipmentServices.Get_RentalEquipment();
                        OnPostSearchCustomerInfo();
                        hasClickedApplyBtn = true;
                    }
                    else
                    {
                        ErrFeedback = "Error encountered while saving.";
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return Page();
        }

        public IActionResult OnPostValidateCoupon()
        {
            bool hasChanged = false;
            CouponInfo CouponDetails = new CouponInfo();
            CouponInfo OrigCouponDetails = new CouponInfo();

            hasClickedApplyBtn = true;
            if (custRentalDetails.Count == 0)
            {
                coupMsg = "No rental equipment selected to apply coupon to.";
            }
            else
            {
                hasRemovedCoupon = false;
                if (tmpCustRentalID != 0 && tmpCustRentalID != CustRentalID)
                {
                    CustRentalID = tmpCustRentalID;
                }
                
                //get original coupon

                //RentalsEmployeeInfo ls_rentals = customerRentals.Where(x => x.RentalID == CustRentalID)
                //    .FirstOrDefault();

                RentalsEmployeeInfo ls_rentals = _rentalServices.Get_RentalsEmployee(customerDetails.CustomerID, CustRentalID)
                    .FirstOrDefault();

                if (ls_rentals != null)
                {
                    OrigCouponDetails = _couponServices.Get_Coupon("", ls_rentals.CouponID);
                }

                CouponDetails = _couponServices.Get_Coupon(CouponIDValue);

                //check for a change in value
                if (CouponIDValue != null)
                {
                    if (OrigCouponDetails != null && OrigCouponDetails.CouponIDValue != null)
                    {
                        if (CouponIDValue != null && OrigCouponDetails.CouponIDValue.ToLower() != CouponIDValue.ToLower())
                        {
                            hasChanged = true;
                        }
                    }
                    if (OrigCouponDetails == null && CouponIDValue != null && CouponDetails != null)
                    {
                        hasChanged = true;
                    }
                }
                else
                {
                    //removal of coupon previously applied
                    if (OrigCouponDetails.CouponIDValue != null)
                    {
                        hasChanged = true;
                    }
                }

                if (!hasChanged)
                {
                    if (CouponDetails == null)
                    {
                        coupMsg = "Coupon is not valid.";
                    }
                    else
                    {
                        if (OrigCouponDetails.CouponIDValue == null)
                        {
                            tmpCouponIDValue = CouponDetails.CouponIDValue;
                            coupMsg = "Coupon is valid and discount will be applied on return.";
                        }
                        else
                        {
                            coupMsg = "Coupon is already in use. No change.";
                        }
                    }
                }
                else
                {
                    if (OrigCouponDetails.CouponIDValue != null)
                    {
                        coupMsg = "Coupon has been removed.";
                    }
                    else
                    {
                        coupMsg = "Coupon is valid and discount will be applied on return.";
                    }
                    tmpCouponIDValue = CouponDetails.CouponIDValue;
                }
            }
            return Page();
        }
        
        public IActionResult OnPostRentCancel()
        {
            hasClickedApplyBtn = true;
            //tmpEmpname = null;
            //tmpEmpID = 0;
            tmpCustRentalID = 0;
            tmpCouponIDValue = null;
            CouponIDValue = null;
            Feedback = null;
            ErrFeedback = null;
            coupMsg = null;
            hasRemovedCoupon = false;
            custRentalDetails = new List<RentalDetailsInfo>();
            ErrorList = new List<Exception>();
            ModelState.Clear();

            equipmentList = _rentalEquipmentServices.Get_RentalEquipment();

            return Page();
        }

        public IActionResult OnPostRemoveEquiptment()
        {
            RemoveEquiptment(CustRentalDetailID, CustRentalDetailEquipmentID);
            return Page();
        }

        public IActionResult OnPostAddEquiptment()
        {
            AddEquiptment(CustEquipmentID);
            return Page();
        }

        public IActionResult OnPostGetRentals()
        {
            RentalsEmployeeInfo ls_rentals = _rentalServices.Get_RentalsEmployee(customerDetails.CustomerID, CustRentalID)
                .FirstOrDefault();

            if (!(DateTime.Today == ls_rentals.RentalDateOut))
            {
                cantUpdate = true;
            }

            if (ls_rentals.SubTotal > 0)
            {
                cantUpdate = true;
            }

            tmpCustRentalID = CustRentalID;
            custRentalDetails = _rentalDetailServices.Get_RentalDetails(CustRentalID);

            if (ls_rentals.CouponID > 0)
            {
                CouponDetails = _couponServices.Get_Coupon("", ls_rentals.CouponID);
            }

            CouponIDValue = tmpCouponIDValue = CouponDetails.CouponIDValue;
            return Page();
        }


        public IActionResult OnPostSearchCustomer()
        {
            OnPostSearchCustomerInfo();

            return Page();
        }//eof OnPostSearchCustomer


        private void AddEquiptment(int equiptid)
        {
            RentalDetailsInfo ls_custrentdetails = new RentalDetailsInfo();

            List<RentalEquipmentInfo> lt_equipt = _rentalEquipmentServices.Get_RentalEquipment('A', equiptid);
            RentalEquipmentInfo ls_equipt = lt_equipt.FirstOrDefault();

            if (ls_equipt != null)
            {
                custRentalDetails.Add(new RentalDetailsInfo()
                {
                    RentalID = CustRentalID,
                    RentalEquipmentID = equiptid,
                    RentalDays = 1,
                    RentalRate = ls_equipt.DailyRate,
                    OutCondition = "Good",
                    InCondition = "out on rental",
                    DamageRepairCost = 0.00m,
                    Comments = null,
                    SerialNumber = ls_equipt.SerialNumber,
                    Description = ls_equipt.Description,
                    DailyRate = ls_equipt.DailyRate,
                    CompleteDescription = ls_equipt.CompleteDescription
                });
                equipmentList.RemoveAll(x => x.RentalEquipmentID == equiptid);
            }
            else
            {
                ErrorList.Add(new Exception($"Equipment {ls_equipt.Description} is no longer available for rent."));
            }
        }//eof AddEquiptment

        private void RemoveEquiptment(int rentdetailsid, int rentdetailequiptid)
        {
            RentalDetailsInfo ls_custrentdetails = new RentalDetailsInfo();
            List<RentalEquipmentInfo> lt_equipt = new List<RentalEquipmentInfo>();
            RentalEquipmentInfo ls_equipt = new RentalEquipmentInfo();
            int rentalid = 0;


            if (rentdetailsid > 0)
            {
                ls_custrentdetails = custRentalDetails.Where(x => x.RentalDetailID == rentdetailsid).FirstOrDefault();

                //assumption: equiptment is unavailable if already in a rental
                lt_equipt = _rentalEquipmentServices.Get_RentalEquipment(' ', ls_custrentdetails.RentalEquipmentID);
                ls_equipt = lt_equipt.FirstOrDefault();
                rentalid = ls_equipt.RentalEquipmentID;
            }
            else //new rental
            {
                lt_equipt = _rentalEquipmentServices.Get_RentalEquipment('A', rentdetailequiptid);
                ls_equipt = lt_equipt.FirstOrDefault();
                rentalid = rentdetailequiptid;
            }
            custRentalDetails.RemoveAll(x => x.RentalEquipmentID == rentalid);

            //check if equiptment is already in the list
            //only add if not
            if (!equipmentList.Where(x => x.RentalEquipmentID == ls_equipt.RentalEquipmentID)
                    .Any())
            {
                equipmentList.Add(ls_equipt);
            }
            equipmentList = equipmentList.OrderBy(x => x.Description).ThenBy(x => x.SerialNumber).ToList();
        }

        private void GetEmployee(bool sup)
        {
            var emplo = _securityServices.GetEmployeeInfoDeptHead(sup);
            employeeName = tmpEmpname = emplo.FirstName + ' ' + emplo.LastName;
            employeeID = tmpEmpID = emplo.EmployeeID;
        }

        private void OnPostSearchCustomerInfo()
        {
            if (phone_number == null)
            {
                ErrorList.Add(new Exception("Phone number is null. Kindly enter customer phone number."));
            }
            else
            {
                customerDetails = _customerServices.Get_CustomerDetails(phone_number);

                if (customerDetails == null)
                {
                    ErrorList.Add(new Exception("Customer does not exist or does not have a good standing."));
                }
                else
                {
                    equipmentList = _rentalEquipmentServices.Get_RentalEquipment();

                    if (equipmentList.Count() == 0)
                    {
                        ErrorList.Add(new Exception("No equipment available for rent."));
                    }

                    if (tmpCustRentalID != 0 && tmpCustRentalID != CustRentalID)
                    {
                        CustRentalID = tmpCustRentalID;
                    }

                    customerRentals = _rentalServices.Get_RentalsEmployee(customerDetails.CustomerID);
                }
            }
        }
    }
}
