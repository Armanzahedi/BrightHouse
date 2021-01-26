using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpadCompanyPanel.Infrastructure.Repositories;

namespace SpadCompanyPanel.Web.Controllers
{
    public class CurrenciesController : Controller
    {
        private readonly CurrenciesRepository _repo;
        public CurrenciesController(CurrenciesRepository repo)
        {
            _repo = repo;
        }
        // GET: Currencies
        public string ChangeCurrency(int? currency)
        {
            int? a = 1;
            if (currency != null)
            {
                a = currency;
            }
            HttpCookie currencyCookie = new HttpCookie("currencyCookie");
            currencyCookie.Value = a.ToString();
            Response.Cookies.Add(currencyCookie);
            var cur = _repo.Get(a.Value).Unit;
            return cur;
        }
    }
}