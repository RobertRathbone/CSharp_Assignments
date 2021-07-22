using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingPlannerContext db;
        public HomeController(WeddingPlannerContext context)
        {
            db = context;
        }

        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
                
            }
        }

        private bool isLoggedIn
        {
            get
            {
                return uid != null;
            }
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            if (isLoggedIn)
            {
                return RedirectToAction("Success");
            }
            return View();
        }
        [HttpGet("/register")]
        public ViewResult Register()
        {
            return View("Register");
        }

        [HttpPost("/register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                bool existingUser = db.Users.Any(u => u.Email == newUser.Email);

                if (existingUser)
                {

                    ModelState.AddModelError("Email", "is taken.");
                }
            }


            if (ModelState.IsValid == false)
            {
                // So error messages will be displayed.
                return View("Index");
            }

            // hash the password
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password);

            db.Users.Add(newUser);
            db.SaveChanges();

            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            HttpContext.Session.SetString("FirstName", newUser.FirstName);
            return RedirectToAction("Success");
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginUser loginUser)
        {
            /* 
            For security, don't reveal what was invalid.
            You can make your error messages more specific to help with testing
            but on a live site it should be ambiguous.
            */
            string genericErrMsg = "Invalid Email or Password";

            if (ModelState.IsValid == false)
            {
                // So error messages will be displayed.
                return View("Index");
            }

            User dbUser = db.Users.FirstOrDefault(user => user.Email == loginUser.LoginEmail);

            if (dbUser == null)
            {
                ModelState.AddModelError("LoginEmail", genericErrMsg);
                Console.WriteLine(new String('*', 30) + "Login: Email not found");
                // So error messages will be displayed.
                return View("Index");
            }

            // User found b/c the above didn't return.
            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
            PasswordVerificationResult pwCompareResult = hasher.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

            if (pwCompareResult == 0)
            {
                ModelState.AddModelError("LoginEmail", genericErrMsg);
                Console.WriteLine(new String('*', 30) + "Login: Password incorrect.");
                return View("Index");
            }

            // Password matched.
            HttpContext.Session.SetInt32("UserId", dbUser.UserId);
            HttpContext.Session.SetString("FirstName", dbUser.FirstName);
            return RedirectToAction("Success");
        }

        [HttpPost("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet("/Dashboard")]
        public IActionResult Success()
        {
            if (!isLoggedIn)
            {
                return View("Index");
            }

    // Black Belt?
            List<Wedding> expiredWeddings = db.Weddings
                .Where(w => w.Date < DateTime.Now)
                .ToList();

            db.RemoveRange(expiredWeddings);
            db.SaveChanges();
    // Black Belt?

            List<Wedding> wedding = db.Weddings
            .Include(w=>w.Attendees)
            .ToList();
            // if (db.Weddings.Include(i=>i.Attendees).FirstOrDefault(j=>j.UserId == uid) != null)
            // {
            //     bool Imgoing = true;
            //     HttpContext.Session.SetInt32("UserName", "Samantha");
            // }   
            

            return View("Dashboard", wedding);
        }

        [HttpGet("Plan")]
        public IActionResult PlanWedding()
        {
        
            return View("Plan");
        }

        [HttpPost("Create")]
        public IActionResult Create(Wedding newWedding)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("hello, it's me...");
                newWedding.UserId = uid.Value;
                User user = db.Users.FirstOrDefault(t => t.UserId == uid.Value);
                newWedding.CreatedBy = user;
                db.Weddings.Add(newWedding);
                db.SaveChanges();
                return Redirect($"/Details/{newWedding.WeddingId}");
            }
            return View("Plan");
        }

        [HttpGet("Details/{weddingId}")]
        // Maybe should be int WeddingId
        public IActionResult Details( int weddingId)
        {
            Wedding wedding = db.Weddings
            .Include(w => w.Attendees)
            .ThenInclude(a => a.Attendee)
            .FirstOrDefault(w => w.WeddingId == weddingId);
        
            return View("Details", wedding);
        }

        [HttpPost("/RSVP/{weddingId}")]
        public IActionResult RSVP(int weddingId)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("index");
            }

            WeddingAttendees existingRSVP = db.Attendees
                .FirstOrDefault(rsvp => rsvp.UserId == uid && rsvp.WeddingId == weddingId);

            if (existingRSVP == null)
            {
                WeddingAttendees rsvp = new WeddingAttendees()
                {
                    WeddingId = weddingId,
                    UserId = (int)uid
                };

                db.Attendees.Add(rsvp);
            }
            else
            {
                db.Attendees.Remove(existingRSVP);
            }

            db.SaveChanges();
            return RedirectToAction("Success");
        }

        [HttpGet("Delete/{weddingId}")]
        public IActionResult Delete(int weddingId)
        {
            Wedding wedding = db.Weddings.FirstOrDefault(t => t.WeddingId == weddingId);

            if (wedding == null)
            {
                return RedirectToAction("Success");
            }

            db.Weddings.Remove(wedding);
            db.SaveChanges();
            return RedirectToAction("Success");
        }
        // [HttpGet("/RSVP/{weddingId}")]
        // public IActionResult RSVP(int weddingId)
        // {
        //     Wedding wedding = db.Weddings
        //         .Include(w => w.CreatedBy)
        //         .Include(trip => trip.TripDestinations)
        //         .ThenInclude(td => td.DestinationMedia)
        //         .FirstOrDefault(t => t.TripId == tripId);
        // }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
