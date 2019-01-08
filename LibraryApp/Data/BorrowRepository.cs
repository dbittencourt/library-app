using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Data
{
    public interface IBorrowRepository : IRepository<Borrow, int>
    {
        Task<Borrow> GetBorrowWithRelatedDataAsync(int id);
        Task<IEnumerable<Borrow>> GetOverdueBorrowsAsync();
    }

    public class BorrowRepository : SqliteRepository<Borrow, int>, IBorrowRepository
    {
        public BorrowRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<Borrow> GetBorrowWithRelatedDataAsync(int id)
        {
            var borrow = await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.Borrower)
                .Where(b => b.BorrowId == id)
                .FirstOrDefaultAsync();

            return borrow;
        }

        public async Task<IEnumerable<Borrow>> GetOverdueBorrowsAsync()
        {
            var borrows = await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.Borrower)
                .Where(b => b.To == null && DateTime.Now.DayOfYear - b.From.DayOfYear > 7)
                .ToListAsync();

            return borrows;
        }
    }
}