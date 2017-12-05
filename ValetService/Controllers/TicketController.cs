using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using ValetService.DataObjects;
using ValetService.Models;

namespace ValetService.Controllers
{
    public class TicketController : TableController<Ticket>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Ticket>(context, Request);
        }

        // GET tables/Ticket
        public IQueryable<Ticket> GetAllTicket()
        {
            return Query(); 
        }

        // GET tables/Ticket/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Ticket> GetTicket(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Ticket/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Ticket> PatchTicket(string id, Delta<Ticket> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Ticket
        public async Task<IHttpActionResult> PostTicket(Ticket item)
        {
            Ticket current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Ticket/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTicket(string id)
        {
             return DeleteAsync(id);
        }
    }
}
