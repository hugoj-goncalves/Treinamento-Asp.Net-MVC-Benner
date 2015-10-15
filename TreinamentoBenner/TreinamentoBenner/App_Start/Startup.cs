using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TreinamentoBenner.Startup))]
namespace TreinamentoBenner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
