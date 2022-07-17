using ElimeProject.Data;
using ElimeProject.Models;
using ElimeProject.Repository.Parents;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElimeProject.Repository.Childs
{
    public class DrinkCategoryRepository : IDrinkCategoryRepository
    {
        private readonly ApplicationContext _dbContext;

        public DrinkCategoryRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<DrinkCategory>> Get()
        {
            return await _dbContext.DrinkCategories.ToListAsync();
        }
        public async Task Create(DrinkCategory drinkCategory)
        {
            _dbContext.Add(drinkCategory);
            await SaveAsync();
        }
        public async Task Update(DrinkCategory drinkCategory)
        {
            _dbContext.Entry(drinkCategory).State = EntityState.Modified;
            await SaveAsync();
        }
        public async Task Delete(int id)
        {
            DrinkCategory drinkCategory = await _dbContext.DrinkCategories.FindAsync(id);
            _dbContext.DrinkCategories.Remove(drinkCategory);
            await SaveAsync();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
