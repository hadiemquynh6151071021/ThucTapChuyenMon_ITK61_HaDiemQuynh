using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ceramics_Store.Startup))]
namespace Ceramics_Store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
