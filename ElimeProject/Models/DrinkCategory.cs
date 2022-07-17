using System.ComponentModel.DataAnnotations;

namespace ElimeProject.Models
{
    public class DrinkCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
