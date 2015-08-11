using System.Data.Entity.Migrations;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using Rightpoint.Peeps.Server.App_Start;

namespace Rightpoint.Peeps.Server
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always; // TODO: remove this for prod

            // this sets up configuration of your database, including your namespace and automatic migration settings
            DbMigrator migrator = new DbMigrator(new Configuration());
            migrator.Update();
        }
    }
}

