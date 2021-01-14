using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Eccomerce_shop.Startup))]
namespace Eccomerce_shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
