using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChefsandDishes2.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsandDishes2.Controllers
{
    public class HomeController : Controller
    {
        private ChefsandDishes2Context db;
        public HomeController(ChefsandDishes2Context context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet("/dishes/new")]
        public IActionResult DishNew()
        {
            ViewBag.ChefsF = db.Chefs.ToList();
            return View("DishNew");
        }

        [HttpGet("/Dishes")]
        public IActionResult DishAll()
        {
            List<Dish> allDishes = db.Dishes.ToList();
            return View("DishAll", allDishes);
        }

        [HttpGet("/dishes/{dishId}")]
        public IActionResult Details(int dishId)
        {
            Dish dish = db.Dishes.FirstOrDefault(p => p.DishId == dishId);

            if (dish == null)
            {
                return RedirectToAction("DishAll");
            }

            return View("DishDetails", dish);
        }

        [HttpPost("/dishes/create")]
        public IActionResult DishCreate(Dish newDish)
        {
            if (ModelState.IsValid == false)
            {
                return View("DishNew");
            }
            db.Dishes.Add(newDish);
            db.SaveChanges();

            return RedirectToAction("DishAll");
        }

        [HttpPost("/Dishes/Delete/{dishId}")]
        public IActionResult DishDelete(int dishId)
        {
            Dish dish =db.Dishes.FirstOrDefault(d => d.DishId == dishId);
            if (dish != null)
            {
                db.Dishes.Remove(dish);
                db.SaveChanges();
            }
            return RedirectToAction("DishAll");
        }

        [HttpGet("/dishes/{dishId}/edit")]
        public IActionResult DishEdit(int dishId)
        {
            Dish dish = db.Dishes.FirstOrDefault(p => p.DishId == dishId);

            // It could be null if user types invalid id directly into address bar.
            if (dish == null)
            {
                return RedirectToAction("DishAll");
            }

            return View("DishEdit", dish);
        }

        [HttpPost("/dishes/{dishId}/update")]
        public IActionResult DishUpdate(Dish editedDish, int dishId)
        {
            if (ModelState.IsValid == false)
            {
                editedDish.DishId = dishId;
                return View("DishEdit", editedDish);
            }

            Dish dish = db.Dishes.FirstOrDefault(p => p.DishId == dishId);

            dish.Name = editedDish.Name;
            // dish.Chef = editedDish.Chef;
            dish.Tastiness = editedDish.Tastiness;
            dish.Calories = editedDish.Calories;
            dish.Description = editedDish.Description;
            dish.UpdatedAt = DateTime.Now;

            db.Dishes.Update(dish);
            db.SaveChanges();
            return RedirectToAction("Details", new { dishId = dishId });
            // return Redirect($"/posts/{postId}");
        }

        [HttpPost("/chef/create")]
        public IActionResult ChefCreate(Chef newChef)
        {
            if (ModelState.IsValid == false)
            {
                return View("ChefNew");
            }

            db.Chefs.Add(newChef);
            db.SaveChanges();

            return RedirectToAction("ChefAll");
        }
        [HttpGet("/chef/new")]
        public IActionResult ChefNew()
        {
            return View("ChefNew");
        }

        [HttpGet("/Chefs")]
        public IActionResult ChefAll()
        {
            List<Chef> allChefs = db.Chefs.ToList();
            return View("ChefAll", allChefs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
