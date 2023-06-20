using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.ViewModels
{
    public class SaleDetailsInfo
    {
        public int SaleID { get; set; }
        public int StockItemID { get; set; }
        public string Description { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }

    }
}
