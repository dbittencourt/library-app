using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace LibraryApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public List<Borrow> Borrows { get; set; }

        public BookViewModel ToViewModel()
        {
            return new BookViewModel()
            {
                Id = this.Id,
                Title = this.Title,
                Author = this.Author,
                Available = this.Borrows == null || this.Borrows.All(b => b.To != null)
            };
        }
    }
}