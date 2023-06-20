using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentals.ViewModels
{
    public class CustomerInfo
    {
        public int CustomerID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? ContactPhone { get; set; }
        public string EmailAddress { get; set; }
        public string CompleteAdd { get; set; }
    }
}
