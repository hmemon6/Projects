using Rentals.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rentals.ViewModels;

namespace Rentals.BLL
{
    public class RentalDetailServices
    {
        private eTools2021Context _context;

        internal RentalDetailServices(eTools2021Context context)
        {
            _context = context;
        }

        public List<RentalDetailsInfo> Get_RentalDetails(int rentID)
        {

            return _context.RentalDetails
                .Where(x => x.RentalID == rentID)
                .Select(x => new RentalDetailsInfo
                {
                    RentalDetailID = x.RentalDetailID,
                    RentalID = x.RentalID,
                    RentalEquipmentID = x.RentalEquipmentID,
                    RentalDays = x.RentalDays,
                    RentalRate = x.RentalRate,
                    OutCondition = x.OutCondition,
                    InCondition = x.InCondition,
                    DamageRepairCost = x.DamageRepairCost,
                    Comments = x.Comments,
                    SerialNumber = x.RentalEquipment.SerialNumber,
                    Description = x.RentalEquipment.Description,
                    CompleteDescription = '(' + x.RentalEquipment.ModelNumber + ") " + x.RentalEquipment.Description,
                    DailyRate = x.RentalEquipment.DailyRate
                })
                .ToList();
        }
    }
}
