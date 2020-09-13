using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NOC_DKRKM.Startup))]
namespace NOC_DKRKM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
