using Microsoft.EntityFrameworkCore;
using Rentals.DAL;
using Rentals.Entities;
using Rentals.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentals.BLL
{
    public class RentalServices
    {
        private eTools2021Context _context;

        internal RentalServices(eTools2021Context context)
        {
            _context = context;
        }

        public List<RentalsEmployeeInfo> Get_RentalsEmployee(int custid, int rentid = 0)
        {

            return _context.Rentals
                .Where(x => rentid == 0
                    ? x.CustomerID.Equals(custid)
                    : rentid == x.RentalID && x.CustomerID == custid)
                .Select(x => new RentalsEmployeeInfo
                {
                    RentalID = x.RentalID,
                    CustomerID = x.CustomerID,
                    EmployeeID = x.EmployeeID,
                    CouponID = x.CouponID,
                    RentalDateOut = x.RentalDateOut,
                    RentalDateIn = x.RentalDateIn,
                    PaymentType = x.PaymentType,
                    EmployeeName = x.Employee.FirstName + ' ' + x.Employee.LastName,
                    SubTotal = x.SubTotal,
                    TaxAmount = x.TaxAmount
                })
                .ToList();
        }

        public int SaveRentaRec(int cust
                                , int emp
                                , CouponInfo coupon
                                , List<RentalDetailsInfo> rentdetailsrec)
        {
            try
            {
                Rental infoRental = new Rental();
                infoRental.CustomerID = cust;
                infoRental.EmployeeID = emp;
                infoRental.CouponID = coupon.CouponID == 0 ? null : coupon.CouponID;
                infoRental.SubTotal = 0.00m;
                infoRental.TaxAmount = 0.00m;
                infoRental.RentalDateOut = DateTime.Today;
                infoRental.RentalDateIn = DateTime.Today.AddDays(1);
                infoRental.PaymentType = "C";
                _context.Rentals.Add(infoRental);

                _context.SaveChanges();

                var newrental = _context.Rentals.Max(x => x.RentalID);

                RentalDetail rentrec = new RentalDetail();
                foreach (var itm in rentdetailsrec.Where(x => x.RentalID == 0))
                {
                    rentrec = new RentalDetail();
                    rentrec.RentalID = newrental;
                    rentrec.RentalEquipmentID = itm.RentalEquipmentID;
                    rentrec.RentalDays = itm.RentalDays;
                    rentrec.RentalRate = itm.RentalRate;
                    rentrec.OutCondition = itm.OutCondition;
                    rentrec.InCondition = itm.InCondition;
                    rentrec.DamageRepairCost = itm.DamageRepairCost;
                    rentrec.Comments = itm.Comments;
                    _context.RentalDetails.Add(rentrec);

                    var equip = _context.RentalEquipments
                        .Where(x => x.RentalEquipmentID == rentrec.RentalEquipmentID)
                        .Select(x => x)
                        .FirstOrDefault();
                    equip.Available = false;
                    _context.RentalEquipments.Update(equip);
                }

                _context.SaveChanges();

                return newrental;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }//eof SaveRentaRec


        public bool UpdateRentaRec(int rentalid
                                , int cust
                                , int emp
                                , CouponInfo coupon
                                , List<RentalDetailsInfo> originalrentals
                                , List<RentalDetailsInfo> rentdetailsrec)
        {
            try
            {
                RentalDetailsInfo recfound = new RentalDetailsInfo();
                RentalDetail rentrec = new RentalDetail();
                List<RentalsEmployeeInfo> rentalhead = new List<RentalsEmployeeInfo>();

                bool haschangedoldrecord = false;

                foreach (var itm in originalrentals)
                {
                    //saved rental is removed in current changes
                    recfound = rentdetailsrec.Where(x => x.RentalDetailID == itm.RentalDetailID)
                        .FirstOrDefault();
                    if (recfound == null)
                    {
                        rentrec = new RentalDetail();
                        rentrec.RentalDetailID = itm.RentalDetailID;
                        rentrec.RentalID = rentalid;
                        rentrec.RentalEquipmentID = itm.RentalEquipmentID;
                        rentrec.RentalDays = itm.RentalDays;
                        rentrec.RentalRate = itm.RentalRate;
                        rentrec.OutCondition = itm.OutCondition;
                        rentrec.InCondition = itm.InCondition;
                        rentrec.DamageRepairCost = itm.DamageRepairCost;
                        rentrec.Comments = itm.Comments;
                        _context.RentalDetails.Remove(rentrec);

                        var equip = _context.RentalEquipments
                            .Where(x => x.RentalEquipmentID == rentrec.RentalEquipmentID)
                            .Select(x => x)
                            .FirstOrDefault();
                        equip.Available = true;
                        _context.RentalEquipments.Update(equip);

                        haschangedoldrecord = true;
                    }
                }

                //insert new rentaldetails
                foreach (var itm in rentdetailsrec.Where(x => x.RentalDetailID == 0))
                {
                    rentrec = new RentalDetail();
                    rentrec.RentalID = rentalid;
                    rentrec.RentalEquipmentID = itm.RentalEquipmentID;
                    rentrec.RentalDays = itm.RentalDays;
                    rentrec.RentalRate = itm.RentalRate;
                    rentrec.OutCondition = itm.OutCondition;
                    rentrec.InCondition = itm.InCondition;
                    rentrec.DamageRepairCost = itm.DamageRepairCost;
                    rentrec.Comments = itm.Comments;
                    _context.RentalDetails.Add(rentrec);

                    var equip = _context.RentalEquipments
                        .Where(x => x.RentalEquipmentID == rentrec.RentalEquipmentID)
                        .Select(x => x)
                        .FirstOrDefault();
                    equip.Available = false;
                    _context.RentalEquipments.Update(equip);
                }

                //check if any changes made to coupon

                rentalhead = Get_RentalsEmployee(cust, rentalid);

                RentalsEmployeeInfo rentalrec = rentalhead.FirstOrDefault();

                if (coupon != null && (rentalrec.CouponID != coupon.CouponID)
                    || (coupon == null && rentalrec.CouponID != null))
                {
                    Rental infoRental = new Rental();
                    infoRental.RentalID = rentalrec.RentalID;
                    infoRental.CustomerID = rentalrec.CustomerID;
                    infoRental.EmployeeID = rentalrec.EmployeeID;
                    infoRental.RentalDateOut = rentalrec.RentalDateOut;
                    infoRental.RentalDateIn = rentalrec.RentalDateIn;
                    infoRental.PaymentType = rentalrec.PaymentType;
                    if (coupon.CouponID > 0)
                    {
                        infoRental.CouponID = coupon.CouponID;
                    }
                    else
                    {
                        infoRental.CouponID = null;
                    }
                    _context.Rentals.Update(infoRental);
                    haschangedoldrecord = true;
                }

                if (haschangedoldrecord == false)
                {
                    return false;
                }
                else
                {
                    _context.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }//eof UodateRentaRec

        public bool UpdateReturnRec(RentalsEmployeeInfo rentalrec
                                    , List<ReturnEquipmentTRXInput> returnequipt
                                    , decimal taxamt
                                    , decimal subtotal
                                    , decimal rentdays)
        {
            RentalDetail rentrec = new RentalDetail();
            try
            {
                Rental rentalinfo = new Rental();
                rentalinfo.RentalID = rentalrec.RentalID;
                rentalinfo.CustomerID = rentalrec.CustomerID;
                rentalinfo.EmployeeID = rentalrec.EmployeeID;
                rentalinfo.SubTotal = subtotal;
                rentalinfo.TaxAmount = taxamt;
                rentalinfo.RentalDateOut = rentalrec.RentalDateOut;
                rentalinfo.RentalDateIn = rentalrec.RentalDateIn;
                rentalinfo.PaymentType = rentalrec.PaymentType;

                _context.Rentals.Update(rentalinfo);

                foreach (var item in returnequipt)
                {
                    rentrec = new RentalDetail();
                    rentrec.RentalDetailID = item.RentalDetailID;
                    rentrec.RentalID = item.RentalID;
                    rentrec.RentalEquipmentID = item.RentalEquipmentID;
                    rentrec.RentalDays = rentdays;
                    rentrec.RentalRate = item.RentalRate;
                    rentrec.InCondition = item.InCondition;
                    rentrec.OutCondition = item.OutCondition;
                    rentrec.DamageRepairCost = item.DamageRepairCost;
                    rentrec.Comments = item.Comments;

                    _context.RentalDetails.Update(rentrec);

                    var equip = _context.RentalEquipments
                        .Where(x => x.RentalEquipmentID == rentrec.RentalEquipmentID)
                        .Select(x => x)
                        .FirstOrDefault();
                    equip.Available = true;
                    _context.RentalEquipments.Update(equip);
                }

                _context.SaveChanges();


                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }//eof UpdateReturnRec

    }
}
