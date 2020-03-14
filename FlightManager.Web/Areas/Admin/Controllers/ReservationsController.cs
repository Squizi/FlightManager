using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightManager.Data;
using FlightManager.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FlightManager.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Reservations
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservations.ToListAsync());
        }

        // GET: Admin/Reservations/Details/5
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var reservations = await _context.Reservations.Where(r => r.ID == id).Include(p => p.ReservationPersons).AsNoTracking().FirstOrDefaultAsync();
            if (reservations == null)
            {
                return NotFound();
            }

            //var people = reservations.ReservationPersons;
            //var people = await _context.Reservations.Include(p => p.ReservationPersons).AsNoTracking();

            return View(reservations);
        }

        private bool ReservationsExists(Guid id)
        {
            return _context.Reservations.Any(e => e.ID == id);
        }
    }
}
