using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElimeProject.Models
{
    public class Drink
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NutritValue { get; set; }
        [Required]
        public int Milliliter { get; set; }
        [Required]
        [StringLength(70, MinimumLength = 0, ErrorMessage = "Длина описания должна до 70 символов")]
        public string Description { get; set; }
        [Required]
        public int DrinkCategoryId { get; set; }
        public DrinkCategory DrinkCategory { get; set; }
        [Required]
        public int Price { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Image { get; set; }
        [NotMapped]
        public string Type { get { return "drink"; } }
    }
}
