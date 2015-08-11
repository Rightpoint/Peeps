using System.Data.Entity.Migrations;
using Rightpoint.Peeps.Server.Models;

namespace Rightpoint.Peeps.Server.App_Start
{
    internal sealed class Configuration : DbMigrationsConfiguration<MobileServiceContext>
    {
        public Configuration()
        {
            base.AutomaticMigrationsEnabled = true;
            base.AutomaticMigrationDataLossAllowed = true;
            base.ContextKey = "Rightpoint.Peeps.Server.Models.MobileServiceContext"; // path to MobileServiceContext
            base.ContextType = typeof(MobileServiceContext);
        }
    }
}
