using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Marcusca10.Samples.BuildingAccessNet.Web.Startup))]
namespace Marcusca10.Samples.BuildingAccessNet.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
