using FlightManager.Data.Models;
using FlightManager.Web.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Web.Areas.Admin.Models
{
    public class ReservationsViewModel
    {
        public ReservationsViewModel()
        {
            ResultsOnPageList = new List<SelectListItem>
            {
                new SelectListItem("10", "10"),
                new SelectListItem("25", "25"),
                new SelectListItem("50", "50")
            };
        }

        public Reservations model { get; set; }
        public PaginatedListHelper<Reservations> Reservations { get; set; }
        public List<SelectListItem> ResultsOnPageList { get; set; }
    }
}
