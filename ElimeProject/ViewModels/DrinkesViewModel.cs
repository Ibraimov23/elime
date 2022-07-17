using ElimeProject.Models;
using System.Collections.Generic;

namespace ElimeProject.ViewModels
{
    public class DrinkesViewModel
    {
        public IEnumerable<Drink> Drinkes { get; set; }
        public IEnumerable<DrinkCategory> DrinkCategories { get; set; }
    }
}
