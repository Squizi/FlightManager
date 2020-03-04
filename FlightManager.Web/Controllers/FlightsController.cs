using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Web.Helpers;
using FlightManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Web.Controllers
{
    public class FlightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightsController(ApplicationDbContext context)
        {
            _context = context;
        }

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
        }
    }
}