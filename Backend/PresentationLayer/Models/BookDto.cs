using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class BookDto
    {
        public int BookId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int? Rating { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int? GenreId { get; set; }
        [Required]
        public bool IsBookAvailable { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int LenderUserId { get; set; }
        public int? BorrowerUserId { get; set; }
    }
}
