using Microsoft.AspNetCore.Mvc;
using Mission08_0106.Models;
using System.Diagnostics;

namespace Mission08_0106.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
