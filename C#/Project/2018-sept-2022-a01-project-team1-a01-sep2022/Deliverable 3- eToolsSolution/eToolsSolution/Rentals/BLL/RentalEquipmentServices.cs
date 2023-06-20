using Rentals.DAL;
using Rentals.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentals.BLL
{
    public class RentalEquipmentServices
    {
        private eTools2021Context _context;

        internal RentalEquipmentServices(eTools2021Context context)
        {
            _context = context;
        }

        public List<RentalEquipmentInfo> Get_RentalEquipment(char indicator = 'A', int equiptid = 0)
        {

            return _context.RentalEquipments
                .Where(x => indicator == 'A'
                    ? x.Available == true
                    : indicator == 'U'
                        ? x.Available == false
                        : x.Available == false
                          || x.Available == true)
                .Select(x => new RentalEquipmentInfo
                {
                    RentalEquipmentID = x.RentalEquipmentID,
                    ModelNumber = x.ModelNumber,
                    SerialNumber = x.SerialNumber,
                    Description = x.Description,
                    DailyRate = x.DailyRate,
                    Available = x.Available,
                    CompleteDescription = "(" + x.ModelNumber + ") " + x.Description
                })
                .Where(x => equiptid > 0
                    ? x.RentalEquipmentID == equiptid
                    : 1 == 1)
                .OrderBy(x => x.Description)
                .ThenBy(x => x.SerialNumber)
                .ToList();
        }

    }
}
