using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Delicious.Models;

namespace Delicious.Controllers
{
    public class HomeController : Controller
    {
        private DeliciousContext db;
        public HomeController(DeliciousContext context)
        {
            db = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/dishes/create")]
        public IActionResult Create(Dish newDish)
        {
            if (ModelState.IsValid == false)
            {
                /* 
                Send back to the page with the form so error messages are
                displayed with the filled in input data.
                */
                return View("New");
            }

            // ModelState IS valid

            /* 
            This Add method auto generates SQL code:
            INSERT INTO posts (Topic, Body, ImgUrl, CreatedAt, UpdatedAt)
            VALUES ("topic data", "body data", "img url data", NOW(), NOW());
            */
            db.deliciousdb.Add(newDish);
            // db doesn't update until we run save changes
            // After SaveChanges, our newPost object now has it's PostId from the db.
            db.SaveChanges();
            return View("New");

            // return RedirectToAction("All");
        }

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
