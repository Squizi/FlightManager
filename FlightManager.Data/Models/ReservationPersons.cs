using System;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Data.Models
{
    public class ReservationPersons
    {
        public Guid ID { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Surname name")]
        public string FatherName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "SSN")]
        public string SSN { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        [Display(Name = "Ticket Type")]
        public string TicketType { get; set; }
        [Display(Name = "Bussines Class")]
        public bool IsBussinesClass { get; set; }

        public virtual Reservations Reservation { get; set; }
    }
}
