#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchasing.ViewModels
{
    // Command Model
    public class CurrentActiveOrderDetails
    {
        public int PurchaseOrderDetailID { get; set; }
        public int PurchaseOrderID { get; set; }
        public int StockItemID { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Quantity { get; set; }
    }
}
