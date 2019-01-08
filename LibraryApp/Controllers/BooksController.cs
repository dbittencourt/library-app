using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class BooksController : Controller
    {
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllWithBorrowsAsync();
            if (!books.Any()) return RedirectToAction(nameof(Create));
            var x = books.Select(b => b.ToViewModel());
            return View(x);
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return NotFound();
            var book = await _bookRepository.GetAsync(id.Value);
            if (book == null)
                return NotFound();

            return View(book.ToViewModel());
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel book)
        {
            if (!ModelState.IsValid) return View(book);
            
            await _bookRepository.AddAsync(new Book
            {
                Title = book.Title,
                Author = book.Author
            });
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue) return NotFound();

            var book = await _bookRepository.GetAsync(id.Value);
            if (book == null)
                return NotFound();
            
            return View(book.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookViewModel book)
        {
            if (!ModelState.IsValid) return View(book);
            await _bookRepository.UpdateAsync(id, new Book
            {
                Author = book.Author,
                Title = book.Title,
                Id = id
            });
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return NotFound();

            var book = await _bookRepository.GetAsync(id.Value);
            if (book == null) return NotFound();

            return View(book.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookRepository.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        
        private readonly IBookRepository _bookRepository;
    }
}