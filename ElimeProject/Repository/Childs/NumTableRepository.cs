using ElimeProject.Data;
using ElimeProject.Models;
using ElimeProject.Repository.Parents;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElimeProject.Repository.Childs
{
    public class NumTableRepository : INumTableRepository
    {
        private readonly ApplicationContext _dbContext;

        public NumTableRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<NumTable>> Get()
        {
            return await _dbContext.NumTables.ToListAsync();
        }
        public async Task Create(NumTable numTable)
        {
            _dbContext.Add(numTable);
            await SaveAsync();
        }
        public async Task Update(NumTable numTable)
        {
            _dbContext.Entry(numTable).State = EntityState.Modified;
            await SaveAsync();
        }
        public async Task Delete(int id)
        {
            NumTable numTable = await _dbContext.NumTables.FindAsync(id);
            _dbContext.NumTables.Remove(numTable);
            await SaveAsync();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
