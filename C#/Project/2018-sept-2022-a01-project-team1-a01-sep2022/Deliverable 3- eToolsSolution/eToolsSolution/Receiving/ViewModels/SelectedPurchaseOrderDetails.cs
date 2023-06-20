#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiving.ViewModels
{
    public class SelectedPurchaseOrderDetails
    {
        public int StockItemID { get; set; }
        public string Description { get; set; }
        public int QuanitiyOrdered { get; set; }
        public int QuanitiyOutStanding { get; set; }
        public int Receive { get; set; }
        public int Return { get; set; }
        public string Reason { get; set; }
    }
}
