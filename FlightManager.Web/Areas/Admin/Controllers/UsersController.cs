﻿using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Web.Areas.Admin.Helpers;
using FlightManager.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users
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

            ApplicationUsersViewModel model = new ApplicationUsersViewModel();
            var selectedListItem = model.ResultsOnPageList.FirstOrDefault(x => x.Value == resultsOnPage.ToString()) ?? model.ResultsOnPageList.First(x => x.Value == "10");
            selectedListItem.Selected = true;

            var query = _userManager.Users.Cast<ApplicationUser>().Where(x => x.UserName != "admin@dev.local");

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Email.Contains(searchString)
                    || x.UserName.Contains(searchString)
                    || x.FirstName.Contains(searchString)
                    || x.LastName.Contains(searchString)
                );
            }
            model.ApplicationUser = await PaginatedListHelper<ApplicationUser>.CreateAsync(query.AsNoTracking(), pageNumber ?? 1, resultsOnPage ?? 1);
            if (model.ApplicationUser.TotalPages > 0 && (pageNumber ?? 1) > model.ApplicationUser.TotalPages)
            {
                model.ApplicationUser = await PaginatedListHelper<ApplicationUser>.CreateAsync(query.AsNoTracking(), model.ApplicationUser.TotalPages, resultsOnPage ?? 1);
            }

            return View(model);
        }
        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _userManager.FindByIdAsync(id);
            //var users = await _userManager.Users.Cast<ApplicationUser>()
                //.FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            UsersViewModel model = new UsersViewModel
            {
                UserName = users.UserName,
                Email = users.Email,
                FirstName = users.FirstName,
                LastName = users.LastName,
                SSN = users.SSN,
                Address = users.Address,
                PhoneNumber = users.PhoneNumber
            };

            return View(model);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsersViewModel model)
        {
            ApplicationUser user = new ApplicationUser();

            // Update it with the values from the view model
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.SSN = model.SSN;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, model.Password);
            //user.PasswordHash = checkUser.PasswordHash;

            if (ModelState.IsValid)
            {
                user.Id = ToString();
                //await _userManager.Add(users);
                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "Employee");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _userManager.FindByIdAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            UsersViewModel model = new UsersViewModel
            {
                UserName = users.UserName,
                Email = users.Email,
                FirstName = users.FirstName,
                LastName = users.LastName,
                SSN = users.SSN,
                Address = users.Address,
                PhoneNumber = users.PhoneNumber
            };
            return View(model);
        }


        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UsersViewModel model)
        {
            //if (id != model.Id)
            //{ 
            //    return NotFound();
            //}

            // Get the existing student from the db
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            
            // Update it with the values from the view model
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.SSN = model.SSN;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, model.Password);
            //user.PasswordHash = checkUser.PasswordHash;

            if (ModelState.IsValid)
            {
                try
                {
                    await _userManager.UpdateAsync(user);
                    //await _userManager.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _userManager.FindByIdAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            UsersViewModel model = new UsersViewModel
            {
                UserName = users.UserName,
                Password = users.PasswordHash,
                Email = users.Email,
                FirstName = users.FirstName,
                LastName = users.LastName,
                SSN = users.SSN,
                Address = users.Address,
                PhoneNumber = users.PhoneNumber
            };
            return View(model);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, UsersViewModel model)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            // Update it with the values from the view model
            user.UserName = model.UserName;
            user.PasswordHash = model.Password;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.SSN = model.SSN;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;

            //_userManager.Users.Cast<ApplicationUser>().Remove(users);
            await _userManager.DeleteAsync(user);
            //await _userManager.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(string id)
        {
            return _userManager.Users.Cast<ApplicationUser>().Any(e => e.Id == id);
        }
    }
}