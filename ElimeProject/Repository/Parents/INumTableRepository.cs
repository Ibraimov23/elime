using ElimeProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElimeProject.Repository.Parents
{
    public interface INumTableRepository
    {
        Task<List<NumTable>> Get();
        Task Create(NumTable numTable);
        Task Delete(int id);
        Task Update(NumTable numTable);
        Task SaveAsync();
    }
}
