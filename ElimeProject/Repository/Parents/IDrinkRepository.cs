using ElimeProject.Models;
using ElimeProject.ViewModels;
using System.Threading.Tasks;

namespace ElimeProject.Repository.Parents
{
    public interface IDrinkRepository
    {
        Task<DrinkesViewModel> Get();
        Task<DrinkesViewModel> GetSearch(string search);
        Task<DrinkesViewModel> GetCategory(int? category);
        Task Create(Drink dish);
        Task Delete(int id);
        Task Update(Drink dish);
        Task SaveAsync();
    }
}
