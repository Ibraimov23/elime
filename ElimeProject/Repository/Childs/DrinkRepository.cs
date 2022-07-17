using ElimeProject.Data;
using ElimeProject.Models;
using ElimeProject.Repository.Parents;
using ElimeProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ElimeProject.Repository.Childs
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DrinkRepository(ApplicationContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<DrinkesViewModel> Get()
        {
            IQueryable<Drink> drinkes = _dbContext.Drinkes.Include(p => p.DrinkCategory);
            DrinkesViewModel viewModel = new DrinkesViewModel
            {
                Drinkes = await drinkes.ToListAsync(),
                DrinkCategories = await _dbContext.DrinkCategories.ToListAsync()
            };
            return viewModel;
        }
        public async Task<DrinkesViewModel> GetSearch(string search)
        {
            IQueryable<Drink> drinkes = _dbContext.Drinkes.Include(p => p.DrinkCategory);
            drinkes = drinkes.Where(u =>
            u.Name.Contains(search));
            DrinkesViewModel viewModel = new DrinkesViewModel
            {
                Drinkes = await drinkes.ToListAsync(),
                DrinkCategories = await _dbContext.DrinkCategories.ToListAsync()
            };
            return viewModel;
        }
        public async Task<DrinkesViewModel> GetCategory(int? category)
        {
            IQueryable<Drink> drinkes = _dbContext.Drinkes.Include(p => p.DrinkCategory);
            drinkes = drinkes.Where(u =>
            u.DrinkCategoryId == category);
            DrinkesViewModel viewModel = new DrinkesViewModel
            {
                Drinkes = await drinkes.ToListAsync(),
                DrinkCategories = await _dbContext.DrinkCategories.ToListAsync()
            };
            return viewModel;
        }
        public async Task Create(Drink drink)
        {
            if (drink.Image != null)
            {
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "drinkes");
                string fileName = $"{Guid.NewGuid().ToString()}_{drink.Image.FileName}";
                string filePath = Path.Combine(folderPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    drink.Image.CopyTo(fileStream);
                }
                drink.ImageName = fileName;
            }
            _dbContext.Add(drink);
            await SaveAsync();
        }
        public async Task Update(Drink drink)
        {
            _dbContext.Entry(drink).State = EntityState.Modified;
            await SaveAsync();
        }
        public async Task Delete(int id)
        {
            Drink dish = await _dbContext.Drinkes.FindAsync(id);
            _dbContext.Drinkes.Remove(dish);
            await SaveAsync();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
