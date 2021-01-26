using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;


namespace SpadCompanyPanel.Infrastructure.Helpers
{
    public class CheckPermission
    {
        public static bool Check(string name)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User.Identity.GetUserName() != null)
            {
                using (MyDbContext db = new MyDbContext())
                {
                    string userId =
                       db.Users.FirstOrDefault(u => u.UserName.Trim().ToLower() == HttpContext.Current.User.Identity.Name.Trim().ToLower()).Id;

                    int? permissionId = db.Permissions.FirstOrDefault(p => p.Name.Trim().ToLower() == name)?.Id;
                    if (permissionId == null)
                    {
                        return false;
                    }
                    var userRoles = db.UserRoles.Where( u => u.UserId == userId).ToList();
                    var isValid = false;
                    foreach (var userRole in userRoles)
                        if (db.RolePermissions.Any(rp=>rp.PermissionId == permissionId && rp.RoleId == userRole.RoleId))
                            isValid = true;
                    
                    return isValid;
                }
            }
            return false;
        }
        public static string GetCurrentUserId()
        {
            string userId = "";
            if (HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User.Identity.GetUserName() != null)
            {
                using (MyDbContext db = new MyDbContext())
                {
                    userId =
                       db.Users.FirstOrDefault(u => u.UserName.Trim().ToLower() == HttpContext.Current.User.Identity.Name.Trim().ToLower()).Id;
                }
            }
            return userId;
        }
        public static void CheckUserPermission()
        {
            string controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            string action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();

            if (!Check(controller.Trim().ToLower() + action.Trim().ToLower()))
            {
                if (GetCurrentUserId() == "")
                {
                    HttpContext.Current.Response.Redirect($"/Account/Login?returnUrl={HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]}");
                }
                else
                {
                    HttpContext.Current.Response.Redirect($"/Account/AccessDenied?returnUrl={HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]}");
                }

            }

        }

    }
}