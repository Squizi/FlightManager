using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Web.Models
{
    public class ReservationPersonViewModel
    {
        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Father name")]
        [Required]
        public string FatherName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "SNN")]
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid {0}, format is 10 digits")]
        public string SSN { get; set; }

        [Display(Name = "Phone number")]
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "Nationality")]
        [Required]
        public string Nationality { get; set; }

        public string TicketType { get; set; }

        [Display(Name = "Business class")]
        public bool IsBussinesClass { get; set; }
    }
}
