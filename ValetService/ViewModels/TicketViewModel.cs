using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ValetService.ViewModels
{
    public class TicketViewModel
    {
        [Display(Name = "Ticket ID")]
        public string TicketId { get; set; }

        [Display(Name = "Ticket Status")]
        public string TicketStatus { get; set; }

        [Display(Name = "Ticket Raiser")]
        public string TicketRaiser { get; set; }

        [Display(Name = "Ticket Closer")]
        public string TicketCloser { get; set; }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Pickup Assigner")]
        public string PickupAssigner { get; set; }

        [Display(Name = "Pickup Accepted")]
        public string PickupAccepted { get; set; }

        [Display(Name = "Vehicle ID")]
        public string VehicleId { get; set; }

    }
}