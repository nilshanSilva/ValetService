using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Mobile.Server;

namespace ValetService.Models
{
    public class Employee : EntityData
    {
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required, Display(Name = "Birth Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required, Display(Name = "ID Card Number")]
        [StringLength(14, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string IDCardNumber { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, Display(Name ="Role")]
        public UserLevel UserLevel { get; set; }

        [Required, ScaffoldColumn(false)]
        public Organization Organization { get; set; }
    }

    public enum Gender { Male = 1, Female }
    public enum UserLevel { ParkingOperator = 1, HeadValet, Receptionist }
}