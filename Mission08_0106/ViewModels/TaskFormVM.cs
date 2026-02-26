// PLACEHOLDER: Temporary view model for Create/Edit task form until Role #1 provides real entity/viewmodel.
// Replace with project view model when models and controllers are finalized.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mission08_0106.ViewModels
{
    public class TaskFormVM
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Task description is required.")]
        [Display(Name = "Task")]
        public string Task { get; set; } = string.Empty;

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Quadrant is required.")]
        [Range(1, 4, ErrorMessage = "Quadrant must be 1, 2, 3, or 4.")]
        [Display(Name = "Quadrant")]
        public int Quadrant { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Completed")]
        public bool Completed { get; set; }

        /// <summary>Options for the category dropdown (populated by controller).</summary>
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
