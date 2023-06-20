#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchasing.ViewModels
{
    //These are the available StockItems
    public class DisplayStockItems
    {
        public int StockItemID { get; set; }
        public string Description { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReOrderLevel { get; set; }
        public int QuantityOnOrder { get; set; }
        public int BufferQuantity { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
