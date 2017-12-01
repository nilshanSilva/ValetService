using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ValetService.Startup))]

namespace ValetService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}