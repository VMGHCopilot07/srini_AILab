using System.Collections.Generic;
using System.Threading.Tasks;
using LabTwoApi.Models;

namespace LabTwoApi.Repositories
{
    public interface IRecordRepository
    {
        Task<IEnumerable<Record>> GetAllAsync();
        Task<Record> GetByIdAsync(int id);
        Task<Record> AddAsync(Record record);
        Task<Record> UpdateAsync(Record record);
        Task DeleteAsync(int id);
    }
}
