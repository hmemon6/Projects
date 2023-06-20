using Rentals.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rentals.ViewModels;

namespace Rentals.BLL
{
    public class CouponServices
    {
        private eTools2021Context _context;

        internal CouponServices(eTools2021Context context)
        {
            _context = context;
        }


        public CouponInfo Get_Coupon(string couponVal = "", int? coupon = 0)
        {
            CouponInfo couponInfo = new CouponInfo();

            if (couponVal != "" && couponVal != null )
            {
                couponInfo = _context.Coupons
                    .Where(x => x.CouponIDValue.ToLower() == couponVal.ToLower())
                    .Select(x => new CouponInfo
                    {
                        CouponID = x.CouponID,
                        CouponIDValue = x.CouponIDValue,
                        CouponDiscount = x.CouponDiscount
                    })
                    .FirstOrDefault();

            }
            if (coupon > 0)
            {
                couponInfo = _context.Coupons
                    .Where(x => x.CouponID == coupon)
                    .Select(x => new CouponInfo
                    {
                        CouponID = x.CouponID,
                        CouponIDValue = x.CouponIDValue,
                        CouponDiscount = x.CouponDiscount
                    })
                    .FirstOrDefault();
            }
            if (coupon > 0 && (couponVal != "" && couponVal != null))
            {
                couponInfo = _context.Coupons
                    .Where(x => x.CouponID == coupon
                        && x.CouponIDValue.ToLower() == couponVal.ToLower()
                    )
                    .Select(x => new CouponInfo
                    {
                        CouponID = x.CouponID,
                        CouponIDValue = x.CouponIDValue,
                        CouponDiscount = x.CouponDiscount
                    })
                    .FirstOrDefault();
            }
            return couponInfo;
        }
    }
}
