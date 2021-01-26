using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SpadCompanyPanel.Web.Controllers
{
    public class LanguageController : Controller
    {
        // GET: LanguageController
        public ActionResult ChangeLanguage(string Language)
        {
            try
            {
                if (Language != null)
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Language);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(Language);
                }
                var a = 0;
                HttpCookie cookie = new HttpCookie("language");
                cookie.Value = Language;
                Response.Cookies.Add(cookie);
                return Redirect(Request.UrlReferrer.ToString());
            }



            catch (Exception ex)
            {

                return HttpNotFound(); //404
            }
        }
    }
}