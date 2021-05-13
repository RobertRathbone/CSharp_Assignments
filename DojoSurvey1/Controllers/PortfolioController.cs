using Microsoft.AspNetCore.Mvc;
using System;

namespace Portfolio
{
    public class PortfolioController : Controller
    {
        [Route("")]
        [HttpGet]

        public ViewResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [Route("projects")]
        public IActionResult Form(string name, string languageSelect, string locationSelect, string textArea)
        {
           ViewBag.name = name;
           ViewBag.language = languageSelect;
           ViewBag.location = locationSelect;
           ViewBag.text = textArea;
           return View();
        }

        [Route("contact")]
        [HttpGet]
        public string contact()
        {
            return ("This is my contact!");
        }
    }
}