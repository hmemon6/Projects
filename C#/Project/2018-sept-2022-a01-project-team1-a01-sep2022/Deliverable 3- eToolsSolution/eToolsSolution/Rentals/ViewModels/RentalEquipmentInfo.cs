using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentals.ViewModels
{
    public class RentalEquipmentInfo
    {
        public int RentalEquipmentID { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public decimal DailyRate { get; set; }
        public bool Available { get; set; }
        public string CompleteDescription { get; set; }
    }
}
