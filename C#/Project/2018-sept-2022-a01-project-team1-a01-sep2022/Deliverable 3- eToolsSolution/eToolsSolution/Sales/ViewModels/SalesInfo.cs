using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.ViewModels
{
    public class SalesInfo
    {
        public int SaleID { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal SubTotal { get; set; }
        public int? CouponID { get; set; }
        public int? CouponDiscount { get; set; }
    }
}
