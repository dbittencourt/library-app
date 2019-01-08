using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Data
{
    public interface IBookRepository : IRepository<Book, int>
    {
        Task<IEnumerable<Book>> FindByTitleAsync(string title);
        Task<IEnumerable<Book>> GetAllWithBorrowsAsync();
        Task<Book> GetBookWithBorrowAsync(int id);
    }

    public class BookRepository: SqliteRepository<Book, int>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> FindByTitleAsync(string title)
        {
            var books = _entities.Where(b => b.Title.Contains(title));

            return await books.ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAllWithBorrowsAsync()
        {
            var books = await _context.Books.Include(b => b.Borrows)
                .ToListAsync();

            return books;
        }

        public async Task<Book> GetBookWithBorrowAsync(int id)
        {
            var book = await _context.Books.Include(b => b.Borrows)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();

            return book;
        }
    }
}