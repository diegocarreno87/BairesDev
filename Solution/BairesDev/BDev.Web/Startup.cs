using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BDev.Web.Startup))]
namespace BDev.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
