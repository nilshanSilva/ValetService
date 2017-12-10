using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using ValetService.Models;

namespace ValetService.Controllers
{
    public class ZoneController : TableController<Zone>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Zone>(context, Request);
        }

        // GET tables/Zone
        public IQueryable<Zone> GetAllZone()
        {
            return Query(); 
        }

        // GET tables/Zone/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Zone> GetZone(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Zone/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Zone> PatchZone(string id, Delta<Zone> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Zone
        public async Task<IHttpActionResult> PostZone(Zone item)
        {
            Zone current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Zone/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteZone(string id)
        {
             return DeleteAsync(id);
        }
    }
}
