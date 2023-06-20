using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchasing.ViewModels
{
    public class AllPurchaseOrders
    {
        public int PurchaseOrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public int VendorID { get; set; }
        public int EmployeeID { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal SubTotal { get; set; }
        public bool Closed { get; set; }
        public string Notes { get; set; }
    }
}
