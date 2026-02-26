using System.ComponentModel.DataAnnotations;

namespace Mission08_0106.Models
{
    public class TaskItem
    {
        public int TaskItemId { get; set; }

        [Required(ErrorMessage = "Task name is required.")]
        public string TaskName { get; set; }

        public DateTime? DueDate { get; set; }  // Optional

        [Required(ErrorMessage = "Quadrant is required.")]
        [Range(1, 4, ErrorMessage = "Quadrant must be between 1 and 4.")]
        public int Quadrant { get; set; }

        public bool Completed { get; set; } = false;

        // Foreign Key
        public int CategoryId { get; set; }

        // Navigation Property
        public Category Category { get; set; }

    }
}