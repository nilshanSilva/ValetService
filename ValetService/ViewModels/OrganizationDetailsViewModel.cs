using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ValetService.Models;

namespace ValetService.ViewModels
{
    public class OrganizationDetailsViewModel
    {
        public Organization Organization { get; set; }
        public Zone Zone { get; set; }
        public Tag Tag { get; set; }
        public FeeRate FeeRate { get; set; }
    }
}