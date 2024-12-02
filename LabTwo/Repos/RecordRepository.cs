using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LabTwoApi.Data;
using LabTwoApi.Models;

namespace LabTwoApi.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly AppDbContext _context;

        public RecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Record>> GetAllAsync()
        {
            return await _context.Records.ToListAsync();
        }

        public async Task<Record> GetByIdAsync(int id)
        {
            return await _context.Records.FindAsync(id);
        }

        public async Task<Record> AddAsync(Record record)
        {
            _context.Records.Add(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task<Record> UpdateAsync(Record record)
        {
            _context.Entry(record).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return record;
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _context.Records.FindAsync(id);
            if (record != null)
            {
                _context.Records.Remove(record);
                await _context.SaveChangesAsync();
            }
        }
    }
}
