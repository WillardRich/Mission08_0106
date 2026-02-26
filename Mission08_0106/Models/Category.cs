using System.ComponentModel.DataAnnotations;

namespace Mission08_0106.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        // Navigation Property (One-to-Many)
        public List<TaskItem> TaskItems { get; set; }
    }
}