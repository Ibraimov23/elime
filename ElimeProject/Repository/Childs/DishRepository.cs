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
    public class DishRepository : IDishRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DishRepository(ApplicationContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<DishesViewModel> Get()
        {
            IQueryable<Dish> dishes = _dbContext.Dishes.Include(p => p.DishCategory);
            DishesViewModel viewModel = new DishesViewModel
            {
                Dishes = await dishes.ToListAsync(),
                DishCategories = await _dbContext.DishCategories.ToListAsync()
            };
            return viewModel;
        }
        public async Task<DishesViewModel> GetSearch(string search)
        {
            IQueryable<Dish> dishes = _dbContext.Dishes.Include(p => p.DishCategory); ;
            dishes = dishes.Where(u =>
            u.Name.Contains(search));
            DishesViewModel viewModel = new DishesViewModel
            {
                Dishes = await dishes.ToListAsync(),
                DishCategories = await _dbContext.DishCategories.ToListAsync()
            };
            return viewModel;
        }
        public async Task<DishesViewModel> GetCategory(int? category)
        {
            IQueryable<Dish> dishes = _dbContext.Dishes.Include(p => p.DishCategory);
            dishes = dishes.Where(u =>
            u.DishCategoryId == category);
            DishesViewModel viewModel = new DishesViewModel
            {
                Dishes = await dishes.ToListAsync(),
                DishCategories = await _dbContext.DishCategories.ToListAsync()
            };
            return viewModel;
        }
        public async Task Create(Dish dish)
        {
            if (dish.Image != null)
            {
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "dishes");
                string fileName = $"{Guid.NewGuid().ToString()}_{dish.Image.FileName}";
                string filePath = Path.Combine(folderPath, fileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    dish.Image.CopyTo(fileStream);
                }
                dish.ImageName = fileName;
            }
            _dbContext.Add(dish);
            await SaveAsync();
        }
        public async Task Update(Dish dish)
        {
            _dbContext.Entry(dish).State = EntityState.Modified;
            await SaveAsync();
        }
        public async Task Delete(int id)
        {
            Dish dish = await _dbContext.Dishes.FindAsync(id);
            _dbContext.Dishes.Remove(dish);
            await SaveAsync();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
