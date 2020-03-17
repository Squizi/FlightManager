using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string flight)
        {
            ViewData["flight"] = flight;

            ReservationsViewModel model = new ReservationsViewModel();

            model.Flight = _context.Flight.First(x => x.ID == new Guid(flight));
            model.FreeSeats = model.Flight.CustomerCapacity - _context.ReservationPersons.Where(x => !x.IsBussinesClass && x.Reservation.Flight.ID == model.Flight.ID).Count();
            model.FreeSeatsBusinessClass = model.Flight.CustomerCapacityBussinessClass - _context.ReservationPersons.Where(x => x.IsBussinesClass && x.Reservation.Flight.ID == model.Flight.ID).Count();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ReservationsViewModel model, string flight)
        {
            ViewData["flight"] = flight;

            model.Flight = _context.Flight.First(x => x.ID == new Guid(flight));
            model.FreeSeats = model.Flight.CustomerCapacity - _context.ReservationPersons.Where(x => !x.IsBussinesClass && x.Reservation.Flight.ID == model.Flight.ID).Count();
            model.FreeSeatsBusinessClass = model.Flight.CustomerCapacityBussinessClass - _context.ReservationPersons.Where(x => x.IsBussinesClass && x.Reservation.Flight.ID == model.Flight.ID).Count();

            if (ModelState.IsValid)
            {
                if (model.FreeSeatsBusinessClass >= model.ReservationPersons.Where(x => x.IsBussinesClass).Count()
                    && model.FreeSeats >= model.ReservationPersons.Where(x => !x.IsBussinesClass).Count())
                {
                    Reservations reservation = new Reservations();
                    reservation.Email = model.Email;
                    reservation.Flight = model.Flight;
                    reservation.ReservationPersons = new List<ReservationPersons>();
                    foreach (var person in model.ReservationPersons)
                    {
                        reservation.ReservationPersons.Add(new ReservationPersons
                        {
                            FirstName = person.FirstName,
                            FatherName = person.FatherName,
                            LastName = person.LastName,
                            SSN = person.SSN,
                            PhoneNumber = person.PhoneNumber,
                            Nationality = person.Nationality,
                            IsBussinesClass = person.IsBussinesClass
                        });
                    }
                    _context.Reservations.Add(reservation);
                    await _context.SaveChangesAsync();

                    SmtpClient client = new SmtpClient("smtp.mailtrap.io");
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("62fc31ef4c5f5e", "45944e2ba3e7f3");

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("flightmanager@dev.local");
                    mailMessage.To.Add(reservation.Email);
                    mailMessage.IsBodyHtml = false;
                    mailMessage.Body = "Your reservation for flight number " + reservation.Flight.FlightNumber + " was successful." + "\n";
                    mailMessage.Subject = "Reservation confirmation.";
                    client.Send(mailMessage);

                    return new RedirectToActionResult("Success", "Reservations", null);
                }
                else
                {
                    ModelState.AddModelError("", "There is not enough free seats.");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Success()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReservationPersonItem([Bind("Email,ReservationPersons")] ReservationsViewModel model)
        {
            ModelState.Clear();
            model.ReservationPersons.Add(new ReservationPersonViewModel());
            return PartialView("ReservationPersonItems", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveReservationPersonItem([Bind("Email,ReservationPersons")] ReservationsViewModel model)
        {
            ModelState.Clear();
            if (model.ReservationPersons.Count > 1)
            {
                model.ReservationPersons.RemoveAt(model.ReservationPersons.Count - 1);
            }
            return PartialView("ReservationPersonItems", model);
        }
    }
}