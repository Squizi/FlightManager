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
using FlightManager.Web.Areas.Admin.Models;
using FlightManager.Web.Areas.Admin.Helpers;

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
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber, int? pageSize, int? resultsOnPage)
        {
            if (searchString != null || resultsOnPage != null)
            {
                pageNumber = 1;
            }
            if (searchString == null)
            {
                searchString = currentFilter;
            }
            if (resultsOnPage == null)
            {
                resultsOnPage = pageSize;
            }
            resultsOnPage = resultsOnPage ?? 10;
            ViewData["CurrentFilter"] = searchString;
            ViewData["PageSize"] = resultsOnPage;

            ReservationsViewModel model = new ReservationsViewModel();
            var selectedListItem = model.ResultsOnPageList.FirstOrDefault(x => x.Value == resultsOnPage.ToString()) ?? model.ResultsOnPageList.First(x => x.Value == "10");
            selectedListItem.Selected = true;

            var query = _context.Reservations.Where(f => f.ReservationNumber > 0);
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Email.Contains(searchString));
            }
            model.Reservations = await PaginatedListHelper<Reservations>.CreateAsync(query.AsNoTracking(), pageNumber ?? 1, resultsOnPage ?? 1);
            if (model.Reservations.TotalPages > 0 && (pageNumber ?? 1) > model.Reservations.TotalPages)
            {
                model.Reservations = await PaginatedListHelper<Reservations>.CreateAsync(query.AsNoTracking(), model.Reservations.TotalPages, resultsOnPage ?? 1);
            }

            return View(model);

            //return View(await _context.Reservations.ToListAsync());
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
