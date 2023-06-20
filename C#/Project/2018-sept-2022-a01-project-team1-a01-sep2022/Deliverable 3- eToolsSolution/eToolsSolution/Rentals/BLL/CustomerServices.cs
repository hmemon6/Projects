using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rentals.DAL;
using Rentals.ViewModels;

namespace Rentals.BLL
{
    public class CustomerServices
    {
        private eTools2021Context _context;

        internal CustomerServices(eTools2021Context context)
        {
            _context = context;
        }

        public CustomerInfo Get_CustomerDetails(string homePhone)
        {

            return _context.Customers
                .Where(x => x.ContactPhone == homePhone)
                .Select(x => new CustomerInfo
                {
                    CustomerID = x.CustomerID,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    Address = x.Address,
                    City = x.City,
                    PostalCode = x.PostalCode,
                    EmailAddress = x.EmailAddress,
                    ContactPhone = x.ContactPhone,
                    CompleteAdd = x.Address + ", " + x.City + ", " + x.PostalCode,
                })
                .FirstOrDefault();
        }
    }
}
