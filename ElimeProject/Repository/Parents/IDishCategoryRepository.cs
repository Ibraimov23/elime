using ElimeProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElimeProject.Repository.Parents
{
    public interface IDishCategoryRepository
    {
        Task<List<DishCategory>> Get();
        Task Create(DishCategory dishCategory);
        Task Delete(int id);
        Task Update(DishCategory dishCategory);
        Task SaveAsync();
    }
}
