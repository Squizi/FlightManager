using System;

namespace FlightManager.Data.Models
{
    public class ReservationPersons
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string TicketType { get; set; }
        public bool IsBussinesClass { get; set; }

        public virtual Reservations Reservation { get; set; }
    }
}
