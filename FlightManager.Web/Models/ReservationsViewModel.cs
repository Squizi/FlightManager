using FlightManager.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Web.Models
{
    public class ReservationsViewModel
    {
        public ReservationsViewModel()
        {
            ReservationPersons = new List<ReservationPersonViewModel> {
                new ReservationPersonViewModel()
            };
        }

        public Flights Flight { get; set; }

        public int FreeSeats { get; set; }
        public int FreeSeatsBusinessClass { get; set; }

        [Display(Name = "Email address")]
        [Required]
        public string Email { get; set; }

        public List<ReservationPersonViewModel> ReservationPersons { get; set; }
    }
}
