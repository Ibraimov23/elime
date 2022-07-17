using ElimeProject.Models;
using System.Collections.Generic;

namespace ElimeProject.ViewModels
{
    public class DishesViewModel
    {
        public IEnumerable<Dish> Dishes { get; set; }
        public IEnumerable<DishCategory> DishCategories { get; set; }
    }
}
