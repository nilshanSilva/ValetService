using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Mobile.Server;

namespace ValetService.Models
{
    public class Organization : EntityData
    {
        [Required]
        public string Name { get; set; }

        public OrganizationType Type { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public ICollection<Employee> Employees { get; set; }

        [ScaffoldColumn(false)]
        public ICollection<Zone> Zones { get; set; }

        [ScaffoldColumn(false)]
        public ICollection<Tag> Tags { get; set; }

        [ScaffoldColumn(false)]
        public FeeRate FeeRate { get; set; }
    }

    public class Zone : EntityData
    {
        [Required]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public Organization Organization { get; set; }
    }

    public class Tag : EntityData
    {
        [Required, Display(Name = "Tag Number")]
        public int TagNumber { get; set; }

        [ScaffoldColumn(false)]
        public Organization Organization { get; set; }
    }

    public class FeeRate : EntityData
    {
        [Required, Display(Name = "Initial Period (Days)")]
        public int InitialPeriod { get; set; }

        [Required, Display(Name = "Initial Rate (USD/Day)")]
        [DataType(DataType.Currency), Range(0, 100)]
        public decimal IntitalRate { get; set; }

        [Required, Display(Name = "Initial Rate (USD/Day)")]
        [DataType(DataType.Currency), Range(0, 100)]
        public decimal NormalRate { get; set; }

        [ScaffoldColumn(false), Required]
        public Organization Organization { get; set; }
    }

    public enum OrganizationType { Hotel = 1, Hospital, Airport, Other }
}