using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SquareNews.Web.Startup))]
namespace SquareNews.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
