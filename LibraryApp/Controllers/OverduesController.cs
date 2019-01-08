using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class OverduesController : Controller
    {
        public OverduesController(IBorrowRepository borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var overdueBorrows = await _borrowRepository.GetOverdueBorrowsAsync();
            var books = overdueBorrows.Select(b => b.Book.ToViewModel()).ToList();
            return View(books);
        }

        private readonly IBorrowRepository _borrowRepository;
    }
}