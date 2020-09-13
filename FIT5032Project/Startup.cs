using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FIT5032Project.Startup))]
namespace FIT5032Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
