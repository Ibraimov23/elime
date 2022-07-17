using ElimeProject.Data;
using ElimeProject.Models;
using ElimeProject.Repository.Parents;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ElimeProject.Repository.Childs
{
    public class NumberRepository : INumberRepository
    {
        private readonly ApplicationContext _dbContext;
        public NumberRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<NumTable> Number(int? id)
        {
            NumTable num = await _dbContext.NumTables.FirstOrDefaultAsync(p => p.Number == id);
            return num;
        }
    }
}
