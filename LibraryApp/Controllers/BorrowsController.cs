using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryApp.Controllers
{
    public class BorrowsController : Controller
    {

        public BorrowsController(IBookRepository bookRepository, IBorrowerRepository borrowerRepository,
            IBorrowRepository borrowRepository)
        {
            _bookRepository = bookRepository;
            _borrowerRepository = borrowerRepository;
            _borrowRepository = borrowRepository;
        }
        
        public async Task<IActionResult> Create(int? id)
        {
            if (!id.HasValue) return NotFound();

            var book = await _bookRepository.GetAsync(id.Value);
            if (book == null) return NotFound();
 
            var borrow = new Borrow 
            { 
                BookId = book.Id, 
                From = DateTime.Now 
            };

            var borrowers = await _borrowerRepository.GetAllAsync();
            ViewBag.Borrowers = borrowers.Select(b => new SelectListItem() { Text = b.FirstName, Value= b.Id.ToString()});
            return View(borrow);
        }
 
        [HttpPost]
        public async Task<IActionResult> Create(Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                await _borrowRepository.AddAsync(borrow);
                return RedirectToAction("Index", "Books");
            }

            return View(borrow);
        }
 
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue) return NotFound();

            var book = await _bookRepository.GetBookWithBorrowAsync(id.Value);
            var borrow = book.Borrows.FirstOrDefault(b => b.To == null);
            if (borrow == null) return NotFound();
            borrow = await _borrowRepository.GetBorrowWithRelatedDataAsync(borrow.BorrowId);
            return View(borrow);
        }
 
        [HttpPost]
        public async Task<IActionResult> Edit(Borrow borrow)
        {
            if (ModelState.IsValid)
            {
                borrow.To = DateTime.Now;
                await _borrowRepository.UpdateAsync(borrow.BorrowId, borrow);
                return RedirectToAction("Index", "Books");
            }
            
            return View(borrow);
        }

        private readonly IBookRepository _bookRepository;
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly IBorrowRepository _borrowRepository;
    }
}