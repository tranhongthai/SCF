using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SCF.ServiceDiscovery.Startup))]
namespace SCF.ServiceDiscovery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
