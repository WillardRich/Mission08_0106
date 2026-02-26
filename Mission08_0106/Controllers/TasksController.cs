// Controller for Task CRUD operations
// Requires Role #1 to implement: ITaskRepository interface

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

        // GET: Display all incomplete tasks in Quadrants view
        public IActionResult Index()
        {
            // Only display tasks that have NOT been completed (per requirements)
            var incompleteTasks = _repo.Tasks
                .Include(t => t.Category)
                .Where(t => !t.Completed)
                .ToList();

            return View(incompleteTasks);
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

            _repo.AddTask(newTask);
            _repo.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var taskToEdit = _repo.Tasks
                .Include(t => t.Category)
                .Single(t => t.TaskId == id);

            ViewBag.Categories = _repo.Categories.ToList();
            return View("Edit", taskToEdit);
        }

        [HttpPost]
        public IActionResult Edit(TaskFormVM updatedTask)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _repo.Categories.ToList();
                return View("Edit", updatedTask);
            }

            _repo.UpdateTask(updatedTask);
            _repo.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var taskToDelete = _repo.Tasks
                .Include(t => t.Category)
                .Single(t => t.TaskId == id);

            return View(taskToDelete);
        }

        [HttpPost]
        public IActionResult Delete(TaskFormVM taskToDelete)
        {
            _repo.DeleteTask(taskToDelete);
            _repo.Save();

            return RedirectToAction("Index");
        }

        // POST: Mark a task as completed (required by assignment)
        [HttpPost]
        public IActionResult MarkComplete(int id)
        {
            var task = _repo.Tasks.Single(t => t.TaskId == id);
            task.Completed = true;
            _repo.Save();

            return RedirectToAction("Index");
        }
    }
}
