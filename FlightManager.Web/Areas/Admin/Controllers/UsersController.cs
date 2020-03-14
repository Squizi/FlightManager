using FlightManager.Data;
using FlightManager.Data.Models;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.Cast<ApplicationUser>().ToListAsync());
        }
        // GET: Users/Details/5
        [Authorize(Roles = "Admin")]
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
            user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, model.Password);
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.SSN = model.SSN;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            //user.PasswordHash = checkUser.PasswordHash;

            if (ModelState.IsValid)
            {
                user.Id = ToString();
                //await _userManager.Add(users);
                await _userManager.CreateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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
            user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, model.Password);
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.SSN = model.SSN;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
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