using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.ViewModels
{
    public class SaleRegistration
    {
        public DateTime SaleDate { get; set; }
        public string PaymentType { get; set; }
        public int EmployeeID { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal SubTotal { get; set; }
        public int CouponID { get; set; }
    }
}
