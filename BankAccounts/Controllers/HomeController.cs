using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BankAccounts.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Controllers
{
    public class HomeController : Controller
    {
        private BankAccountsContext db;
        public HomeController(BankAccountsContext context)
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

        [HttpGet("/success")]
        public IActionResult Success()
        {
            if (!isLoggedIn)
            {
                return View("Index");
            }
            List<Transaction> transactions = db.Transactions
            // populates each Message with its related User object (Creator)
            .Include(users => users.Customer)
            .ToList();
            decimal total = 0;
            foreach (Transaction transaction in transactions)
            {
                total = total + transaction.Amount;
            }
            ViewBag.total = total;

            ViewBag.Transactions = transactions;
            return View("Success");
        }

        [HttpPost("/Deposit")]
        public IActionResult Deposit(Decimal Amount)
        {
            if (ModelState.IsValid == false)
            {
                return View("Index");
            }
            List<Transaction> transactions = db.Transactions
            // populates each Message with its related User object (Creator)
            .Include(users => users.Customer)
            .ToList();
            decimal total = 0;
            foreach (Transaction transaction in transactions)
            {
                total = total + transaction.Amount;
            }
            if (total + Amount < 0)
            {
                return RedirectToAction("Success");
            }
            Transaction newTransaction = new Transaction();
            newTransaction.Amount = Amount;
            newTransaction.UserId = (int)uid;
            db.Transactions.Add(newTransaction);
            db.SaveChanges();
            return RedirectToAction("Success"); 
        }

        // [HttpGet("/users/{userId}")]
        // public IActionResult Details(int userId)
        // {
        //     User user = db.Users.Include(user => user.Posts).FirstOrDefault(u => u.UserId == userId);

        //     if (user == null)
        //     {
        //         return RedirectToAction("All", "Posts");
        //     }

        //     return View("Details", user);
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

