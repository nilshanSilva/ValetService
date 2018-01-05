    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ValetService.ViewModels
{
    public class VehicleViewModel
    {
        [Display(Name = "Vehicle ID")]
        public string VehicleId { get; set; }

        [Display(Name = "Licence Plate")]
        public string LicencePlate { get; set; }

        [Display(Name = "Vehicle Status")]
        public string VehicleStatus { get; set; }

        public string Zone { get; set; }

        [Display(Name = "Ticket ID")]
        public string TicketId { get; set; }

    }
}