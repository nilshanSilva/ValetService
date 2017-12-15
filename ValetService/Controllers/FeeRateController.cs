using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using ValetService.Models;

namespace ValetService.Controllers
{
    public class FeeRateController : TableController<FeeRate>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<FeeRate>(context, Request);
        }

        // GET tables/FeeRate
        public IQueryable<FeeRate> GetAllFeeRate()
        {
            return Query(); 
        }

        // GET tables/FeeRate/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<FeeRate> GetFeeRate(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/FeeRate/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<FeeRate> PatchFeeRate(string id, Delta<FeeRate> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/FeeRate
        public async Task<IHttpActionResult> PostFeeRate(FeeRate item)
        {
            FeeRate current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/FeeRate/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFeeRate(string id)
        {
             return DeleteAsync(id);
        }
    }
}
