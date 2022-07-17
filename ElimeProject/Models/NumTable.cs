using System.ComponentModel.DataAnnotations;

namespace ElimeProject.Models
{
    public class NumTable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
    }
}
