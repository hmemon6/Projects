#nullable disable
using Sales.DAL;
using Sales.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.BLL
{
    public class CouponServices
    {
		private eTools2021Context _context;

		internal CouponServices(eTools2021Context context)
		{
			_context = context;
		}

		public CouponInfo Coupon_FetchByCouponIDValue(string couponIDValue)
		{
			CouponInfo coupon = _context.Coupons
									.Where(x => x.CouponIDValue == couponIDValue)
									.Select(x => new CouponInfo
									{
										CouponID = x.CouponID,
										CouponIDValue = x.CouponIDValue,
										StartDate = x.StartDate,
										EndDate = x.EndDate,
										CouponDiscount = x.CouponDiscount
									}).FirstOrDefault();
			return coupon;
		}
	}
}
