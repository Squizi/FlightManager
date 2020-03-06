using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Data.Models
{
    public class Flights
    {
        public Guid ID { get; set; }

        [Display(Name = "From")]
        public string StartLocation { get; set; }

        [Display(Name = "To")]
        public string Destination { get; set; }

        [Display(Name = "Departure at")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Arrive at")]
        public DateTime EndTime { get; set; }

        [Display(Name = "Plane type")]
        public string PlaneType { get; set; }

        [Display(Name = "Flight number")]
        public string FlightNumber { get; set; }

        [Display(Name = "Pilot Name")]
        public string PilotName { get; set; }

        [Display(Name = "Customer Capacity")]
        public int CustomerCapacity { get; set; }

        [Display(Name = "Customer Capacity Bussiness Class")]
        public int CustomerCapacityBussinessClass { get; set; }

        public ICollection<Reservations> Reservations { get; set; }
    }
}
