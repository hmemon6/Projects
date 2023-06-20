using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Rentals.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentals.ViewModels
{
    public class RentalsEmployeeInfo
    {
        public int RentalID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public int? CouponID { get; set; }
        public DateTime RentalDateOut { get; set; }
        public DateTime RentalDateIn { get; set; }
        public string PaymentType { get; set; }
        public string EmployeeName { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
