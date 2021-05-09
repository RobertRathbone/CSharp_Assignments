using Microsoft.AspNetCore.Mvc;

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
        
        [Route("projects")]
        [HttpGet]
        public string projects()
        {
            return ("This is my projects!");
        }

        [Route("contact")]
        [HttpGet]
        public string contact()
        {
            return ("This is my contact!");
        }
    }
}