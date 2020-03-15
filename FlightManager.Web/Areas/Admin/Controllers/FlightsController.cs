using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Web.Areas.Admin.Models;
using FlightManager.Web.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class FlightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Flights
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

            FlightsViewModel model = new FlightsViewModel();
            var selectedListItem = model.ResultsOnPageList.FirstOrDefault(x => x.Value == resultsOnPage.ToString()) ?? model.ResultsOnPageList.First(x => x.Value == "10");
            selectedListItem.Selected = true;

            var query = _context.Flight.Where(f => f.StartTime > DateTime.Now);
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.StartLocation.Contains(searchString)
                                       || x.Destination.Contains(searchString));
            }
            model.Flights = await PaginatedListHelper<Flights>.CreateAsync(query.AsNoTracking(), pageNumber ?? 1, resultsOnPage ?? 1);
            if (model.Flights.TotalPages > 0 && (pageNumber ?? 1) > model.Flights.TotalPages)
            {
                model.Flights = await PaginatedListHelper<Flights>.CreateAsync(query.AsNoTracking(), model.Flights.TotalPages, resultsOnPage ?? 1);
            }

            return View(model);
            //return View(await _context.Flight.ToListAsync());
        }

        // GET: Flights/Details/5
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flights = await _context.Flight
                .FirstOrDefaultAsync(m => m.ID == id);
            if (flights == null)
            {
                return NotFound();
            }

            return View(flights);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StartLocation,Destination,StartTime,EndTime,PlaneType,FlightNumber,PilotName,CustomerCapacity,CustomerCapacityBussinessClass")] Flights flights)
        {
            if (ModelState.IsValid)
            {
                flights.ID = Guid.NewGuid();
                _context.Add(flights);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flights);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flights = await _context.Flight.FindAsync(id);
            if (flights == null)
            {
                return NotFound();
            }
            return View(flights);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,StartLocation,Destination,StartTime,EndTime,PlaneType,FlightNumber,PilotName,CustomerCapacity,CustomerCapacityBussinessClass")] Flights flights)
        {
            if (id != flights.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flights);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightsExists(flights.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flights);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flights = await _context.Flight
                .FirstOrDefaultAsync(m => m.ID == id);
            if (flights == null)
            {
                return NotFound();
            }

            return View(flights);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var flights = await _context.Flight.FindAsync(id);
            _context.Flight.Remove(flights);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightsExists(Guid id)
        {
            return _context.Flight.Any(e => e.ID == id);
        }
    }
}
