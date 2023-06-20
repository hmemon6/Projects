using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.ViewModels
{
    public class CouponInfo
    {
        public int CouponID { get; set; }
        public string CouponIDValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CouponDiscount { get; set; }
    }
}
