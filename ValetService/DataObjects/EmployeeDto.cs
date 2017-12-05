using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ValetService.Models;

namespace ValetService.DataObjects
{
    public class EmployeeDto : EntityData
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string IDCardNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserLevel UserLevel { get; set; }
        public Organization Organization { get; set; }
    }
}