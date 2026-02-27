// Controller for Task CRUD operations

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mission08_0106.Models;
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
            var vm = new TaskFormVM
            {
                Categories = _repo.Categories
                    .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.CategoryName })
                    .ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(TaskFormVM formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Categories = _repo.Categories
                    .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.CategoryName })
                    .ToList();
                return View(formData);
            }

            // Convert TaskFormVM to TaskItem entity
            var taskItem = new TaskItem
            {
                TaskName = formData.Task,
                DueDate = formData.DueDate,
                Quadrant = formData.Quadrant,
                CategoryId = formData.CategoryId,
                Completed = formData.Completed
            };

            _repo.AddTask(taskItem);
            _repo.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var taskToEdit = _repo.Tasks
                .Include(t => t.Category)
                .Single(t => t.TaskItemId == id);

            // Convert TaskItem to TaskFormVM for the view
            var formData = new TaskFormVM
            {
                Id = taskToEdit.TaskItemId,
                Task = taskToEdit.TaskName,
                DueDate = taskToEdit.DueDate,
                Quadrant = taskToEdit.Quadrant,
                CategoryId = taskToEdit.CategoryId,
                Completed = taskToEdit.Completed,
                Categories = _repo.Categories
                    .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.CategoryName })
                    .ToList()
            };
            return View("Edit", formData);
        }

        [HttpPost]
        public IActionResult Edit(TaskFormVM formData)
        {
            if (!ModelState.IsValid)
            {
                formData.Categories = _repo.Categories
                    .Select(c => new SelectListItem { Value = c.CategoryId.ToString(), Text = c.CategoryName })
                    .ToList();
                return View("Edit", formData);
            }

            // Convert TaskFormVM back to TaskItem entity
            var taskItem = new TaskItem
            {
                TaskItemId = formData.Id ?? 0,
                TaskName = formData.Task,
                DueDate = formData.DueDate,
                Quadrant = formData.Quadrant,
                CategoryId = formData.CategoryId,
                Completed = formData.Completed
            };

            _repo.UpdateTask(taskItem);
            _repo.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var taskToDelete = _repo.Tasks
                .Include(t => t.Category)
                .Single(t => t.TaskItemId == id);

            return View(taskToDelete);
        }

        [HttpPost]
        public IActionResult Delete(TaskItem taskToDelete)
        {
            _repo.DeleteTask(taskToDelete);
            _repo.Save();

            return RedirectToAction("Index");
        }

        // POST: Mark a task as completed (required by assignment)
        [HttpPost]
        public IActionResult MarkComplete(int id)
        {
            var task = _repo.Tasks.Single(t => t.TaskItemId == id);
            task.Completed = true;
            _repo.Save();

            return RedirectToAction("Index");
        }
    }
}
