using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Tables;
using Rightpoint.Peeps.Server.DataObjects;

namespace Rightpoint.Peeps.Server.Models
{

    public class MobileServiceContext : DbContext
    {
        private const string ConnectionStringName = "Name=MS_TableConnectionString";
        public string Schema { get; private set; }

        public MobileServiceContext() : base(ConnectionStringName)
        {
            this.Schema = ServiceSettingsDictionary.GetSchemaName();
        }

        public DbSet<Peep> Peeps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));

            base.OnModelCreating(modelBuilder);
        }
    }

}
