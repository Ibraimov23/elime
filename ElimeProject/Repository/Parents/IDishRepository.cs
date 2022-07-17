using ElimeProject.Models;
using ElimeProject.ViewModels;
using System.Threading.Tasks;

namespace ElimeProject.Repository.Parents
{
    public interface IDishRepository
    {
        Task<DishesViewModel> Get();
        Task<DishesViewModel> GetSearch(string search);
        Task<DishesViewModel> GetCategory(int? category);
        Task Create(Dish dish);
        Task Delete(int id);
        Task Update(Dish dish);
        Task SaveAsync();
    }
}
