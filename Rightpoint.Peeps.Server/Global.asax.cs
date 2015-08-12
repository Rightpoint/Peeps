using System.Web;

namespace Rightpoint.Peeps.Server
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register();
        }
    }
}