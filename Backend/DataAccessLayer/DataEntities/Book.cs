using System;
using System.Collections.Generic;



namespace DAL.DataEntities
{
    public partial class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int? Rating { get; set; }
        public int AuthorId { get; set; }
        public int? GenreId { get; set; }
        public bool IsBookAvailable { get; set; }
        public string Description { get; set; }
        public int? LenderUserId { get; set; }
        public int? BorrowerUserId { get; set; }

        public virtual Author Author { get; set; }
        public virtual User BorrowerUser { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual User LenderUser { get; set; }
    }
}
