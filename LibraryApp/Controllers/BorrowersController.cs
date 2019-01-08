using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class BorrowersController : Controller
    {
        public BorrowersController(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public async Task<IActionResult> Index()
        {
            var borrowers = await _borrowerRepository.GetAllAsync();
            if (!borrowers.Any()) return RedirectToAction(nameof(Create));
            return View(borrowers);
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return NotFound();
            var borrower = await _borrowerRepository.GetAsync(id.Value);
            if (borrower == null)
                return NotFound();

            return View(borrower);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Borrower borrower)
        {
            if (!ModelState.IsValid) return View(borrower);
            await _borrowerRepository.AddAsync(borrower);
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue) return NotFound();

            var borrower = await _borrowerRepository.GetAsync(id.Value);
            if (borrower == null)
                return NotFound();
            
            return View(borrower);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Borrower borrower)
        {
            if (!ModelState.IsValid) return View(borrower);
            await _borrowerRepository.UpdateAsync(id, borrower);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return NotFound();

            var borrower = await _borrowerRepository.GetAsync(id.Value);
            if (borrower == null) return NotFound();

            return View(borrower);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _borrowerRepository.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        private readonly IBorrowerRepository _borrowerRepository;
    }
}