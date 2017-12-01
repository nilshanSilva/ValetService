using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Mobile.Server;
using ValetService.Models;


namespace ValetService.DataObjects
{
    public class Ticket : EntityData
    {
        public Employee TicketRaiser { get; set; }
        public Employee TicketCloser { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime ClosedAt { get; set; }
        public Tag Tag { get; set; }
        public Vehicle Vehicle { get; set; }
        public decimal Fee { get; set; }
    }

    public class Vehicle : EntityData
    {
        [Required]
        public string LicencePlateNumber { get; set; }
        [Required]
        public VehicleStatus Status { get; set; }
        public string ImageLocation { get; set; }
        public Zone Zone { get; set; }

        [Required]
        public Ticket Ticket { get; set; }
    }

    public enum TicketStatus { Opened = 1, Closed, Cancelled }
    public enum VehicleStatus { Parking = 1, Parked, Returning, Returned }
}