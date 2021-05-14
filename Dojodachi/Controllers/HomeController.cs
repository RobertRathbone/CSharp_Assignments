using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public Dachi Jim = new Dachi()
            {
                Fullness = 20,
                Happiness = 20,
                Energy = 50,
                Meals = 3,   
                
            };

        public void setSession(Dachi Jim)
        {
            HttpContext.Session.SetInt32("Fullness", 20);
            HttpContext.Session.SetInt32("Happiness", 20);
            HttpContext.Session.SetInt32("Energy", 50);
            HttpContext.Session.SetInt32("Meals", 3);
            Console.Write("Session");
        }

        public int update(Dachi Jim)
        {
            Random rnd = new Random();
            int addme = 4 + rnd.Next(1,7); 
            if (rnd.Next(0,3) == 0)
            {
                return 0;
            }
            return addme;
        } 

        public bool didIWin(Dachi Jim)
        {
            int? F = HttpContext.Session.GetInt32("Fullness");
            int? E = HttpContext.Session.GetInt32("Energy");
            int? H = HttpContext.Session.GetInt32("Happiness");
            Console.Write("DidIWin?");
            if (F>=100)
            { 
                if (E>=100)
                {
                    if (H>=100)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool didILose(Dachi Jim)
        {
            int? F = HttpContext.Session.GetInt32("Fullness");
            int? E = HttpContext.Session.GetInt32("Energy");
            int? H = HttpContext.Session.GetInt32("Happiness");
            if (F <=0)
            {
                return true;
            }
            if (H<=0)
            {
                return true;
            }
            return false;

        }

        [HttpGet("feed")]
        public ViewResult feed(Dachi Jim)
        {
            int? CurrentMeals = HttpContext.Session.GetInt32("Meals");
            CurrentMeals = CurrentMeals -1;
            HttpContext.Session.SetInt32("Meals", ((int)(CurrentMeals)));
            int? CurrentFullness = HttpContext.Session.GetInt32("Fullness");
            int bonus = update(Jim);
            CurrentFullness = CurrentFullness + bonus;
            HttpContext.Session.SetInt32("Fullness", ((int)(CurrentFullness)));
            ViewBag.Jim =$"You fed your Dojodachi and Fullness increased {bonus} and meals went down 1";
            if (didILose(Jim))
            {
                return View("failure", Jim);
            }
            if (didIWin(Jim))
            {
                return View("success", Jim);
            }
            return View("Dojo", Jim);
        }
        [HttpGet("play")]
        public ViewResult play(Dachi Jim)
        {
            int? CurrentEnergy = HttpContext.Session.GetInt32("Energy");
            CurrentEnergy = CurrentEnergy -5;
            HttpContext.Session.SetInt32("Energy", ((int)(CurrentEnergy)));
            int? CurrentHappiness = HttpContext.Session.GetInt32("Happiness");
            int bonus = update(Jim);
            CurrentHappiness = CurrentHappiness + bonus;
            HttpContext.Session.SetInt32("Happiness", ((int)(CurrentHappiness)));
            ViewBag.Jim =$"You played with your Dojodachi and Happiness increased {bonus} and Energy went down 5";
            if (didILose(Jim))
            {
                return View("failure", Jim);
            }
            if (didIWin(Jim))
            {
                return View("success", Jim);
            }
            return View("Dojo", Jim);
        }
        [HttpGet("work")]
        public ViewResult work(Dachi Jim)
        {
            int? CurrentEnergy = HttpContext.Session.GetInt32("Energy");
            CurrentEnergy = CurrentEnergy -5;
            HttpContext.Session.SetInt32("Energy", ((int)(CurrentEnergy)));
            Random rnd = new Random();
            int bonus = rnd.Next(1,3);
            int? CurrentMeals = HttpContext.Session.GetInt32("Meals");
            CurrentMeals = CurrentMeals + bonus;
            HttpContext.Session.SetInt32("Meals", ((int)(CurrentMeals)));
            ViewBag.Jim =$"Your Dojodachi worked and meals increased {bonus} and Energy went down 5";
            if (didILose(Jim))
            {
                return View("failure", Jim);
            }
            if (didIWin(Jim))
            {
                return View("success", Jim);
            }
            return View("Dojo", Jim);
        }

        [HttpGet("sleep")]
        public ViewResult sleep(Dachi Jim)
        {
            int? CurrentEnergy = HttpContext.Session.GetInt32("Energy");
            CurrentEnergy = CurrentEnergy +15;
            HttpContext.Session.SetInt32("Energy", ((int)(CurrentEnergy)));
            int? CurrentFullness = HttpContext.Session.GetInt32("Fullness");
            CurrentFullness = CurrentFullness -5;
            HttpContext.Session.SetInt32("Fullness", ((int)(CurrentFullness)));
            int? CurrentHappiness = HttpContext.Session.GetInt32("Happiness");
            CurrentHappiness = CurrentHappiness -5;
            HttpContext.Session.SetInt32("Happiness", ((int)(CurrentHappiness)));
            ViewBag.Jim ="Your Dojodachi slept and Energy increased 15, Fullness decreased by 5 and Energy also went down 5";
            if (didILose(Jim))
            {
                return View("failure", Jim);
            }
            if (didIWin(Jim))
            {
                return View("success", Jim);
            }
            return View("Dojo", Jim);


        }
        [HttpGet("Dojo")]
        public IActionResult Dojo()
        {
            setSession(Jim);
            Console.Write("in Dojo");
            return View("Dojo", Jim);
        }

        [HttpGet("restart")]
        public ViewResult restart(Dachi Jim)
        {
            HttpContext.Session.Clear();
            setSession(Jim);
            return View("Dojo", Jim);
        }

        [HttpGet("success")]
        public IActionResult success(Dachi Jim)
        {
            setSession(Jim);
            return View("success", Jim);
        }

        [HttpGet("failure")]
        public IActionResult failure(Dachi Jim)
        {
            setSession(Jim);
            return View("failure", Jim);
        }

        public IActionResult Privacy()
        {
            setSession(Jim);
            return View("Dojo", Jim);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
