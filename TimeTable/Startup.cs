using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeTable.Startup))]
namespace TimeTable
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
