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
    public class SaleServices
    {
        private eTools2021Context _context;

        internal SaleServices(eTools2021Context context)
        { 
            _context = context;
        }
		public SalesInfo Sale_FetchBySaleID(int saleID)
		{
			SalesInfo sale = _context.Sales
								.Where(x => x.SaleID == saleID)
								.Select(x => new SalesInfo
								{
									SaleID = x.SaleID,
									TaxAmount = x.TaxAmount,
									SubTotal = x.SubTotal,
									CouponID = x.CouponID,
									CouponDiscount = x.Coupon.CouponDiscount
								}).FirstOrDefault();
			return sale;
		}

		public void AddSale(string paymentType, int employeeID,
						int? couponID, List<ItemRegistration> itemList)
		{
			// Local Variables
			Sale sale = null;
			DateTime today = DateTime.Now;
			decimal subTotal = 0;
			decimal taxAmount = 0;
			Coupon couponExists = null;
			StockItem stockItem = null;
			SaleDetail saleItem = null;
			List<Exception> errorList = new List<Exception>();

			// Validation
			if (paymentType == "M" || paymentType == "C" || paymentType == "D")
			{ }
			else
			{
				throw new ArgumentException("Payment Type does not match any of the used payment types.");
			}

			foreach (var item in itemList)
			{
				stockItem = _context.StockItems
								.Where(x => x.StockItemID == item.StockItemID)
								.FirstOrDefault();
				if (item.Quantity > stockItem.QuantityOnHand)
				{
					errorList.Add(new ArgumentException("Item Quantity exceeds amount in quantity."));
				}
			}
			if (couponID != null)
			{
				couponExists = _context.Coupons.Where(x => x.CouponID == couponID).FirstOrDefault();
				if (couponExists == null)
				{
					errorList.Add(new ArgumentException("Coupon does not exist."));
				}
				else if (today < couponExists.StartDate || today > couponExists.EndDate)
				{
					errorList.Add(new ArgumentException("Coupon is expired."));
				}
			}

			// Calculate Subtotal and TaxAmount
			foreach (var item in itemList)
			{
				subTotal = subTotal + (item.Quantity * item.SellingPrice);
				if (couponExists != null)
				{
					subTotal = subTotal * couponExists.CouponDiscount;
				}

				stockItem = _context.StockItems
								.Where(x => x.StockItemID == item.StockItemID)
								.FirstOrDefault();
				stockItem.QuantityOnHand = stockItem.QuantityOnHand - item.Quantity;
				_context.StockItems.Update(stockItem);
			}
			taxAmount = subTotal * (decimal)0.05;

			sale = new Sale
			{
				SaleDate = today,
				PaymentType = paymentType,
				EmployeeID = employeeID,
				TaxAmount = taxAmount,
				SubTotal = subTotal,
				CouponID = couponExists != null ? couponID : null,
			};

			foreach (var item in itemList)
			{
				saleItem = new SaleDetail
				{
					StockItemID = item.StockItemID,
					SellingPrice = item.SellingPrice,
					Quantity = item.Quantity
				};

				sale.SaleDetails.Add(saleItem);
			}

			_context.Sales.Add(sale);

			if (errorList.Count() > 0)
			{
				throw new AggregateException("Unable to add new skills. Check concerns", errorList);
			}
			else
			{
				_context.SaveChanges();
			}
		}
	}
}
