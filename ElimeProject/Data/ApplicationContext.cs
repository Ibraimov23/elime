using ElimeProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElimeProject.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<NumTable> NumTables { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Drink> Drinkes { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<DrinkCategory> DrinkCategories { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }
}
