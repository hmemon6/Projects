#nullable disable
using Sales.DAL;
using Sales.Entities;
using Sales.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL
{
    public class StockItemServices
    {
        private eTools2021Context _context;

        internal StockItemServices(eTools2021Context context)
        {
            _context = context;
        }

		public List<StockItemInfo> StockItems_FetchByCategoryDescription(string description)
		{
			IEnumerable<StockItemInfo> stockItems = _context.StockItems
											.Where(x => x.Category.Description == description)
											.Select(x => new StockItemInfo
											{
												StockItemID = x.StockItemID,
												CategoryID = x.CategoryID,
												Description = x.Description,
												SellingPrice = x.SellingPrice,
												QuantityOnHand = x.QuantityOnHand,
												Discontinued = x.Discontinued
											});
			return stockItems.ToList();
		}
		public void Add_Item(int stockItemID, int quantity)
		{
			// Local Variables
			StockItem ItemValidation = null;
			ItemRegistration newItem = null;

			List<Exception> errorList = new List<Exception>();
			// Validation
			if (quantity <= 0)
			{
				throw new ArgumentException("Item quantity cannot be 0 or less than 0.");
			}
			else
			{
				ItemValidation = _context.StockItems.Where(x => x.StockItemID == stockItemID).FirstOrDefault();
				foreach (var item in ShoppingCart.ShoppingCartItems)
				{
					if (item.StockItemID == ItemValidation.StockItemID)
					{
						throw new ArgumentException("Shopping Item already exists in the shopping cart.");
					}
				}

				if (ItemValidation.Discontinued == true)
				{
					throw new ArgumentException("Item has been discontinued and cannot be added to cart.");
				}
			}



			newItem = _context.StockItems
						.Where(x => x.StockItemID == stockItemID)
						.Select(x => new ItemRegistration
						{
							StockItemID = x.StockItemID,
							Description = x.Description,
							Quantity = quantity,
							SellingPrice = x.SellingPrice
						}).FirstOrDefault();


			ShoppingCart.ShoppingCartItems.Add(newItem);
		}
	}
}
