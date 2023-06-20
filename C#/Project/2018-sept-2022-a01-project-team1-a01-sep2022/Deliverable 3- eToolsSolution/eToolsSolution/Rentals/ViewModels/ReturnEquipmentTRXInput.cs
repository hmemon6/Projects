using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentals.ViewModels
{
    public class ReturnEquipmentTRXInput
    {
        public int RentalDetailID { get; set; }
        public int RentalID { get; set; }
        public int RentalEquipmentID { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public decimal RentalRate { get; set; }
        public string OutCondition { get; set; }
        public string InCondition { get; set; }
        public decimal DamageRepairCost { get; set; } = 0.00m;
        public string Comments { get; set; }
        public bool Available { get; set; }
    }
}
