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
    public class RefundServices
    {
		private eTools2021Context _context;

		internal RefundServices(eTools2021Context context)
		{
			_context = context;
		}

		public void Add_Refund(int saleID, int employeeID, List<ItemRegistration> refundItems)
		{
			// Local Variables
			Sale saleExist = null;
			SaleRefund refund = null;
			SaleRefundDetail refundDetail = null;
			SaleDetail saleItem = null;
			Coupon couponExists = null;
			Employee employee = null;
			int refundDiscount = 1;
			decimal subTotal = 0;
			decimal taxAmount = 0;

			List<Exception> errorList = new List<Exception>();
			// Validation
			saleExist = _context.Sales
							.Where(x => x.SaleID == saleID)
							.FirstOrDefault();
			if (saleExist == null)
			{
				throw new ArgumentNullException("Sale does not exist.");
			}
			foreach (var item in refundItems)
			{
				saleItem = _context.SaleDetails
								.Where(x => x.SaleID == saleID && x.StockItemID == item.StockItemID)
								.FirstOrDefault();
				if (item.Quantity > saleItem.Quantity)
				{
					errorList.Add(new ArgumentException("Refund item quantity cannot be higher than the sale quantity."));
				}
				if (item.SellingPrice > saleItem.SellingPrice)
				{
					errorList.Add(new ArgumentException("Refudn item price cannot be higher than sale price."));
				}
			}

			// Calculate Subtotal and TaxAmount
			foreach (var item in refundItems)
			{
				subTotal = subTotal + (item.Quantity * item.SellingPrice);
				StockItem stockItem = _context.StockItems
											.Where(x => x.StockItemID == item.StockItemID)
											.FirstOrDefault();
				stockItem.QuantityOnHand = stockItem.QuantityOnHand + item.Quantity;
				_context.StockItems.Update(stockItem);
			}

			couponExists = _context.Coupons
								.Where(x => x.CouponID == saleExist.CouponID)
								.FirstOrDefault();
			if (couponExists != null && couponExists.CouponID != 0)
			{
				refundDiscount = couponExists.CouponDiscount;
				subTotal = subTotal * refundDiscount;
			}

			taxAmount = subTotal * (decimal)0.05;

			// Make Refund and RefundDetails
			refund = new SaleRefund
			{
				SaleRefundDate = DateTime.Now,
				SaleID = saleID,
				EmployeeID = employeeID,
				TaxAmount = taxAmount,
				SubTotal = subTotal
			};

			foreach (var item in refundItems)
			{
				refundDetail = new SaleRefundDetail
				{
					StockItemID = item.StockItemID,
					SellingPrice = item.SellingPrice,
					Quantity = item.Quantity
				};
				refund.SaleRefundDetails.Add(refundDetail);
			}
			_context.SaleRefunds.Add(refund);

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
