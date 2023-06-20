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
    public class CouponInfo
    {
        public int CouponID { get; set; }
        public string? CouponIDValue { get; set; }
        public int CouponDiscount { get; set; }
    }
}
