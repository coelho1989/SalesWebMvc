using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            IQueryable<SalesRecord> record = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                record = record.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                record = record.Where(x => x.Date <= maxDate.Value);
            }

            return await record
                .Include(x=>x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x=>x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            IQueryable<SalesRecord> record = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                record = record.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                record = record.Where(x => x.Date <= maxDate.Value);
            }

            return await record
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x=>x.Seller.Department)
                .ToListAsync();
        }
    }
}
