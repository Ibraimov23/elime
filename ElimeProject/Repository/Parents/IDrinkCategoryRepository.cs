using ElimeProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElimeProject.Repository.Parents
{
    public interface IDrinkCategoryRepository
    {
        Task<List<DrinkCategory>> Get();
        Task Create(DrinkCategory drinkCategory);
        Task Delete(int id);
        Task Update(DrinkCategory drinkCategory);
        Task SaveAsync();
    }
}
