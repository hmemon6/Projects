﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.ViewModels
{
    public class StockItemInfo
    {
        public int StockItemID { get; set; }
        public int CategoryID { get; set; }
        public string Description { get; set; }
        public decimal SellingPrice { get; set; }
        public int QuantityOnHand { get; set; }
        public bool Discontinued { get; set; }
    }
}
