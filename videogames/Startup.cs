using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VideoGames.Startup))]
namespace VideoGames
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
