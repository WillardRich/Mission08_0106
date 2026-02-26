// PLACEHOLDER CONTROLLER: Temporary until Role #1 provides models and full controller logic.
// POST actions only redirect; no database or repository code. Replace when real TasksController is implemented.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission08_0106.ViewModels;

namespace Mission08_0106.Controllers
{
    public class TasksController : Controller
    {

        private ITaskRepository _repo;

        public TasksController(ITaskRepository temp)
        {
            _repo = temp;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _repo.Categories.ToList();

            return View(new TaskFormVM());
        }

        [HttpPost]
        public IActionResult Create(TaskFormVM newTask)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _repo.Categories.ToList();
                return View(newTask);
            }

            _repo.Tasks.Add(newTask);
            _repo.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var taskToEdit = _repo.Tasks
                .Include(t => t.Category)
                .Single(t => t.Id == id);
            
            ViewBag.Categories = _repo.Categories.ToList();
            return View("Create", taskToEdit);
        }

        [HttpPost]
        public IActionResult Edit(TaskFormVM updatedTask)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _repo.Categories.ToList();
                return View("Create", updatedTask);
            }

            _repo.Tasks.Update(updatedTask);
            _repo.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var taskToDelete = _repo.Tasks
                .Include(t => t.Category)
                .Single(t => t.Id == id);

            return View(taskToDelete);
        }

        [HttpPost]
        public IActionResult Delete(TaskFormVM taskToDelete)
        {
            _repo.Tasks.Delete(taskToDelete);
            _repo.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
