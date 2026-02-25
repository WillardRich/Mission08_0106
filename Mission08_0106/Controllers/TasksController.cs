// PLACEHOLDER CONTROLLER: Temporary until Role #1 provides models and full controller logic.
// POST actions only redirect; no database or repository code. Replace when real TasksController is implemented.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission08_0106.ViewModels;

namespace Mission08_0106.Controllers
{
    public class TasksController : Controller
    {
        private static IEnumerable<SelectListItem> GetPlaceholderCategories()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Home" },
                new SelectListItem { Value = "2", Text = "School" },
                new SelectListItem { Value = "3", Text = "Work" },
                new SelectListItem { Value = "4", Text = "Church" }
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new TaskFormVM { Categories = GetPlaceholderCategories() };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskFormVM model)
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vm = new TaskFormVM
            {
                Id = id,
                Categories = GetPlaceholderCategories()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskFormVM model)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
