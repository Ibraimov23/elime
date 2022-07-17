using Microsoft.AspNetCore.Mvc;

namespace ElimeProject.Controllers
{
    public class BasketsController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Layout"] = "_Layout2";
            ViewData["color"] = "background-color: #f4f4f4;";
            return View();
        }

    }
}
