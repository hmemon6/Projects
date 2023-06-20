using Rentals.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Rentals.ViewModels;

namespace Rentals.BLL
{
    public class EmployeeServices
    {
        private eTools2021Context _context;

        internal EmployeeServices(eTools2021Context context)
        {
            _context = context;
        }

        public string GetEmployeeDetails(int employeeId)
        {
            return _context.Employees
                .Where(x => x.EmployeeID == employeeId
                            && (x.PositionID == 4 || x.PositionID == 5)
                            && x.DateReleased == null)
                .Select(x => x.FirstName + ' ' + x.LastName)
                .FirstOrDefault();
        }
    }
}
