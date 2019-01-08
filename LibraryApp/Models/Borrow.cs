using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Borrow
    {
        [Key]
        public int BorrowId { get; set; }
        [Required]
        [Display(Name="Borrower")]
        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
        [Required]
        [Display(Name="Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}