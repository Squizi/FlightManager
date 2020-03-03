using System;
using System.Collections.Generic;

namespace FlightManager.Data.Models
{
    public class Reservations
    {
        public Guid ID { get; set; }
        public string Email { get; set; }
        public int ReservationNumber { get; set; }

        public virtual Flights Flight { get; set; }
        public ICollection<ReservationPersons> ReservationPersons { get; set; }
    }
}
