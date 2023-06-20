#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchasing.ViewModels
{
    public class DisplayVendorInfo
    {
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public int PurchaseOrderID { get; set; }
        public int EmployeeID { get; set; }
    }
}
