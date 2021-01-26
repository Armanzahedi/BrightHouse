using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpadCompanyPanel.Web.Startup))]
namespace SpadCompanyPanel.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
