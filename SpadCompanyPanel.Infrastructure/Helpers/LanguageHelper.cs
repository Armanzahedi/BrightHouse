using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SpadCompanyPanel.Infrastructure.Helpers
{
    public static class LanguageHelper
    {
        public static int GetCulture()
        {
            int culture = 1;
            switch (System.Globalization.CultureInfo.CurrentCulture.Name)
            {
                case "fa-IR":
                    culture = 1;
                    break;

                case "en-US":
                    culture = 2;
                    break;

            }
            return culture;
        }
    }
}
