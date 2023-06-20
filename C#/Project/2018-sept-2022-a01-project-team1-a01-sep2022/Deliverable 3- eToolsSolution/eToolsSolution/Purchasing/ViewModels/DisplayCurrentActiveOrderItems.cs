#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchasing.ViewModels
{

    //This is similar to the DisplayStockItems ViewModel, however instead of available items, these are the non-available items.
    //Items actually on the CurrentActiveOrder.
    public class DisplayCurrentActiveOrderItems
    {
        public int StockItemID { get; set; }
        public string Description { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReOrderLevel { get; set; }
        public int QuantityOnOrder { get; set; }
        public int QuantityToOrder { get; set; }
        public int BufferQuantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public int PurchaseOrderDetailID { get; set; }
        public int PurchaseOrderID { get; set; }
    }
}
