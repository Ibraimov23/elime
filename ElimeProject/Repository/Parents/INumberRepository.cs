using ElimeProject.Models;
using System.Threading.Tasks;

namespace ElimeProject.Repository.Parents
{
    public interface INumberRepository
    {
        Task<NumTable> Number(int? id);
    }
}
