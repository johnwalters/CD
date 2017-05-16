using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CDSite.Startup))]
namespace CDSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
