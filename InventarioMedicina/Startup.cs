using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventarioMedicina.Startup))]
namespace InventarioMedicina
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
