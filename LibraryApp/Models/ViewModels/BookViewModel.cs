using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public bool Available { get; set; }
    }
}