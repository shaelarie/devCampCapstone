using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyTrainer.Startup))]
namespace MyTrainer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
