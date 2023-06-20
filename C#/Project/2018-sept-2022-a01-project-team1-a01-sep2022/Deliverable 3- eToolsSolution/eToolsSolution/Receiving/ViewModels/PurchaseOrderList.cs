#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiving.ViewModels
{
    public class PurchaseOrderList
    {
        public int PurchaseOrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Vendor { get; set; }
        public string Phone { get; set; }
    }
}
