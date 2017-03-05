using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdeaStorm.WebUI.Startup))]
namespace IdeaStorm.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}