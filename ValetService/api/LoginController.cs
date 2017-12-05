using System.Web.Http;
using Microsoft.Azure.Mobile.Server.Config;
using ValetService.Models;
using System.Linq;
using System.Data.Entity;
using ValetService.DataObjects;

namespace ValetService.Controllers
{
    [MobileAppController]
    public class LoginController : ApiController
    {
        private MobileServiceContext db = new MobileServiceContext();

        [HttpGet, Route("api/Login")]
        // GET api/Login
        public string Get()
        {
            var employee = db.Employees
               .Where(e => e.Email == "nilshan@valet.com").Where(p => p.Password == "Nilshan@12")
               .FirstOrDefault();
            return Newtonsoft.Json.JsonConvert.SerializeObject(employee);
            //return "Hello from custom controller!";
        }

        [HttpGet, Route("api/Login")]
        public IHttpActionResult GetAccess(string email, string password)
        {
            if (email == null || password == null)
                return BadRequest();

           var employee = db.Employees.Include(o => o.Organization)
                .Where(e => e.Email == email).Where(p => p.Password == password)
                .FirstOrDefault();

            if (employee == null)
                return NotFound();

            EmployeeDto dto = new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                Surname = employee.Surname,
                Gender = employee.Gender,
                BirthDate = employee.BirthDate,
                IDCardNumber = employee.IDCardNumber,
                Email = employee.Email,
                Password = employee.Password,
                UserLevel = employee.UserLevel,
                Organization = new Organization { Id = employee.Organization.Id, Name = employee.Organization.Name},
                Version = employee.Version
            };
            return Ok(dto);
        }
    }
}
