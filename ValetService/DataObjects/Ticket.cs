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
        [Required]
        public string TicketRaiserId { get; set; }
        public string  TicketCloserId { get; set; }
        public string PickupAssignerId { get; set; }
        public bool PickupAccepted { get; set; }
        public TicketStatus Status { get; set; }
        public DateTimeOffset ClosedAt { get; set; }
        public DateTimeOffset OpenedAt { get; set; }
        public string  TagId { get; set; }
        public string  VehicleId { get; set; }
        public decimal Fee { get; set; }
        public string OrganizationId { get; set; }
    }

    public class Vehicle : EntityData
    {
        [Required]
        public string LicencePlateNumber { get; set; }
        [Required]
        public VehicleStatus Status { get; set; }
        public string ImageLocation { get; set; }
        public string ZoneId { get; set; }

        [Required]
        public string TicketId { get; set; }

        public string OrganizationId { get; set; }
    }

    public enum TicketStatus { Opened = 1, Closed, Cancelled }
    public enum VehicleStatus { Parking = 1, Parked, Returning, Returned }
}