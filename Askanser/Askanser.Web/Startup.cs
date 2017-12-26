using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Askanser.Web.Startup))]
namespace Askanser.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
