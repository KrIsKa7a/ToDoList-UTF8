using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        public string Comments { get; set; }
    }
}