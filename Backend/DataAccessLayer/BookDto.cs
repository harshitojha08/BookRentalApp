using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int? Rating { get; set; }
        public int AuthorId { get; set; }
        public int? GenreId { get; set; }
        public bool IsBookAvailable { get; set; }
        public string Description { get; set; }
        public int? LenderUserId { get; set; }
    }
}
