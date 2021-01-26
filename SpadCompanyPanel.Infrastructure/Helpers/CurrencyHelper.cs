using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SpadCompanyPanel.Infrastructure.Helpers
{
    public static class CurrencyHelper
    {
        public static float ExchangeAmount(float currentPrice, int reqCurrencyId)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var ReqCurrencyRate = db.Currencies.Find(reqCurrencyId).ExchangeRate;
                var newPrice = currentPrice * ReqCurrencyRate;

                return newPrice;
            }

        }
        public static void SetDefaultCurrency()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["currencyCookie"];
            if (cookie == null || cookie.Value == null)
            {
                HttpCookie currencyCookie = new HttpCookie("currencyCookie") { Value = "1" };
                HttpContext.Current.Response.Cookies.Add(currencyCookie);
            }
        }
        public static int GetCurrencyId()
        {
            int currency = 1;
            HttpCookie cookie = HttpContext.Current.Request.Cookies["currencyCookie"];
            if (cookie != null && cookie.Value != "1")
            {
                currency = Convert.ToInt32(cookie.Value);
            }
            return currency;
        }
        public static string GetCurrencyUnit()
        {
            var unit = "EUR";
            HttpCookie cookie = HttpContext.Current.Request.Cookies["currencyCookie"];
            if (cookie != null && cookie.Value != null)
            {
                using (var _context = new MyDbContext())
                {
                    var currency = _context.Currencies.Find(Convert.ToInt32(cookie.Value));
                    if (currency != null && currency.Unit != null)
                    {
                        unit = currency.Unit;
                    }

                }
            }
            return unit;
        }
        public static float GetDefaultAmount(float currentPrice)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var ReqCurrencyRate = db.Currencies.Find(GetCurrencyId()).ExchangeRate;
                var newPrice = currentPrice / ReqCurrencyRate;

                return newPrice;
            }

        }
    }
}
