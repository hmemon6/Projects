using Sales.DAL;
using Sales.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL
{
    public class SaleDetailServices
    {
        private eTools2021Context _context;

        internal SaleDetailServices(eTools2021Context context)
        {
            _context = context;
        }

        public List<SaleDetailsInfo> SaleDetails_FetchBySaleID(int saleID)
        {
            IEnumerable<SaleDetailsInfo> saleDetails = _context.SaleDetails
                                                            .Where(x => x.SaleID == saleID)
                                                            .Select(x => new SaleDetailsInfo
                                                            {
                                                                SaleID = x.SaleID,
                                                                StockItemID = x.StockItemID,
                                                                Description = x.StockItem.Description,
                                                                SellingPrice = x.SellingPrice,
                                                                Quantity = x.Quantity
                                                            });
            return saleDetails.ToList();
        }
    }
}
