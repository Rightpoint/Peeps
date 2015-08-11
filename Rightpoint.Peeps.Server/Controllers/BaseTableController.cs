using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using Rightpoint.Peeps.Server.Models;

namespace Rightpoint.Peeps.Server.Controllers
{
    public class BaseTableController<T> : TableController<T> where T : EntityData
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            this.DomainManager = new EntityDomainManager<T>(context, base.Request, base.Services);
        }

        // GET tables/T
        public virtual IQueryable<T> GetAllT()
        {
            return base.Query();
        }

        // GET tables/T/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public virtual SingleResult<T> GetT(string id)
        {
            return base.Lookup(id);
        }

        // PATCH tables/T/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public virtual Task<T> PatchT(string id, Delta<T> patch)
        {
            return base.UpdateAsync(id, patch);
        }

        // POST tables/T
        public virtual async Task<IHttpActionResult> PostT(T item)
        {
            T current = await base.InsertAsync(item);
            return base.CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/T/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public virtual Task DeleteT(string id)
        {
            return base.DeleteAsync(id);
        }
    }
}
