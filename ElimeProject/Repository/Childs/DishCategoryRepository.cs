using ElimeProject.Data;
using ElimeProject.Models;
using ElimeProject.Repository.Parents;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElimeProject.Repository.Childs
{
    public class DishCategoryRepository : IDishCategoryRepository
    {
        private readonly ApplicationContext _dbContext;

        public DishCategoryRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<DishCategory>> Get()
        {
            return await _dbContext.DishCategories.ToListAsync();
        }
        public async Task Create(DishCategory dishCategory)
        {
            _dbContext.Add(dishCategory);
            await SaveAsync();
        }
        public async Task Update(DishCategory dishCategory)
        {
            _dbContext.Entry(dishCategory).State = EntityState.Modified;
            await SaveAsync();
        }
        public async Task Delete(int id)
        {
            DishCategory dishCategory = await _dbContext.DishCategories.FindAsync(id);
            _dbContext.DishCategories.Remove(dishCategory);
            await SaveAsync();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
