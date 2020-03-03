﻿using Microsoft.AspNetCore.Identity;

namespace FlightManager.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string Address { get; set; }
    }
}
