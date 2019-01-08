using System;
using LibraryApp.Models;

namespace LibraryApp.Data
{
    public interface IBorrowerRepository: IRepository<Borrower, int>
    {
        
    }

    public class BorrowerRepository: SqliteRepository<Borrower, int>, IBorrowerRepository
    {
        public BorrowerRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}