using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CrudDelicious.Models;

namespace CrudDelicious.Controllers
{
    public class HomeController : Controller
    {
        private CrudDeliciousContext db;
        public HomeController(CrudDeliciousContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/dishes/new")]
        public IActionResult New()
        {
            return View("New");
        }

        [HttpGet("/Dishes")]
        public IActionResult All()
        {
            List<Dish> allDishes = db.Dishes.ToList();
            return View("All", allDishes);
        }

        [HttpGet("/dishes/{dishId}")]
        public IActionResult Details(int dishId)
        {
            Dish dish = db.Dishes.FirstOrDefault(p => p.DishId == dishId);

            if (dish == null)
            {
                return RedirectToAction("All");
            }

            return View("Details", dish);
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
            db.Dishes.Add(newDish);
            // db doesn't update until we run save changes
            // After SaveChanges, our newPost object now has it's PostId from the db.
            db.SaveChanges();

            return RedirectToAction("All");
        }

        [HttpPost("/Dishes/Delete/{dishId}")]
        public IActionResult Delete(int dishId)
        {
            Dish dish =db.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if (dish != null)
            {
                db.Dishes.Remove(dish);
                db.SaveChanges();
            }
            return RedirectToAction("All");
        }

        [HttpGet("/dishes/{dishId}/edit")]
        public IActionResult Edit(int dishId)
        {
            Dish dish = db.Dishes.FirstOrDefault(p => p.DishId == dishId);

            // It could be null if user types invalid id directly into address bar.
            if (dish == null)
            {
                return RedirectToAction("All");
            }

            return View("Edit", dish);
        }

        [HttpPost("/dishes/{dishId}/update")]
        public IActionResult Update(Dish editedDish, int dishId)
        {
            if (ModelState.IsValid == false)
            {
                editedDish.DishId = dishId;
                /* 
                Send back to the page with the form so error messages are
                displayed with the filled in input data.

                The Edit pages @model is a Post so we have to send it as the
                ViewModel and add the id to it since the Edit page needs access
                to the id.
                */
                return View("Edit", editedDish);
            }

            Dish dish = db.Dishes.FirstOrDefault(p => p.DishId == dishId);

            dish.Name = editedDish.Name;
            dish.Chef = editedDish.Chef;
            dish.Tastiness = editedDish.Tastiness;
            dish.Calories = editedDish.Calories;
            dish.Description = editedDish.Description;
            dish.UpdatedAt = DateTime.Now;

            db.Dishes.Update(dish);
            db.SaveChanges();

            /* 
            new {} means new dict of key value pairs, key = value where key
            matches param name of the method we are redirecting to.
            */
            return RedirectToAction("Details", new { dishId = dishId });
            // return Redirect($"/posts/{postId}");
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
